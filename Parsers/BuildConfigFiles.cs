using LC.DataTypes;
using LC.LangMachine;
using System.Collections.Generic;
namespace LC.Parsers
{
    public class BuildConfigFiles : Tester
    {
        private GroupFiles SourceCode = null;
        private List<IRObject> IRObjects = new();
        private IRStorage Storage;

        private DataTypes.SyntaxRules.Root SyntaxRules = new();
        private SyntaxWorker SyWorker;

        private bool Stop = false;
        private int IndexFile = 0;
        private FileCode CurrentFile;
        private List<int> RootObjectsIndexes = new();

        public BuildConfigFiles()
        {
            Storage = new(ref IRObjects);
            SyWorker = new(ref SyntaxRules, ref IRObjects, ref Storage);
        }

        public void Parse(ref GroupFiles fs, ref bool stop)
        {
            SourceCode = fs;
            for (; IndexFile < fs.Files.Count; IndexFile++)
            {
                Reset();
                CurrentFile = fs.Files[IndexFile];
                Var(CurrentFile.Info.FullName, "CurrentFile.Info.FullName");
                Syntax();
				ErrorsHandler.Call(new()
				{
					Sender = Sender.BuildConfigParser,
					Type = TypeMassage.Info,
					LinesMassage = Lines
				});
            }
            Semantic();
            if (Stop) { stop = true; return; }
            GenerateData();
        }
        private void Syntax()
        {
            SyWorker.DoFile(ref CurrentFile);
            RootObjectsIndexes.Add(SyWorker.IndexRootObject);
        }
        private void Semantic()
        {}
        private void GenerateData()
        {}
        private void Reset()
        {
        }
        /*public void ParseAndSavePath()
        {
            // /a/a/ // ./a/a/ // /a.a // ./a.a // /*.a // /*.* // /**.a  // /**.*
            DataTypes.Path path = new();
            List<Token> End = new();
            if (Path.Count > 0) {
                if (Path[0].Type == TokenType.Dot) {
                    path.isRelativePath = true;
                }
                if (Path[Path.Count - 1].Type == TokenType.ForwrdSlash || Path[Path.Count - 1].Type == TokenType.BacwrdSlash) {
                    path.isFolder = true;
                }
            }
            for ( int i = Path.Count - 1; i >= 0; i--) {
                if (Path[i].Type == TokenType.ForwrdSlash || Path[i].Type == TokenType.BacwrdSlash) { 
                    for (int j = path.isRelativePath ? 1 : 0; j <= i; j++) {
                        path.FullPath += Path[j].UValue;
                    }
                    break;
                }
                else { End.Add(Path[i]); }
            }
            if (path.isFolder) { GroupFilesDebug.Paths.Add(path); return; }

            int IndexDot = -1;
            for ( int i = End.Count - 1; i >= 0; i--) {
                if (End[i].Type == TokenType.Dot) { IndexDot = i; break; }
            }

            if (End.Count > 0) {
                if (End[End.Count - 1].Type == TokenType.Asterisk) { path.AllNames = true; }
            }
            if (End.Count > 1) {
                if (End[End.Count - 2].Type == TokenType.Asterisk) { path.InAllFolders = true; }
            }
            if (End.Count > 0 && IndexDot != -1) {
                if (End[0].Type == TokenType.Asterisk) { path.AllExt = true; }
            }

            if (!path.AllNames) {
                for ( int i = End.Count - 1; i > IndexDot; i--) {
                    path.Name += End[i].UValue;
                }
            }
            if (!path.AllExt) {
                for ( int i = IndexDot; i >= 0; i--) {
                    if (End[i].Type == TokenType.Dot) { continue; }
                    path.Ext += End[i].UValue;
                }
            }
            GroupFilesDebug.Paths.Add(path);
            GroupFilesDebug.IndexLines.Add(t.IndexStartLine);
        }*/
        private void Error(params string[] s)
        {
			string ss = "";
			for (int i = 0; i < s.Length; i++)
			{
				ss += Lang.Key(s[i]);
			}
            ErrorsHandler.Call(new()
            {
                Sender = Sender.BuildConfigParser,
                Type = TypeMassage.Error,
				Path = SourceCode.Files[IndexFile].Info.FullName,
                //LinesIndexes = new int[] { t.IndexStartLine },
                LinesMassage = new() { ss }
            });
            Stop = true;
        }
        private void Error(params string[][] s)
        {
			List<string> ss = new();
			for (int i = 0; i < s.Length; i++)
			{
                string[] sss = s[i];
                string l = "";
                for (int j = 0; j < sss.Length; j++)
                {
                    l += Lang.Key(sss[j]);
                }
                ss.Add(l);
			}
            ErrorsHandler.Call(new()
            {
                Sender = Sender.BuildConfigParser,
                Type = TypeMassage.Error,
				Path = SourceCode.Files[IndexFile].Info.FullName,
                //LinesIndexes = new int[] { t.IndexStartLine },
                LinesMassage = ss
            });
            Stop = true;
        }
    }
}