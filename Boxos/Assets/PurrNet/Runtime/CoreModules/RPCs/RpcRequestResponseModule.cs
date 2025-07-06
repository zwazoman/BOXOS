using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PurrNet.Logging;
using PurrNet.Packing;
using PurrNet.Transports;
using UnityEngine;

#if !UNITASK_PURRNET_SUPPORT
using RawTask = System.Threading.Tasks.Task;
#else
using Cysharp.Threading.Tasks;
using RawTask = Cysharp.Threading.Tasks.UniTask;
#endif

namespace PurrNet.Modules
{
    public struct RpcRequest
    {
        [UsedByIL] public uint id;
        public PlayerID? target;

        public float timeSent;
        public float timeout;

        public Action timeoutRequest;
        public Action<BitPacker> respond;
    }

    public struct RpcResponse
    {
        public uint id;
        public ByteData data;
    }

    public class RpcRequestResponseModule : INetworkModule, IFixedUpdate
    {
        private readonly PlayersManager _playersManager;
        private readonly List<RpcRequest> _requests = new List<RpcRequest>();

        private uint _nextId;

        public RpcRequestResponseModule(PlayersManager playersManager)
        {
            _playersManager = playersManager;
        }

        public void Enable(bool asServer)
        {
            _playersManager.Subscribe<RpcResponse>(OnRpcResponse);
        }

        public void Disable(bool asServer)
        {
            _playersManager.Unsubscribe<RpcResponse>(OnRpcResponse);
        }

        private void OnRpcResponse(PlayerID conn, RpcResponse data, bool asServer)
        {
            for (int i = 0; i < _requests.Count; i++)
            {
                var request = _requests[i];
                if (request.id == data.id && (!request.target.HasValue || request.target == conn))
                {
                    _requests.RemoveAt(i);

                    using var stream = RPCModule.AllocStream(true);
                    stream.WriteBytes(data.data);
                    stream.ResetPosition();

                    request.respond(stream);
                    break;
                }
            }
        }

        [UsedByIL]
        public static Task GetNextIdStatic(PlayerID? target, RPCType rpcType, float timeout, out RpcRequest request)
        {
            var networkManager = NetworkManager.main;
            request = default;

            if (!networkManager)
            {
                return Task.FromException(new InvalidOperationException(
                    "NetworkManager is not initialized. Make sure you have a NetworkManager active."));
            }

            bool asServer = rpcType switch
            {
                RPCType.ServerRPC => !networkManager.isClient,
                RPCType.TargetRPC => networkManager.isServer,
                RPCType.ObserversRPC => networkManager.isServer,
                _ => throw new ArgumentOutOfRangeException(nameof(rpcType), rpcType, null)
            };

            if (!networkManager.TryGetModule(out RpcRequestResponseModule rpcModule, asServer))
            {
                return Task.FromException(new InvalidOperationException(
                    "RpcRequestResponseModule is not initialized.."));
            }

            return rpcModule.GetNextId(target, timeout, out request);
        }

        [UsedByIL]
        public static Task<T> GetNextIdStatic<T>(PlayerID? target, RPCType rpcType, float timeout,
            out RpcRequest request)
        {
            var networkManager = NetworkManager.main;
            request = default;

            if (!networkManager)
            {
                return Task.FromException<T>(new InvalidOperationException(
                    "NetworkManager is not initialized. Make sure you have a NetworkManager active."));
            }

            bool asServer = rpcType switch
            {
                RPCType.ServerRPC => !networkManager.isClient,
                RPCType.TargetRPC => networkManager.isServer,
                RPCType.ObserversRPC => networkManager.isServer,
                _ => throw new ArgumentOutOfRangeException(nameof(rpcType), rpcType, null)
            };

            if (!networkManager.TryGetModule(out RpcRequestResponseModule rpcModule, asServer))
            {
                return Task.FromException<T>(new InvalidOperationException(
                    "RpcRequestResponseModule is not initialized.."));
            }

            return rpcModule.GetNextId<T>(target, timeout, out request);
        }

