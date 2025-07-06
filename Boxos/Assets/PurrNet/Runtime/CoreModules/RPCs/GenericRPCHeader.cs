﻿using System;
using JetBrains.Annotations;
using PurrNet.Logging;
using PurrNet.Packing;
using PurrNet.Utils;

namespace PurrNet
{
    public struct GenericRPCHeader
    {
        public BitPacker stream;
        public Type[] types;
        public object[] values;
        public RPCInfo info;

        [UsedImplicitly]
        public void SetPlayerId(PlayerID player, int index)
        {
            values[index] = player;
        }

        [UsedImplicitly]
        public void SetInfo(int index)
        {
            values[index] = info;
        }

        [UsedImplicitly]
        public void Read(int genericIndex, int index)
        {
            PackedUInt hash = default;
            Packer<PackedUInt>.Read(stream, ref hash);

            if (!Hasher.TryGetType(hash, out var type))
            {
                throw new InvalidOperationException(
                    PurrLogger.FormatMessage($"Type with hash '{hash}' not found.")
                );
            }

            Packer.Read(stream, type, ref values[index]);
        }

        [UsedImplicitly]
        public void Read<T>(int index)
        {
            T value = default;
            Packer<T>.Read(stream, ref value);
            values[index] = value;
        }
    }
}
