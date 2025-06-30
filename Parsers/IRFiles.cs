using LC.DataTypes;
using static LC.DataTypes.IRParserStates;

namespace LC.Parsers
{
    public class IRFiles
    {
        private int c = 0;
        private Lexer LexerObj = new();
        private List<DebugIR>[] DebugInfoIRsLangs = new List<DebugIR>[ProjectData.Langs.Count];
        private List<IRObject> eIRObjects = new();
        private List<DebugIR> eIRObjectsDebugInfo = new();
        private IRObject IRObject = new();
        private DebugIR IRObjectDebugInfo = new();
        private FileCode CurrentFile = null;
        private Token t = new();
        private bool End = false;
        private int State = -1;
        private int IndexFile = -1;
        private int IndexLang = -1;
        private Word NameLang = new();
        private Word NameNamespace = new();
        private Word NameObject = new();
        private Word TypeField = new();
        private Word NameField = new();
        private GroupFiles SourceFiles;
        public void Parse(ref GroupFiles sourceFiles, ref bool stop)
        {
            SourceFiles = sourceFiles;
            Reset();
            c = SourceFiles.Files.Count;

            for (int i = 0; i < DebugInfoIRsLangs.Length; i++) { DebugInfoIRsLangs[i] = new(); }
            while (IndexFile < c - 1)
            {
                ResetState();
                IndexFile += 1;
                LexerObj.Reset();
                CurrentFile = SourceFiles.Files[IndexFile];
                //Info1(SourceFiles.Files[i].Info.FullName);
                CurrentFile.StartStream();

                while (true)
                {
                    if (End) { stop = true; CurrentFile.CloseStream(); return; }
                    t = CurrentFile.GetNextToken(ref LexerObj, out End);
                    if (End) { break; }
                    Start:
					if (State == START)         { if (t.Type == TokenType.Word)       { State = FIRST_WORD;                                                }   else { goto Error; } }
                    if (State == FIRST_WORD)    { if (t.UValue == KeyWords.lang)      { State = LANG_WORD;                                       continue; }   else { goto Error; } }
                    if (State == LANG_WORD)     { if (t.Type == TokenType.Space)      { State = LANG_SPACE;                                      continue; }   else { goto Error; } }
                    if (State == LANG_SPACE)    { if (t.Type == TokenType.NewLine)    { if (!FindSaveLangIndex()) {  goto Error; } State =  5;   continue; }
                                                  else                                {                          NameLang += t.UValue;           continue; }
                    }
                    if (State == DEFAULT)       { if (t.Type == TokenType.Word)       { State = SECOND_WORD;                                               }   else { goto Error; } }
                    if (State == SECOND_WORD)   { if (t.UValue == KeyWords.Namespace) { State = NAMESPACE;       NameNamespace = new();          continue; }   else { goto Error; } }
					if (State == NAMESPACE)     { if (t.Type == TokenType.Word)       { State =  4;              NameNamespace += t.UValue;      continue; } 
                                                  if (t.Type == TokenType.NewLine)    { State =  5;                                              continue; }
                                                  if (t.Type == TokenType.Space)      {                                                          continue; }   else { goto Error; }
                    }
                    if (State == 4)  { if (t.Type == TokenType.Dot)     { State =  NAMESPACE; NameNamespace += t.UValue;            continue; }
                                       if (t.Type == TokenType.NewLine) { State =  5;                                       continue; }   else { goto Error; } }

                    if (State == 5)  { if (t.Type == TokenType.Space)   { State =  6;                                       continue; } 
                                       if (t.Type == TokenType.Tab)     { State =  9;                                       continue; }
                                       if (t.Type == TokenType.NewLine) { State =  5;                                       continue; }
                                       if (t.Type == TokenType.Word)    { State = DEFAULT;                                       goto Start; } else { goto Error; }
                    }
                    if (State == 6)  { if (t.Type == TokenType.Space)   { State =  7;                                       continue; }
                                       if (t.Type == TokenType.NewLine) { State =  5;                                       continue; }
                                       if (t.Type == TokenType.Tab)     { State = 15;                                       continue; }   else { goto Error; }
                    }
                    if (State == 7)  { if (t.Type == TokenType.Space)   { State =  8;                                       continue; }
                                       if (t.Type == TokenType.NewLine) { State =  5;                                       continue; }
                                       if (t.Type == TokenType.Tab)     { State = 15;                                       continue; }   else { goto Error; }
                    }
                    if (State == 8)  { if (t.Type == TokenType.Space)   { State =  9;                                       continue; }
                                       if (t.Type == TokenType.NewLine) { State =  5;                                       continue; }
                                       if (t.Type == TokenType.Tab)     { State = 15;                                       continue; }   else { goto Error; }
                    }
                    if (State == 9)  { if (t.Type == TokenType.Space)   { State = 11;                                       continue; }
                                       if (t.Type == TokenType.Tab)     { State = 14;                                       continue; }
                                       if (t.Type == TokenType.NewLine) { State =  5;                                       continue; }
                                       if (t.Type == TokenType.Word)    { State = 10; NameObject = t.UValue;                continue; }   else { goto Error; }
                    }
                    if (State == 10) { if (t.Type == TokenType.Space)   {                                                   continue; }
                                       if (t.Type == TokenType.NewLine) { State =  5; CreateIRObject();AddIRObjectToList(); continue; }   else { goto Error; }
                    }
                    if (State == 11) { if (t.Type == TokenType.Space)   { State = 12;                                       continue; }
                                       if (t.Type == TokenType.NewLine) { State =  5;                                       continue; }
                                       if (t.Type == TokenType.Tab)     { State = 15;                                       continue; }   else { goto Error; }
                    }
                    if (State == 12) { if (t.Type == TokenType.Space)   { State = 13;                                       continue; }
                                       if (t.Type == TokenType.NewLine) { State =  5;                                       continue; }
                                       if (t.Type == TokenType.Tab)     { State = 15;                                       continue; }   else { goto Error; }
                    }
                    if (State == 13) { if (t.Type == TokenType.Space)   { State = 14;                                       continue; }
                                       if (t.Type == TokenType.NewLine) { State =  5;                                       continue; }
                                       if (t.Type == TokenType.Tab)     { State = 15;                                       continue; }   else { goto Error; }
                    }
                    if (State == 14) { if (t.Type == TokenType.NewLine) { State =  5;                                       continue; }
                                       if (t.Type == TokenType.Space)   { State = 15;                                       continue; }
                                       if (t.Type == TokenType.Tab)     { State = 15;                                       continue; }
                                       if (t.Type == TokenType.Word)    { State = 17; TypeField += t.UValue;                continue; }   else { goto Error; }
                    }
                    if (State == 15) { if (t.Type == TokenType.Tab)     {                                                   continue; }
                                       if (t.Type == TokenType.Space)   {                                                   continue; }
                                       if (t.Type == TokenType.NewLine) { State = 5;                                        continue; }   else { goto Error; }
                    }
                    if (State == 16) { if (t.Type == TokenType.Word)    { State = 17; TypeField += t.UValue;                continue; }   else { goto Error; } } 
                    if (State == 17) { if (t.Type == TokenType.Dot)     { State = 16; TypeField += t.UValue;                continue; }
                                       if (t.Type == TokenType.Word)    { State = 18; NameField  = t.UValue;                continue; }
                                       if (t.Type == TokenType.Space)   {                                                   continue; }   else { goto Error; }
                    }
                    if (State == 18) { if (t.Type == TokenType.NewLine) { State = 5; AddFieldToObject();                    continue; }
                                       if (t.Type == TokenType.Space)   {                                                   continue; }   else { goto Error; }
                    }
                }
                CurrentFile.CloseStream();
                continue;
                Error:
				if (State == START) { Error("LC-Expected-'lang'."); } //expected "lang"
                if (State == FIRST_WORD) { Error("LC-Expected-'lang'."); } //expected "lang"
                if (State == 0) { Error("LC-Expected-space."); }
                if (State == 1) { Error("LC-Expected-enter.", "LC-Or-lang-not-exists."); } //
                if (State == DEFAULT) { Error("LC-Expected-'namespace'."); } //expected "namespace"
                if (State == NAMESPACE) { Error("LC-Expected-word.", "LC-Or-enter."); }
                if (State == 4) { Error("LC-Expected-dot.", "LC-Or-enter."); }
                if (State == 5) { Error("LC-Unknown-token: ", t.ToString()); }
                if (State == 6) { Error("LC-Unknown-token: ", t.ToString()); }
                if (State == 7) { Error("LC-Unknown-token: ", t.ToString()); }
                if (State == 8) { Error("LC-Unknown-token: ", t.ToString()); }
                if (State == 9) { Error("LC-Expected-word.", "LC-Or-enter."); }
                if (State == 10) { Error("LC-Expected-enter."); }
                if (State == 11) { Error("LC-Unknown-token: ", t.ToString()); }
                if (State == 12) { Error("LC-Unknown-token: ", t.ToString()); }
                if (State == 13) { Error("LC-Unknown-token: ", t.ToString()); }
                if (State == 14) { Error("LC-Expected-word."); }
                if (State == 15) { Error("LC-Expected-enter."); }
                if (State == 16) { Error("LC-Expected-word."); }
                if (State == 17) { Error("LC-Expected-word.", "LC-Or-dot."); } //or dot
                if (State == 18) { Error("LC-Expected-enter."); }
                continue;
            }
            Semantic(ref stop);
            Info2();
            return;
        }
        public void Semantic(ref bool stop)
        {
            for (int IndexLang = 0; IndexLang < ProjectData.Langs.Count; IndexLang++)
            {
                bool good = false;
                int CountIRObjects = ProjectData.Langs[IndexLang].IRObjects.Count;
                /*string LengthBytesProp = ProjectData.Langs[IndexLang].Info.GetProperty("LengthBytesLinkIRObjects");
                string LengthBytesStrProp = ProjectData.Langs[IndexLang].Info.GetProperty("LengthBytesIRString");
                int LengthBytesObj = 3;
                int LengthBytesStr = 3;
                try 
                {
                    if (LengthBytesProp.Length > 0) { LengthBytesObj = int.Parse(LengthBytesProp); }
                }
                catch (Exception e)
                {
                    ErrorsHandler.Call(new()
                    {
                        Sender = Sender.IRParser,
                        Type = TypeMassage.Error,
                        LinesMassage = new()
                        {
                            Lang.Key("LC-Lang-property-not-correct."),
                            "LengthBytesLinkIRObjects",
                            Lang.Key("LC-Lang: ") + ProjectData.Langs[IndexLang].Info.Name,
                            e.ToString()
                        }
                    });
                }
                try 
                {
                    if (LengthBytesStrProp.Length > 0) { LengthBytesStr = int.Parse(LengthBytesStrProp); }
                }
                catch (Exception e)
                {
                    ErrorsHandler.Call(new()
                    {
                        Sender = Sender.IRParser,
                        Type = TypeMassage.Error,
                        LinesMassage = new()
                        {
                            Lang.Key("LC-Lang-property-not-correct."),
                            "LengthBytesIRString",
                            Lang.Key("LC-Lang: ") + ProjectData.Langs[IndexLang].Info.Name,
                            e.ToString()
                        }
                    });
                }*/
                for (int IndexIRObject = 0; IndexIRObject < CountIRObjects; IndexIRObject++)
                {
                    int FieldsCount = ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].Fields.Count;
                    for (int IndexField = 0; IndexField < FieldsCount; IndexField++)
                    {
						Word nf = ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].Fields[IndexField].Type;
						if (nf == KeyWords.BoolL || nf == KeyWords.BoolH || nf == KeyWords.ByteL || nf == KeyWords.ByteH)
						{
							good = true;
							ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].Fields[IndexField].OffsetBytes = ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].Fields[IndexField].LengthBytes;
							ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].LengthBytes += 1;
							ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].Fields[IndexField].LengthBytes = 1;
							goto EndCheckFields;
						}
						else if (nf == KeyWords.IntL || nf == KeyWords.IntH)
						{
							good = true;
							ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].Fields[IndexField].OffsetBytes = ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].Fields[IndexField].LengthBytes;
							ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].LengthBytes += 4;
							ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].Fields[IndexField].LengthBytes = 4;
							goto EndCheckFields;
						}
						else if (nf == KeyWords.StringL || nf == KeyWords.StringH)
						{
							good = true;
							ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].Fields[IndexField].OffsetBytes = ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].Fields[IndexField].LengthBytes;
							ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].LengthBytes += 4;
							ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].Fields[IndexField].LengthBytes = 4;
							goto EndCheckFields;
						}
                        for (int j = 0; j < CountIRObjects; j++)
                        {
                            if (ProjectData.Langs[IndexLang].IRObjects[j].FullName == nf)
                            {
                                good = true;
                                ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].Fields[IndexField].OffsetBytes = ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].Fields[IndexField].LengthBytes;
                                ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].LengthBytes += 4;//LengthBytesObj;
                                ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].Fields[IndexField].LengthBytes = 4;//LengthBytesObj;
                                break;
                            }
                        }
						EndCheckFields:
                        if (!good)
                        {
                            Error13((string)ProjectData.Langs[IndexLang].IRObjects[IndexIRObject].Fields[IndexField].Type, DebugInfoIRsLangs[IndexLang][IndexIRObject].Fields[IndexField].IndexLine);
                            stop = true;
                            return;
                        }
                        else { good = false; continue; }
                    }
                }
            }
        }
        private void Reset()
        {
            DebugInfoIRsLangs = new List<DebugIR>[ProjectData.Langs.Count];
            IRObject = new();
            IRObjectDebugInfo = new();
            CurrentFile = null;
            t = new();
            End = false;
            IndexFile = -1;
            State = -1;
            IndexLang = -1;
            NameLang = new();
            NameNamespace = new();
            NameObject = new();
            TypeField = new();
            NameField = new();
        }
        private void ResetState()
        {
            IRObject = new();
            IRObjectDebugInfo = new();
            CurrentFile = null;
            t = new();
            End = false;
            //IndexFile = -1;
            State = -1;
            IndexLang = -1;
            NameLang = new();
            NameNamespace = new();
            NameObject = new();
            TypeField = new();
            NameField = new();
        }
        private void AddIRObjectToList()
        {
            FindExistsIRObject();
            ProjectData.Langs[IndexLang].IRObjects.Add(IRObject);
            DebugInfoIRsLangs[IndexLang].Add(IRObjectDebugInfo);
        }
        private void CreateIRObject()
        {
            IRObject = new() { FullName = NameNamespace + new Word(new U(46)) + NameObject };
            IRObjectDebugInfo = new()  { IndexLine = t.IndexStartLine, IndexFile = this.IndexFile };
        }
        private void AddFieldToObject() 
        {
            for (int i = 0; i < IRObject.Fields.Count; i++) {
                if (IRObject.Fields[i].Name == NameField)
                {
                    End = true;
                    Error12((string)NameField, t.IndexStartLine, (string)IRObject.Fields[i].Name, IRObjectDebugInfo.Fields[i].IndexLine);
                    return;
                }
            }
            IRObject.Fields.Add(new Field() { Type = TypeField, Name = NameField });
            IRObjectDebugInfo.Fields.Add(new() { IndexLine = t.IndexStartLine });
            TypeField = new();
            NameField = new();
        }
        private bool FindSaveLangIndex()
        {
            for (int j = 0; j < ProjectData.Langs.Count; j++)
            {
                if (ProjectData.Langs[j].Info.Name == (string)NameLang)
                {
                    IndexLang = j;
                    for (int i = 0; i < 5; i++)
                    {
                        ProjectData.Langs[IndexLang].IRObjects.Add(null);
                        DebugInfoIRsLangs[IndexLang].Add(null);
                    }
                    return true;
                }
            }
			return false;
        }
        private void FindExistsIRObject()
        {
            for (int j = 0; j < ProjectData.Langs[IndexLang].IRObjects.Count; j++)
            {
                if (ProjectData.Langs[IndexLang].IRObjects[j].FullName == IRObject.FullName) {
                    Error11(j);
                    End = true;
                    continue;
                }
            }
        }
        private void Error(params string[] s)
        {
			List<string> ss = new();
            ss.Add("State: " + State);
			for (int i = 0; i < s.Length; i++)
			{
				ss.Add(Lang.Key(s[i]));
			}
            ErrorsHandler.Call(new()
            {
                Sender = Sender.IRParser,
                Type = TypeMassage.Error,
				Path = SourceFiles.Files[IndexFile].Info.FullName,
                LinesIndexes = new int[] { t.IndexStartLine },
                LinesMassage = new(ss)
            });
            CurrentFile.CloseStream();
        }
        private void Error11(int IndexObject)
        {
            ErrorsHandler.Call(new()
            {
                Sender = Sender.IRParser,
                Type = TypeMassage.Error,
                LinesMassage = new()
                {
                    Lang.Key("LC-Object-already-exists-in-lang."),
                    Lang.Key("LC-First-line: "),
                    "(" + CurrentFile.Info.FullName + ")",
                    IRObjectDebugInfo.IndexLine + " " + (string)IRObject.FullName,
                    Lang.Key("LC-Second-line: "),
                    "(" + SourceFiles.Files[DebugInfoIRsLangs[IndexLang][IndexObject].IndexFile].Info.FullName + ")",
                    DebugInfoIRsLangs[IndexLang][IndexObject].IndexLine + " " + (string)ProjectData.Langs[IndexLang].IRObjects[IndexObject].FullName
                }
            });
        }
        private void Error12(string NameField1, int IndexLine1, string NameField2, int IndexLine2)
        {
            ErrorsHandler.Call(new()
            {
                Sender = Sender.IRParser,
                Type = TypeMassage.Error,
                LinesMassage = new()
                {
                    Lang.Key("LC-Field-already-exists-in-objects."),//\\\\
                    Lang.Key("LC-First-line: "),
                    IndexLine1 + " " + NameField1,
                    Lang.Key("LC-Second-line: "),
                    IndexLine2 + " " + NameField2
                }
            });
        }
        private void Error13(string TypeField, int IndexLine)
        {
            ErrorsHandler.Call(new()
            {
                Sender = Sender.IRParser,
                Type = TypeMassage.Error,
                LinesIndexes = new int[] { IndexLine + 1 },
                LinesMassage = new()
                {
                    Lang.Key("LC-Field-type-not-exists-in-lang."),
                    TypeField,
                }
            });
        }
        private void Info1(string path)
        {
            ErrorsHandler.Call(new()
            {
                Sender = Sender.IRParser,
                Type = TypeMassage.Info,
                LinesMassage = new()
                {
                    Lang.Key("LC-Start-parsing-file: ") + "(" + path + ")",
                }
            });
        }
        private void Info2()
        {
            List<string> ll = new();
            for (int i = 0; i < ProjectData.Langs.Count; i++)
            {
                ll.Add(Lang.Key("LC-Count-objects: ") + ProjectData.Langs[i].IRObjects.Count + ", " + Lang.Key("LC-Lang: ") + (string)ProjectData.Langs[i].Info.Name);
            }
            ErrorsHandler.Call(new()
            {
                Sender = Sender.IRParser,
                Type = TypeMassage.Info,
                LinesMassage = ll
            });
        }
        private void Info3(List<IRObject> ir, int il)
        {
            List<string> ll = new();
            ll.Add((string)ProjectData.Langs[il].Info.Name);
            for (int i = 0; i < ir.Count; i++)
            {
                ll.Add("IR object:" + (string)ir[i].FullName + " Fields.Count: " + ir[i].Fields.Count);
                for (int j = 0; j < ir[i].Fields.Count; j++)
                {
                    ll.Add("Field Type:" + (string)ir[i].Fields[j].Type + " Name:" + (string)ir[i].Fields[j].Name);
                }
            }
            ErrorsHandler.Call(new()
            {
                Sender = Sender.IRParser,
                Type = TypeMassage.Info,
                LinesMassage = ll
            });
        }
    }
}