        [UsedByIL]
        public static RawTask GetNextIdUniTaskStatic(PlayerID? target, RPCType rpcType, float timeout,
            out RpcRequest request)
        {
            var networkManager = NetworkManager.main;
            request = default;

            if (!networkManager)
            {
                return RawTask.FromException(new InvalidOperationException(
                    "NetworkManager is not initialized. Make sure you have a NetworkManager active."));
            }

            bool asServer = rpcType switch
            {
                RPCType.ServerRPC => !networkManager.isClient,
                RPCType.TargetRPC => networkManager.isServer,
                RPCType.ObserversRPC => networkManager.isServer,
                _ => throw new ArgumentOutOfRangeException(nameof(rpcType), rpcType, null)
            };

            if (!networkManager.TryGetModule(out RpcRequestResponseModule rpcModule, asServer))
            {
                return RawTask.FromException(new InvalidOperationException(
                    "RpcRequestResponseModule is not initialized.."));
            }

            return rpcModule.GetNextIdUniTask(target, timeout, out request);
        }

        [UsedByIL]
#if !UNITASK_PURRNET_SUPPORT
        public static Task<T>
#else
        public static UniTask<T>
#endif
        GetNextIdUniTaskStatic<T>(PlayerID? target, RPCType rpcType, float timeout,
            out RpcRequest request)
        {
            var networkManager = NetworkManager.main;
            request = default;

            if (!networkManager)
            {
                return RawTask.FromException<T>(new InvalidOperationException(
                    "NetworkManager is not initialized. Make sure you have a NetworkManager active."));
            }

            bool asServer = rpcType switch
            {
                RPCType.ServerRPC => !networkManager.isClient,
                RPCType.TargetRPC => networkManager.isServer,
                RPCType.ObserversRPC => networkManager.isServer,
                _ => throw new ArgumentOutOfRangeException(nameof(rpcType), rpcType, null)
            };

            if (!networkManager.TryGetModule(out RpcRequestResponseModule rpcModule, asServer))
            {
                return RawTask.FromException<T>(new InvalidOperationException(
                    "RpcRequestResponseModule is not initialized.."));
            }

            return rpcModule.GetNextIdUniTask<T>(target, timeout, out request);
        }

        public Task GetNextId(PlayerID? target, float timeout, out RpcRequest request)
        {
            var tcs = new TaskCompletionSource<bool>();
            var id = _nextId++;

            request = new RpcRequest
            {
                id = id,
                target = target,
                timeSent = Time.unscaledTime,
                timeout = timeout,
                respond = _ => { tcs.SetResult(true); },
                timeoutRequest = () =>
                {
                    tcs.SetException(
                        new TimeoutException(
                            $"Async RPC with request id of '{id}' timed out after {timeout} seconds."));
                }
            };

            _requests.Add(request);
            return tcs.Task;
        }

        public RawTask GetNextIdUniTask(PlayerID? target, float timeout, out RpcRequest request)
        {
#if !UNITASK_PURRNET_SUPPORT
            var tcs = new TaskCompletionSource<bool>();
#else
            var tcs = new UniTaskCompletionSource<bool>();
#endif
            var id = _nextId++;

            request = new RpcRequest
            {
                id = id,
                target = target,
                timeSent = Time.unscaledTime,
                timeout = timeout,
                respond = _ => { tcs.TrySetResult(true); },
                timeoutRequest = () =>
                {
                    tcs.TrySetException(
                        new TimeoutException(
                            $"Async RPC with request id of '{id}' timed out after {timeout} seconds."));
                }
            };

            _requests.Add(request);
            return tcs.Task;
        }

#if !UNITASK_PURRNET_SUPPORT
        public Task<T>
#else
        public UniTask<T>
#endif
        GetNextIdUniTask<T>(PlayerID? target, float timeout, out RpcRequest request)
        {
#if !UNITASK_PURRNET_SUPPORT
            var tcs = new TaskCompletionSource<T>();
#else
            var tcs = new UniTaskCompletionSource<T>();
#endif
            var id = _nextId++;

            request = new RpcRequest
            {
                id = id,
                target = target,
                timeSent = Time.unscaledTime,
                timeout = timeout,
                respond = stream =>
                {
                    T response = default;
                    Packer<T>.Read(stream, ref response);
                    tcs.TrySetResult(response);
                },
                timeoutRequest = () =>
                {
                    tcs.TrySetException(
                        new TimeoutException(
                            $"Async RPC with request id of '{id}' timed out after {timeout} seconds."));
                }
            };

            _requests.Add(request);
            return tcs.Task;
        }

