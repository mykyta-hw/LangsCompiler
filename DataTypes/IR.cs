using System.Collections.Generic;

namespace LC.DataTypes
{
    public class IRObject
    {
        public Word FullName { get; set; }
        public int LengthBytes { get; set; }
        public List<Field> Fields { get; set; }
        public IRObject()
        {
            Fields = new();
            FullName = new();
            LengthBytes = 0;
        }
    }
    public class Field 
    {
        public Word Name { get; set; }
        public Word Type { get; set; }
        public int LengthBytes { get; set; }
        public int OffsetBytes { get; set; }
        public Field()
        {
            Name = new();
            Type = new();
            LengthBytes = 0;
            OffsetBytes = 0;
        }
    }
    public enum IRFieldType
    {
        Number,
        String,
        Bool
    }
}