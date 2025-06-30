using System.Collections.Generic;

namespace LC.DataTypes
{
    public class DebugIR
    {
        public int IndexLine { get; set; }
        public int IndexFile { get; set; }
        public List<DebugIRField> Fields { get; set; }
        public DebugIR()
        {
            IndexLine = -1;
            IndexFile = -1;
            Fields = new();
        }
        public void Null()
        {
            Fields = null;
        }
    }
    public class DebugIRField
    {
        public int IndexLine { get; set; }
        public DebugIRField()
        {
            IndexLine = -1;
        }
    }
}