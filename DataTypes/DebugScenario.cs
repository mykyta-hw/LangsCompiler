namespace LC.DataTypes
{
    public class DebugScenario
    {
        public Word Name { get; set; }
        public List<GroupFilesDebug> GroupsFilesDebug { get; set; }
        public List<GroupLangsDebug> GroupsLangsDebug { get; set; }
        public List<CallGroupDebug> CallsGroupDebug { get; set; }
        public List<CallScenarioDebug> CallScenariosDebug { get; set; }
        public List<bool> isCallGroup { get; set; }
        public List<Word> Inputs { get; set; }
        public List<Word> Outputs { get; set; }
        public DebugScenario()
        {
            Name = new();
            GroupsFilesDebug = new();
            GroupsLangsDebug = new();
            CallsGroupDebug = new();
            CallScenariosDebug = new();
            isCallGroup = new();
            Inputs = new();
            Outputs = new();
        }
        public override string ToString()
        {
            string s = "DebugScenario: " + (string)Name + "\n";
            s += "Inputs: ";
            Inputs.ForEach(x => { s += (string)x + ", "; });
            s += "\nOutputs: ";
            Outputs.ForEach(x => { s += (string)x + ", "; });
            return s;
        }
        public void Null()
        {
            isCallGroup = null;
            Inputs = null;
            Outputs = null;
            if (GroupsFilesDebug != null) {
                for (int i = 0; i < GroupsFilesDebug.Count; i++) {
                    GroupsFilesDebug[i].Null();
                    GroupsFilesDebug[i] = null;
                }
                GroupsFilesDebug = null;
            }
            if (GroupsLangsDebug != null) {
                for (int i = 0; i < GroupsLangsDebug.Count; i++) {
                    GroupsLangsDebug[i].Null();
                    GroupsLangsDebug[i] = null;
                }
                GroupsLangsDebug = null;
            }
            if (CallScenariosDebug != null) {
                for (int i = 0; i < CallScenariosDebug.Count; i++) {
                    CallScenariosDebug[i].Null();
                    CallScenariosDebug[i] = null;
                }
                CallScenariosDebug = null;
            }
        }
    }
    public class CallScenarioDebug
    {
        public Word Name { get; set; }
        public int IndexLine { get; set; }
        public Word GroupOut { get; set; }
        public List<Word> Inputs { get; set; }
        public CallScenarioDebug()
        {
            Name = new();
            GroupOut = new();
            Inputs = new();
        }
        public override string ToString()
        {
            string s = "CallScenarioDebug: " + (string)Name + ", GroupOut = " + (string)GroupOut + ", Inputs = ";
            Inputs.ForEach(x=>{s+=(string)x + ", ";});
            s += "IndexLine = " + IndexLine;
            return s;
        }
        public void Null()
        {
            Inputs = null;
        }
    }
    public class CallGroupDebug
    {
        public Word Name { get; set; }
        public int IndexLine { get; set; }
        public CallGroupDebug()
        {
            Name = new();
        }
        public override string ToString()
        {
            return "CallGroupDebug: " + (string)Name + " " + "IndexLine = " + IndexLine;
        }
    }
    public class GroupFilesDebug
    {
        public Word Name { get; set; }
        public int IndexLine { get; set; }
        public List<Path> Paths { get; set; }
        public List<int> IndexLines { get; set; }
        public GroupFilesDebug()
        {
            Name = new();
            Paths = new();
            IndexLines = new();
        }
        public override string ToString()
        {
            string s = "GroupFilesDebug: " + (string)Name + ", " + "IndexLine = " + IndexLine + ", ";
            for (int i = 0; i < Paths.Count; i++)
            {
                s += "[" + Paths[i].ToString() + "\nLineIndex = " + IndexLines[i] + "]";
            }
            return s;
        }
        public void Null()
        {
            Paths = null;
            IndexLines = null;
        }
    }
    public class GroupLangsDebug
    {
        public Word Name { get; set;  }
        public int IndexLine { get; set;  }
        public List<LangDebug> Langs { get; set; }
        public GroupLangsDebug()
        {
            Name = new();
            Langs = new();
        }
        public override string ToString()
        {
            return "GroupLangsDebug: " + (string)Name + " " + "IndexLine = " + IndexLine;
        }
        public void Null()
        {
            if (Langs != null)
            {
                for (int i = 0; i < Langs.Count; i++)
                {
                    Langs[i].Null();
                    Langs[i] = null;
                }
                Langs = null;
            }
        }
    }
    public class LangDebug
    {
        public Word Name { get; set;  }
        public int IndexLine { get; set;  }
        public LangInputsDebug Input { get; set; } //1 inputs
        public LangOutsDebug Output { get; set; }  //1 outs
        public LangDebug()
        {
            Name = new();
            Input = new();
            Output = new();
        }
        public override string ToString()
        {
            return "Lang: " + (string)Name + " " + "IndexLine = " + IndexLine;
        }
        public void Null()
        {
            if (Input != null)
            {
                Input.Null();
                Input = null;
            }
            if (Output != null)
            {
                Output.Null();
                Output = null;
            }
        }
    }
    public class LangInputsDebug
    {
        public List<Word> GroupsFiles { get; set; }
        public int IndexLine { get; set; }
        public LangInputsDebug()
        {
            GroupsFiles = new();
        }
        public override string ToString()
        {
            string s = "Input: ";
            GroupsFiles.ForEach(x=>{s+=(string)x + ", ";});
            s += "IndexLine = " + IndexLine;
            return s;
        }
        public void Null()
        {
            GroupsFiles = null;
        }
    }
    public class LangOutsDebug
    {
        public List<Word> GroupsFiles { get; set; }
        public List<Word> LangFormats { get; set; }
        public int IndexLine { get; set; }
        public LangOutsDebug()
        {
            GroupsFiles = new();
            LangFormats = new();
        }
        public override string ToString()
        {
            string s = "Output: ";
            for (int i = 0; i < GroupsFiles.Count; i++)
            {
                s += "[" + (string)LangFormats[i] + ":" + (string)GroupsFiles[i] + "]";
            }
            s += " IndexLine = " + IndexLine;
            return s;
        }
        public void Null()
        {
            GroupsFiles = null;
            LangFormats = null;
        }
    }
}