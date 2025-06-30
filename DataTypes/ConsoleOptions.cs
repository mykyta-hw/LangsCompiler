namespace LC.DataTypes
{
    public class ConsoleOptions
    {
        public bool CalledWithUI { get; set; }
        public CompileType Mode { get; set; }
        public string PathToProject { get; set; }
        public string PathToOutFolder { get; set; }
        public string[] PathsToLangFolders { get; set; }
        public string PathToCacheFolder { get; set; }
        public ConsoleOptions()
        {
            CalledWithUI = false;
            Mode = CompileType.Build;
            PathToProject = "";
            PathToOutFolder = "";
            PathsToLangFolders = new string[0];
            PathToCacheFolder = "";
        }
        public override string ToString()
        {
            return "ConsoleOptions [CalledWithUI:" + CalledWithUI +
            "\n, Mode:CompileType." + Mode + 
            "\n, PathToProject.Length:" + PathToProject.Length +
            "\n, PathsToLangFolders.Length:" + PathsToLangFolders.Length +
            "\n, PathToCacheFolder.Length:" + PathToCacheFolder.Length;
        }
        public void Null()
        {
            PathToProject = null;
            PathToOutFolder = null;
            PathsToLangFolders = null;
            PathToCacheFolder = null;
        }
    }
    public enum CompileType 
    {
        Build,
        Hell
    }
}