        public Task<T> GetNextId<T>(PlayerID? target, float timeout, out RpcRequest request)
        {
            var tcs = new TaskCompletionSource<T>();
            var id = _nextId++;

            request = new RpcRequest
            {
                id = id,
                target = target,
                timeSent = Time.unscaledTime,
                timeout = timeout,
                respond = stream =>
                {
                    T response = default;
                    Packer<T>.Read(stream, ref response);
                    tcs.SetResult(response);
                },
                timeoutRequest = () =>
                {
                    tcs.SetException(
                        new TimeoutException(
                            $"Async RPC with request id of '{id}' timed out after {timeout} seconds."));
                }
            };

            _requests.Add(request);
            return tcs.Task;
        }

        public void FixedUpdate()
        {
            for (int i = 0; i < _requests.Count; i++)
            {
                var request = _requests[i];
                if (Time.unscaledTime - request.timeSent > request.timeout)
                {
                    _requests.RemoveAt(i);
                    i--;
                    request.timeoutRequest();
                }
            }
        }

        static IEnumerator WaitThen(IEnumerator coroutine, Action action)
        {
            yield return coroutine;

            try
            {
                action();
            }
            catch (Exception ex)
            {
                PurrLogger.LogError($"Error while processing RPC response: {ex.Message}\n{ex.StackTrace}");
            }
        }

        [UsedByIL]
        public static void CompleteRequestWithCoroutine(IEnumerator response, RPCInfo info, uint reqId,
            NetworkManager manager)
        {
            try
            {
                manager.StartCoroutine(WaitThen(response, () =>
                {
                    if (manager.TryGetModule<RpcRequestResponseModule>(info.asServer, out var rpcModule))
                    {
                        // rpcModule
                        var responsePacket = new RpcResponse
                        {
                            id = reqId,
                            data = ByteData.empty
                        };

                        var channel = info.compileTimeSignature.channel;

                        if (info.asServer)
                            rpcModule._playersManager.Send(info.sender, responsePacket, channel);
                        else rpcModule._playersManager.SendToServer(responsePacket, channel);
                    }
                    else
                    {
                        PurrLogger.LogError("Failed to get module, response won't be sent and receiver will timeout.");
                    }
                }));
            }
            catch (Exception ex)
            {
                PurrLogger.LogError($"Error while processing RPC response: {ex.Message}\n{ex.StackTrace}");
            }
        }

        [UsedByIL]
        public static async void CompleteRequestWithEmptyResponse(Task response, RPCInfo info, uint reqId,
            NetworkManager manager)
        {
            try
            {
                await response;

                if (manager.TryGetModule<RpcRequestResponseModule>(info.asServer, out var rpcModule))
                {
                    // rpcModule
                    var responsePacket = new RpcResponse
                    {
                        id = reqId,
                        data = ByteData.empty
                    };

                    var channel = info.compileTimeSignature.channel;

                    if (info.asServer)
                        rpcModule._playersManager.Send(info.sender, responsePacket, channel);
                    else rpcModule._playersManager.SendToServer(responsePacket, channel);
                }
                else
                {
                    PurrLogger.LogError("Failed to get module, response won't be sent and receiver will timeout.");
                }
            }
            catch (Exception ex)
            {
                PurrLogger.LogError($"Error while processing RPC response: {ex.Message}\n{ex.StackTrace}");
            }
        }

        [UsedByIL]
        public static void CompleteRequestWithResponseObject<T>(object task, RPCInfo info, uint reqId,
            NetworkManager manager)
        {
            if (task is not Task<T> response)
            {
                PurrLogger.LogError("Task is not UniTask<T>, response won't be sent and receiver will timeout.");
                return;
            }

            CompleteRequestWithResponse(response, info, reqId, manager);
        }

