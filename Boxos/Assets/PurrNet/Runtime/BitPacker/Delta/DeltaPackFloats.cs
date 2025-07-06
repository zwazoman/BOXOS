using System;
using PurrNet.Modules;
using UnityEngine;

namespace PurrNet.Packing
{
    public static class DeltaPackFloats
    {
        [UsedByIL]
        private static void WriteHalf(BitPacker packer, Half value)
        {
            Packer<ushort>.Write(packer, value.rawValue);
        }

        [UsedByIL]
        private static void ReadHalf(BitPacker packer, ref Half value)
        {
            ushort rawValue = default;
            Packer<ushort>.Read(packer, ref rawValue);
            value = Half.FromRawValue(rawValue);
        }

        [UsedByIL]
        private static void WriteCompressedFloat(BitPacker packer, CompressedFloat value)
        {
            Packer<PackedInt>.Write(packer, value.ToPackedInt());
        }

        [UsedByIL]
        private static void ReadCompressedFloat(BitPacker packer, ref CompressedFloat value)
        {
            PackedInt val = default;
            Packer<PackedInt>.Read(packer, ref val);
            value = new CompressedFloat(val.value * CompressedFloat.PRECISION);
        }

        [UsedByIL]
        private static bool WriteHalf(BitPacker packer, Half oldvalue, Half newvalue)
        {
            return DeltaPacker<ushort>.Write(packer, oldvalue.rawValue, newvalue.rawValue);
        }

        [UsedByIL]
        private static void ReadHalf(BitPacker packer, Half oldvalue, ref Half value)
        {
            ushort newValue = default;
            DeltaPacker<ushort>.Read(packer, oldvalue.rawValue, ref newValue);
            value = Half.FromRawValue(newValue);
        }

        [UsedByIL]
        private static unsafe bool WriteDouble(BitPacker packer, double oldvalue, double newvalue)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            bool hasChanged = oldvalue != newvalue;

            Packer<bool>.Write(packer, hasChanged);

            if (hasChanged)
            {
                ulong oldBits = *(ulong*)&oldvalue;
                ulong newBits = *(ulong*)&newvalue;
                long diff = (long)(newBits - oldBits);
                Packer<PackedLong>.Write(packer, diff);
            }

            return hasChanged;
        }

        [UsedByIL]
        private static unsafe void ReadDouble(BitPacker packer, double oldvalue, ref double value)
        {
            bool hasChanged = default;
            Packer<bool>.Read(packer, ref hasChanged);

            if (hasChanged)
            {
                PackedLong packed = default;
                Packer<PackedLong>.Read(packer, ref packed);
                ulong oldBits = *(ulong*)&oldvalue;
                ulong newBits = (ulong)((long)oldBits + packed.value);
                value = *(double*)&newBits;
            }
            else value = oldvalue;
        }

        [UsedByIL]
        private static bool WriteSingle(BitPacker packer, CompressedFloat oldvalue, CompressedFloat newvalue)
        {
            float delta = newvalue - oldvalue;

            if (Math.Abs(delta) <CompressedFloat. PRECISION)
            {
                Packer<bool>.Write(packer, false);
                return false;
            }

            Packer<bool>.Write(packer, true);

            var deltaAsInt = Mathf.RoundToInt(delta / CompressedFloat.PRECISION);
            var estimatedNewValue = oldvalue + deltaAsInt * CompressedFloat.PRECISION;
            bool isCorrect = Mathf.Abs(newvalue - estimatedNewValue) < CompressedFloat.PRECISION * 2;

            Packer<bool>.Write(packer, isCorrect);

            if (isCorrect)
            {
                Packer<PackedInt>.Write(packer, deltaAsInt);
            }
            else
            {
                Packer<CompressedFloat>.Write(packer, newvalue);
            }

            return true;
        }

        [UsedByIL]
        private static void ReadSingle(BitPacker packer, CompressedFloat oldvalue, ref CompressedFloat value)
        {
            bool hasChanged = default;
            Packer<bool>.Read(packer, ref hasChanged);

            if (hasChanged)
            {
                bool isCorrect = default;
                Packer<bool>.Read(packer, ref isCorrect);

                if (isCorrect)
                {
                    PackedInt packed = default;
                    Packer<PackedInt>.Read(packer, ref packed);
                    value = oldvalue + packed.value * CompressedFloat.PRECISION;
                }
                else
                {
                    Packer<CompressedFloat>.Read(packer, ref value);
                }
            }
            else value = oldvalue;
        }
    }
}
