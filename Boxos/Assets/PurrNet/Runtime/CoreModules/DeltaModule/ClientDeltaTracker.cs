﻿using System;
using System.Collections.Generic;
using PurrNet.Packing;
using UnityEngine;

namespace PurrNet.Modules
{
    internal abstract class ClientDeltaTracker : IDisposable
    {
        protected uint mostRecentId;
        private uint nextId = 1;

        public uint GenerateId()
        {
            return nextId++;
        }

        public abstract uint CleanupUpTo(float maxAge);

        public abstract void CleanupUpTo(uint exclusive);

        public abstract bool ContainsKey(uint id);

        public abstract void Dispose();

        public void ValidateId(PackedUInt dataValueId)
        {
            if (dataValueId.value > mostRecentId)
                mostRecentId = dataValueId.value;
        }
    }

    internal class ClientDeltaTracker<T> : ClientDeltaTracker
    {
        const int MAX_HISTORY_SIZE = 64;

        private struct Entry
        {
            public uint key;
            public T value;
            public float enterTime;
        }

        private readonly List<Entry> _history = new(MAX_HISTORY_SIZE);

        private int BinarySearch(uint key)
        {
            int low = 0;
            int high = _history.Count - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                if (_history[mid].key < key)
                {
                    low = mid + 1;
                }
                else if (_history[mid].key > key)
                {
                    high = mid - 1;
                }
                else
                {
                    return mid;
                }
            }

            return low;
        }

        private int BinarySearchOlderThan(float seconds)
        {
            float threshold = Time.unscaledTime - seconds;
            int low = 0;
            int high = _history.Count - 1;
            int result = _history.Count;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                if (_history[mid].enterTime < threshold)
                {
                    result = mid;
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            return result;
        }

        public int GetLastMatch()
        {
            if (_history.Count == 0)
                return -1;
            return _history.Count - 1;
        }

        public T GetLastValue()
        {
            if (_history.Count == 0)
                return default;
            return _history[^1].value;
        }

        public int FindBestMatch(out uint key)
        {
            key = mostRecentId;
            int index = BinarySearch(mostRecentId);
            if (index < _history.Count && _history[index].key == mostRecentId)
                return index;
            return -1;
        }

        public override uint CleanupUpTo(float maxAge)
        {
            // If the history is smaller than the maximum size, we don't need to clean up.
            if (_history.Count < MAX_HISTORY_SIZE)
                return 0;

            if (_history.Count == 0)
                return 0;

            bool isFirstOldEnough = _history[0].enterTime < Time.unscaledTime - maxAge;

            if (!isFirstOldEnough)
                return 0;

            int removeUpTo = BinarySearchOlderThan(maxAge);

            for (int i = 0; i < removeUpTo; i++)
            {
                if (_history[i].value is IDisposable disposable)
                    disposable.Dispose();
            }

            _history.RemoveRange(0, removeUpTo);
            return _history.Count > 0 ? _history[0].key : 0;
        }

        public override void CleanupUpTo(uint exclusive)
        {
            int removeUpTo = BinarySearch(exclusive);

            if (removeUpTo == 0)
                return;

            if (removeUpTo >= _history.Count)
            {
                for (int i = 0; i < _history.Count; i++)
                {
                    if (_history[i].value is IDisposable disposable)
                        disposable.Dispose();
                }

                _history.Clear();
                return;
            }

            for (int i = 0; i < removeUpTo; i++)
            {
                if (_history[i].value is IDisposable disposable)
                    disposable.Dispose();
            }

            _history.RemoveRange(0, removeUpTo);
        }

        public override bool ContainsKey(uint id)
        {
            int index = BinarySearch(id);
            return index < _history.Count && _history[index].key == id;
        }

        public override void Dispose()
        {
            if (_history != null)
            {
                int c = _history.Count;
                for (int i = 0; i < c; i++)
                {
                    if (_history[i].value is IDisposable disposable)
                        disposable.Dispose();
                }

                _history.Clear();
            }
        }

        public bool TryGetValueAtIndex(int id, out T o)
        {
            if (id < 0 || id >= _history.Count)
            {
                o = default;
                return false;
            }

            o = _history[id].value;
            return true;
        }

        public bool TryGetValue(uint id, out T o)
        {
            int index = BinarySearch(id);

            if (index < _history.Count && _history[index].key == id)
            {
                o = _history[index].value;
                return true;
            }

            o = default;
            return false;
        }

        public void Set(T oldValue)
        {
            _history.Clear();
            _history.Add(new Entry { key = 0, value = Packer.Copy(oldValue), enterTime = Time.unscaledTime });
        }

        public void Set(uint id, T newValue)
        {
            int index = BinarySearch(id);
            if (index < _history.Count && _history[index].key == id)
            {
                var old = _history[index];
                if (old.value is IDisposable disposable)
                    disposable.Dispose();
                _history[index] = new Entry { key = id, value = Packer.Copy(newValue) };
            }
            else
            {
                _history.Insert(index, new Entry { key = id, value = Packer.Copy(newValue) });
            }
        }
    }
}