        [UsedByIL]
        public static async void CompleteRequestWithResponse<T>(Task<T> response, RPCInfo info, uint reqId,
            NetworkManager manager)
        {
            try
            {
                var result = await response;

                if (manager.TryGetModule<RpcRequestResponseModule>(info.asServer, out var rpcModule))
                {
                    using var tmpStream = RPCModule.AllocStream(false);

                    Packer<T>.Write(tmpStream, result);

                    // rpcModule
                    var responsePacket = new RpcResponse
                    {
                        id = reqId,
                        data = tmpStream.ToByteData()
                    };

                    var channel = info.compileTimeSignature.channel;

                    if (info.asServer)
                        rpcModule._playersManager.Send(info.sender, responsePacket, channel);
                    else rpcModule._playersManager.SendToServer(responsePacket, channel);
                }
                else
                {
                    PurrLogger.LogError("Failed to get module, response won't be sent and receiver will timeout.");
                }
            }
            catch (Exception ex)
            {
                PurrLogger.LogError($"Error while processing RPC response: {ex.Message}\n{ex.StackTrace}");
            }
        }

        [UsedByIL]
        public static async void CompleteRequestWithUniTaskEmptyResponse(RawTask response, RPCInfo info, uint reqId,
            NetworkManager manager)
        {
            try
            {
                await response;

                if (manager.TryGetModule<RpcRequestResponseModule>(info.asServer, out var rpcModule))
                {
                    // rpcModule
                    var responsePacket = new RpcResponse
                    {
                        id = reqId,
                        data = ByteData.empty
                    };

                    var channel = info.compileTimeSignature.channel;

                    if (info.asServer)
                        rpcModule._playersManager.Send(info.sender, responsePacket, channel);
                    else rpcModule._playersManager.SendToServer(responsePacket, channel);
                }
                else
                {
                    PurrLogger.LogError("Failed to get module, response won't be sent and receiver will timeout.");
                }
            }
            catch (Exception ex)
            {
                PurrLogger.LogError($"Error while processing RPC response: {ex.Message}\n{ex.StackTrace}");
            }
        }

        [UsedByIL]
        public static void CompleteRequestWithUniTaskObject<T>(object task, RPCInfo info, uint reqId,
            NetworkManager manager)
        {
#if !UNITASK_PURRNET_SUPPORT
            if (task is not Task<T> response)
#else
            if (task is not UniTask<T> response)
#endif
            {
                PurrLogger.LogError("Task is not UniTask<T>, response won't be sent and receiver will timeout.");
                return;
            }

            CompleteRequestWithUniTask(response, info, reqId, manager);
        }

        [UsedByIL]
        public static async void CompleteRequestWithUniTask<T>(
#if !UNITASK_PURRNET_SUPPORT
            Task<T> response
#else
            UniTask<T> response
#endif
            , RPCInfo info, uint reqId,
            NetworkManager manager)
        {
            try
            {
                var result = await response;

                if (manager.TryGetModule<RpcRequestResponseModule>(info.asServer, out var rpcModule))
                {
                    using var tmpStream = RPCModule.AllocStream(false);

                    Packer<T>.Write(tmpStream, result);

                    // rpcModule
                    var responsePacket = new RpcResponse
                    {
                        id = reqId,
                        data = tmpStream.ToByteData()
                    };

                    var channel = info.compileTimeSignature.channel;

                    if (info.asServer)
                        rpcModule._playersManager.Send(info.sender, responsePacket, channel);
                    else rpcModule._playersManager.SendToServer(responsePacket, channel);
                }
                else
                {
                    PurrLogger.LogError("Failed to get module, response won't be sent and receiver will timeout.");
                }
            }
            catch (Exception ex)
            {
                PurrLogger.LogError($"Error while processing RPC response: {ex.Message}\n{ex.StackTrace}");
            }
        }

        [UsedByIL]
        public static IEnumerator WaitForTask(Task task)
        {
            while (task is { IsCompleted: false })
                yield return null;

            if (task is { IsFaulted: true, Exception: not null })
                throw task.Exception;
        }
    }
}
