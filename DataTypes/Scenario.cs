namespace LC.DataTypes
{
    public class Scenario
    {
        public Word Name { get; set; }
        public List<GroupFilesScenario> GroupsFiles { get; set; }
        public List<GroupLangsScenario> GroupLangs { get; set; }
        public List<ScenarioCall> ScenarioCalls { get; set; }
        public List<int> IndexesCall { get; set; }
        public List<bool> IsCallGroupLang { get; set; }
        public List<int> IndexesInputGroupsFiles { get; set; }
        public List<int> IndexesOutputGroupsFiles { get; set; }
        public Scenario()
        {
            GroupsFiles = new();
            GroupLangs = new();
            ScenarioCalls = new();
            Name = new();
            IndexesInputGroupsFiles = new();
            IndexesOutputGroupsFiles = new();
            IndexesCall = new();
            IsCallGroupLang = new();
        }
    }
    public class ScenarioCall
    {
        public List<int> InputIndexesGroupFiles { get; set; }
        public int OutputGroupFilesIndex { get; set; } 
        public int ScenarioIndex { get; set; }
        public ScenarioCall()
        {
            InputIndexesGroupFiles = new();
            OutputGroupFilesIndex = -1;
            ScenarioIndex = -1;
        }
    }
    public class GroupFilesScenario
    {
        public Word Name { get; set; }
        public List<DataTypes.Path> Paths { get; set; }
        public GroupFilesScenario()
        {
            Name = new();
            Paths = new();
        }
    }
    public class GroupLangsScenario
    {
        public Word Name { get; set;  }
        public List<int> IndexesLangs { get; set; }  //1 lang
        public List<LangInputsScenario> Inputs { get; set; } //1 inputs
        public List<LangOutsScenario> Outputs { get; set; }  //1 outs
        public GroupLangsScenario()
        {
            Name = new();
            IndexesLangs = new();
            Inputs = new();
            Outputs = new();
        }
    }
    public class LangInputsScenario
    {
        public List<int> IndexesGroupFiles { get; set; }
        public LangInputsScenario()
        {
            IndexesGroupFiles = new();
        }
    }
    public class LangOutsScenario
    {
        public List<int> IndexesGroupFiles { get; set; }
        public List<int> IndexesLangFormat { get; set; }
        public LangOutsScenario()
        {
            IndexesGroupFiles = new();
            IndexesLangFormat = new();
        }
    }
    public class Path
    {
        public bool isFolder { get; set; }
        public Word FullPath { get; set; }
        public bool isRelativePath { get; set; }
        public Word Ext { get; set; }
        public bool AllExt { get; set; }
        public Word Name { get; set; }
        public bool AllNames { get; set; }
        public bool InAllFolders { get; set; }
        public bool isCorrect { get; set; }
        public bool NotExt { get; set; }
        public Path()
        {
            FullPath = new();
            Ext = new();
            Name = new();
        }
        public override string ToString()
        {
            return "Path: isFolder = " + isFolder + ",\n" +
            "FullPath = " + (string)FullPath + ",\n" +
            "isRelativePath = " + isRelativePath + ",\n" +
            "Ext = " + (string)Ext + ",\n" +
            "AllExt = " + AllExt + ",\n" + 
            "Name = " + (string)Name + ",\n" +
            "AllNames = " + AllNames + ",\n" +
            "InAllFolders = " + InAllFolders + ",\n" +
            "isCorrect = " + isCorrect + ",\n" +
            "NotExt = " + NotExt + "";
        }
    }
}