using System;
using UnityEngine;

namespace Scripts.Inventory
{
    [Serializable]
    public class SerializableGuid : IEquatable<SerializableGuid>//là một giao diện (interface) được sử dụng để xác định một phương thức cụ thể cho việc so sánh tính bằng giữa các thể hiện của một kiểu dữ liệu (value type hoặc class)
    {
        [SerializeField, HideInInspector] public uint Part1;
        [SerializeField, HideInInspector] public uint Part2;
        [SerializeField, HideInInspector] public uint Part3;
        [SerializeField, HideInInspector] public uint Part4;

        public SerializableGuid(uint part1, uint part2, uint part3, uint part4)
        {
            Part1 = part1;
            Part2 = part2;
            Part3 = part3;
            Part4 = part4;
        }
        public SerializableGuid(Guid guid)
        {
            byte[] bytes = guid.ToByteArray();
            Part1 = BitConverter.ToUInt32(bytes, 0);//Chuyển đổi byte thành giá trị uint với vị trí bắt đầu là 0
            Part2 = BitConverter.ToUInt32(bytes, 4);//tương tự với vị trí bắt đầu là 4
            Part3 = BitConverter.ToUInt32(bytes, 8);//tương tự với vị trí bắt đầu là 8
            Part4 = BitConverter.ToUInt32(bytes, 12);//tương tự với vị trí bắt đầu là 12
        }

        public static SerializableGuid Empty => new(0, 0, 0, 0);

        public static SerializableGuid NewGuid() => Guid.NewGuid().ToSerializableGuid();

        public static SerializableGuid FromHexString(string hexString)
        {
            if (hexString.Length != 32)
            {
                return Empty;
            }

            uint part1 = Convert.ToUInt32(hexString.Substring(0, 8), 16);
            uint part2 = Convert.ToUInt32(hexString.Substring(8, 8), 16);
            uint part3 = Convert.ToUInt32(hexString.Substring(16, 8), 16);
            uint part4 = Convert.ToUInt32(hexString.Substring(24, 8), 16);
            return new SerializableGuid(part1, part2, part3, part4);
        }

        public string ToHexString()
        {
            return $"{Part1:X8}{Part2:X8}{Part3:X8}{Part4:X8}";
        }

        public Guid ToGuid()
        {
            var bytes = new byte[16];
            BitConverter.GetBytes(Part1).CopyTo(bytes, 0);
            BitConverter.GetBytes(Part2).CopyTo(bytes, 4);
            BitConverter.GetBytes(Part3).CopyTo(bytes, 8);
            BitConverter.GetBytes(Part4).CopyTo(bytes, 12);
            return new Guid(bytes);
        }
        //***************************************************************************************************************************************************
        // Dòng này định nghĩa một toán tử chuyển đổi ngầm định từ kiểu SerializableGuid sang kiểu Guid.
        // Khi bạn sử dụng toán tử chuyển đổi này, nó sẽ tự động gọi phương thức ToGuid() của đối tượng serializableGuidguid để chuyển đổi nó thành một Guid.
        // Kết quả trả về là một đối tượng Guid.
        public static implicit operator Guid(SerializableGuid serializableGuidguid) => serializableGuidguid.ToGuid();
        //***************************************************************************************************************************************************
        // Dòng này định nghĩa một toán tử chuyển đổi ngầm định từ kiểu Guid sang kiểu SerializableGuid.
        // Khi bạn sử dụng toán tử chuyển đổi này, nó sẽ tạo một đối tượng SerializableGuid mới bằng cách sử dụng guid đã cho.
        // Kết quả trả về là một đối tượng SerializableGuid mới được tạo từ guid.
        public static implicit operator SerializableGuid(Guid guid) => new SerializableGuid(guid);
        //***************************************************************************************************************************************************
        public override bool Equals(object obj)
        {
            return obj is SerializableGuid guid && this.Equals(guid);
        }

        public bool Equals(SerializableGuid other)
        {
            return Part1 == other.Part1 && Part2 == other.Part2 && Part3 == other.Part3 && Part4 == other.Part4;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Part1, Part2, Part3, Part4);
        }

        public static bool operator ==(SerializableGuid left, SerializableGuid right) => left.Equals(right);
        public static bool operator !=(SerializableGuid left, SerializableGuid right) => !left.Equals(right);
    }
}
