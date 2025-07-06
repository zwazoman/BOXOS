﻿using System;
using UnityEngine;

namespace PurrNet.Packing
{
    [System.Serializable]
    public struct CompressedVector3 : IEquatable<CompressedVector3>
    {
        public CompressedFloat x;
        public CompressedFloat y;
        public CompressedFloat z;

        public CompressedVector3(CompressedFloat x, CompressedFloat y, CompressedFloat z)
        {
            this.x = x.Round();
            this.y = y.Round();
            this.z = z.Round();
        }

        public override string ToString()
        {
            return $"CompressedVector3({x}, {y}, {z})";
        }

        public static implicit operator CompressedVector3(Vector3 value) => new CompressedVector3(value.x, value.y, value.z);
        public static implicit operator Vector3(CompressedVector3 vector) => new Vector3(vector.x.value, vector.y.value, vector.z.value);

        public bool Equals(CompressedVector3 other)
        {
            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
        }

        public override bool Equals(object obj)
        {
            return obj is CompressedVector3 other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }
    }
}
