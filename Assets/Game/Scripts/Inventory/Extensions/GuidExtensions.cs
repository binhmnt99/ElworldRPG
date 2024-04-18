using System;

namespace Scripts.Inventory
{
    public static class GuidExtensions
    {
        public static SerializableGuid ToSerializableGuid(this Guid systemGuid)
        {
            byte[] bytes = systemGuid.ToByteArray();
            uint part1 = BitConverter.ToUInt32(bytes, 0);
            uint part2 = BitConverter.ToUInt32(bytes, 4);
            uint part3 = BitConverter.ToUInt32(bytes, 8);
            uint part4 = BitConverter.ToUInt32(bytes, 12);
            return new SerializableGuid(part1, part2, part3, part4);
        }

        public static Guid ToSystemGuid(this SerializableGuid serializableGuid)
        {
            byte[] bytes = new byte[16];
            //sao chép dữ liệu từ một mảng byte (chứa các phần của một serializableGuid) vào một mảng byte khác
            Buffer.BlockCopy(BitConverter.GetBytes(serializableGuid.Part1), 0, bytes, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(serializableGuid.Part2), 0, bytes, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(serializableGuid.Part3), 0, bytes, 8, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(serializableGuid.Part4), 0, bytes, 12, 4);
            return new Guid(bytes);
        }
    }
}
