namespace LC.DataTypes
{
    public class PathsFiles
    {
        public System.Collections.Generic.List<string> LIFiles { get; set; }
        public System.Collections.Generic.List<string> BCFiles { get; set; }
        public System.Collections.Generic.List<string> SyFiles { get; set; }
        public System.Collections.Generic.List<string> SeFiles { get; set; }
        public System.Collections.Generic.List<string> IRFiles { get; set; }
        public System.Collections.Generic.List<string> CGFiles { get; set; }

        public PathsFiles()
        {
            LIFiles = new();
            BCFiles = new();
            SyFiles = new();
            SeFiles = new();
            IRFiles = new();
            CGFiles = new();
        }
    }
}