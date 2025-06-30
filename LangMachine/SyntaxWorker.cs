using LC.DataTypes;
using LC.DataTypes.SyntaxRules;
using System.Collections.Generic;
namespace LC.LangMachine
{
    public class SyntaxWorker
    {
        public Tester tester = new();
        public void Log()
        {
            tester.Clear();
            tester.Log("SyntaxWorker\n{");
            tester.TabLine();
            tester.Var(t, "t");
            tester.Var(Stop, "Stop");
            tester.Var(StopCode, "StopCode");
            tester.Var(PointerInts, "PointerInts");
            tester.Var(PointerData, "PointerData");
            tester.EmpetyLine();
            tester.Var(IfFlag, "IfFlag");
            tester.Var(EqualTypeHistory, "EqualTypeHistory");
            tester.Var(EqualIndexHistory, "EqualIndexHistory");
            tester.Dump(EqualsTypeHistory.ToArray(), "EqualsTypeHistory");
            tester.Dump(EqualsIndexHistory.ToArray(), "EqualsIndexHistory");
            tester.EmpetyLine();
            tester.Dump(StackIRObjects.ToArray(), "StackIRObjects");
            tester.Var(CurrentObjectAddress, "CurrentObjectAddress");
            tester.EmpetyLine();
            tester.Dump(Array.ToArray(), "Array");
            tester.Var(IRTypeIndex, "IRTypeIndex");
            tester.Var(LengthArray, "LengthArray");
            tester.UnTabLine();
            tester.Log("}");
            Storage.Log();
            tester.Lines.AddRange(blx.tester.Lines);
            blx.Log();
            tester.Lines.AddRange(blx.tester.Lines);
        }

        private List<IRObject> Objects;
        private Root Data;
        private IRStorage Storage;

        private delegate void Int();
        private Int[] Ints;

        private List<byte> InstructionsLink;
        private List<int> DataIntsLink;
        private List<Word> LiteralsLink;
        private List<TokenType> TypesLink;
        private List<byte> BytesLink;

        private FileCode File;
        private Token t;
        private Lexer lx = new();
        private BufferedLexer blx = null;
        private bool Stop = false;
        private int StopCode = 0;    //for debug

        private int PointerInts = 0;
        private int PointerData = 0;

        private bool IfFlag = false;
        private byte EqualTypeHistory = 0;
        private int EqualIndexHistory = 0;
        private List<byte> EqualsTypeHistory = new();
        private List<int> EqualsIndexHistory = new();

        private Stack<int> StackIRObjects = new();
        private int CurrentObjectAddress = 0;

        private List<byte> Array = new();
        private int IRTypeIndex = 0;
        private int LengthArray = 0;

        public int IndexRootObject { get; private set; }
        
        public SyntaxWorker(ref Root root, ref List<IRObject> objects, ref IRStorage storage)
        {
            Objects = objects;
            Data = root;
            Storage = storage;
            InstructionsLink = Data.Instructions;
            DataIntsLink = Data.Data;
            LiteralsLink = Data.Literals;
            TypesLink = Data.Types;
            BytesLink = Data.Bytes;
            blx = new BufferedLexer(ref lx);
            InitIntsImplementation();
        }
        public void InitIntsImplementation()
        {
            Ints = new Int[31];
            Ints[0] = Jump;
            Ints[1] = JumpIfTrue;
            Ints[2] = JumpIfFalse;

            Ints[3] = LiteralEqual;
            Ints[4] = TypeEqual;
            Ints[5] = ByteEqual;
            Ints[6] = ResetEqual;
            
            Ints[7] = CreateObject;
            Ints[8] = WriteConsoleError;

            Ints[9] = SaveIntToVar;
            Ints[10] = SaveWordToVar;
            Ints[11] = SaveByteToVar;
            Ints[12] = SaveTokenToVar;
            Ints[13] = SaveObjectToVar;

            Ints[14] = PushObject;
            Ints[15] = PopObject;

            Ints[16] = SaveTokenPosition;
            Ints[17] = ReturnToLastTokenPosition;
            Ints[18] = SetDataPointer;
            Ints[19] = GetNextToken;

            Ints[20] = SaveLastEqual;
            Ints[21] = ClearEqualHistory;

            Ints[22] = CreateArray;
            Ints[23] = AddArrayToStorage;
            Ints[24] = AddToArrayByte;
            Ints[25] = AddToArrayInt;
            Ints[26] = AddToArrayWord;
            Ints[27] = AddToArrayToken;
            Ints[28] = AddToArrayObject;

            Ints[29] = StopInt;
            Ints[30] = SaveRootObject;
        }
        public void Reset()
        {
            blx.Reset();
            t = new();
            Stop = false;
            StopCode = 0;
            PointerInts = 0;
            PointerData = 0;
            IfFlag = false;
            EqualTypeHistory = 0;
            EqualIndexHistory = 0;
            EqualsTypeHistory = new();
            EqualsIndexHistory = new();
            StackIRObjects = new();
            CurrentObjectAddress = 0;
            Array = new();
            IRTypeIndex = 0;
            LengthArray = 0;
            GC.Collect();
        }
        public void DoFile(ref FileCode file)
        {
            Reset();
            File = file;
            File.StartStream();
            while (true)
            {
                Log();
                ErrorsHandler.Call(new()
                {
                    Sender = Sender.SyntaxRules,
                    Type = TypeMassage.Error,
                    Path = File.Info.FullName,
                    LinesMassage = tester.Lines
                });
                if (Stop) break;
                Ints[InstructionsLink[PointerInts]]();
            }
            File.CloseStream();
        }


        private void Jump()
        {
            PointerInts = DataIntsLink[PointerData];
            PointerData++;
        }
        private void JumpIfTrue()
        {
            if (IfFlag)
            {
                PointerInts = DataIntsLink[PointerData];
                PointerData++;
                IfFlag = false;
            }
        }
        private void JumpIfFalse()
        {
            if (!IfFlag)
            {
                PointerInts = DataIntsLink[PointerData];
                PointerData++;
            }
        }
        private void LiteralEqual()
        {
            if (t.Type == TokenType.Word)
            {
                if (t.UValue == LiteralsLink[DataIntsLink[PointerData]])
                {
                    EqualTypeHistory = 1;
                    EqualIndexHistory = DataIntsLink[PointerData];
                    IfFlag = true;
                    PointerData++;
                    PointerInts++;
                }
            }
            else 
            {
                Stop = true;
                StopCode = 1;
            }
        }
        private void TypeEqual()
        {
            if (t.Type == TypesLink[DataIntsLink[PointerData]])
            {
                EqualTypeHistory = 2;
                EqualIndexHistory = DataIntsLink[PointerData];
                IfFlag = true;
                PointerData++;
                PointerInts++;
            }
        }
        private void ByteEqual()
        {
            if (t.Type == TokenType.Byte)
            {
                if (t.ValueByte == (int)BytesLink[DataIntsLink[PointerData]])
                {
                    EqualTypeHistory = 3;
                    EqualIndexHistory = DataIntsLink[PointerData];
                    IfFlag = true;
                    PointerData++;
                    PointerInts++;
                }
            }
            else 
            {
                Stop = true;
                StopCode = 1;
            }
        }
        
        private void ResetEqual()
        {
            IfFlag = false;
            PointerInts++;
        }
        private void CreateObject()
        {
            int indexPatern = DataIntsLink[PointerData];
            byte[] data = new byte[Objects[indexPatern].LengthBytes];
            CurrentObjectAddress = Storage.AddObject(ref data, indexPatern);
            PointerData++;
            PointerInts++;
        }
        private void WriteConsoleError()
        {
            int a = 0; //DataIntsLink[PointerData];
            if (a == 0)
            {
                string s = "";
                for (int i = 0; i < EqualsTypeHistory.Count; i++)
                {
                    s += ", ";
                    if (EqualsTypeHistory[i] == 0) //literal
                    {
                        s += "\"" + (string)LiteralsLink[EqualsIndexHistory[i]] + "\"";
                        continue;
                    }
                    if (EqualsTypeHistory[i] == 1) //type
                    {
                        s += "\"" + TypesLink[EqualsIndexHistory[i]] + "\"";
                        continue;
                    }
                    if (EqualsTypeHistory[i] == 2) //byte
                    {
                        s += "\"" + BytesLink[EqualsIndexHistory[i]] + "\"";
                        continue;
                    }
                }
                ErrorsHandler.Call(new()
                {
                    Sender = Sender.SyntaxRules,
                    Type = TypeMassage.Error,
                    Path = File.Info.FullName,
                    LinesIndexes = new int[] { t.IndexStartLine },
                    LinesMassage = new()
                    {
                        Lang.Key("LC-Expected") + ": " + s,
                        Lang.Key("LC-Position-in-file: ") + t.IndexStartLine + " " + t.IndexStartPos
                    }
                });
            }
        }
        private void SaveIntToVar()
        {
            int res = 0;
            if (int.TryParse((string)t.UValue, out res))
            {
                int offset = DataIntsLink[PointerData];
                Storage.Write4Bytes(res, offset, CurrentObjectAddress);
                PointerData++;
                PointerInts++;
                return;
            }
            else 
            {
                //error
                Stop = true;
                ErrorsHandler.Call(new()
                {
                    Sender = Sender.SyntaxRules,
                    Type = TypeMassage.Error,
                    Path = File.Info.FullName,
                    LinesIndexes = new int[] { t.IndexStartLine },
                    LinesMassage = new()
                    {
                        Lang.Key("LC-Dont-parse-number-value-to-int."),
                        Lang.Key("LC-Position-in-file: ") + t.IndexStartLine + " " + t.IndexStartPos
                    }
                });
            }
        }
        private void SaveWordToVar()
        {
            Storage.Write4Bytes(Storage.AddString(t.UValue), DataIntsLink[PointerData], CurrentObjectAddress);
            PointerData++;
            PointerInts++;
        }
        private void SaveByteToVar()
        {
            Storage.Write1Byte((byte)t.ValueByte, DataIntsLink[PointerData], CurrentObjectAddress);
            PointerData++;
            PointerInts++;
        }
        private void SaveTokenToVar()
        {
            Storage.Write4Bytes(Storage.AddToken(t), DataIntsLink[PointerData], CurrentObjectAddress);
            PointerData++;
            PointerInts++;
        }
        private void SaveObjectToVar()
        {
            int obj = StackIRObjects.Pop();
            Storage.Write4Bytes(CurrentObjectAddress, DataIntsLink[PointerData], obj);
            StackIRObjects.Push(obj);
            PointerData++;
            PointerInts++;
        }
        private void PushObject()
        {
            StackIRObjects.Push(CurrentObjectAddress);
            PointerInts++;
        }
        private void PopObject()
        {
            if (StackIRObjects.Count == 0) return;
            CurrentObjectAddress = StackIRObjects.Pop();
            PointerInts++;
        }
        private void SaveTokenPosition()
        {
            blx.SavePosition(ref File);
            PointerInts++;
        }
        private void ReturnToLastTokenPosition()
        {
            blx.ReturnToLastPosition(ref File);
            PointerInts++;
        }
        private void SetDataPointer()
        {
            PointerData = DataIntsLink[PointerData];
            PointerInts++;
        }
        private void GetNextToken()
        {
            t = File.GetNextToken(ref lx, out Stop);
            PointerInts++;
        }
        private void SaveLastEqual()
        {
            EqualsTypeHistory.Add(EqualTypeHistory);
            EqualsIndexHistory.Add(EqualIndexHistory);
            PointerInts++;
        }
        private void ClearEqualHistory()
        {
            EqualsTypeHistory = new();
            EqualsIndexHistory = new();
            PointerInts++;
        }
        private void CreateArray()
        {
            Array = new();
            IRTypeIndex = DataIntsLink[PointerData];
            LengthArray = 0;
            PointerData++;
            PointerInts++;
        }
        private void AddArrayToStorage()
        {
            byte[] b = Array.ToArray();
            Storage.AddArray(ref b, IRTypeIndex, LengthArray);
            PointerInts++;
        }
        private void AddToArrayByte()
        {
            Array.Add((byte)t.ValueByte);
            LengthArray++;
            PointerInts++;
        }
        private void AddToArrayInt()
        {
            int res = 0;
            if (int.TryParse((string)t.UValue, out res))
            {
                Array.Add((byte)(res >>> 24));
                Array.Add((byte)(res >>> 16));
                Array.Add((byte)(res >>> 8));
                Array.Add((byte)(res >>> 0));
                LengthArray++;
                PointerInts++;
            }
            else 
            {
                //error
                Stop = true;
                ErrorsHandler.Call(new()
                {
                    Sender = Sender.SyntaxRules,
                    Type = TypeMassage.Error,
                    Path = File.Info.FullName,
                    LinesIndexes = new int[] { t.IndexStartLine },
                    LinesMassage = new()
                    {
                        Lang.Key("LC-Dont-parse-number-value-to-int."),
                        Lang.Key("LC-Position-in-file: ") + t.IndexStartLine + " " + t.IndexStartPos
                    }
                });
            }
        }
        private void AddToArrayWord()
        {
            int res = Storage.AddString(t.UValue);
            Array.Add((byte)(res >>> 24));
            Array.Add((byte)(res >>> 16));
            Array.Add((byte)(res >>> 8));
            Array.Add((byte)(res >>> 0));
            LengthArray++;
            PointerInts++;
        }
        private void AddToArrayToken()
        {
            int res = Storage.AddToken(t);
            Array.Add((byte)(res >>> 24));
            Array.Add((byte)(res >>> 16));
            Array.Add((byte)(res >>> 8));
            Array.Add((byte)(res >>> 0));
            LengthArray++;
            PointerInts++;
        }
        private void AddToArrayObject()
        {
            Array.Add((byte)(CurrentObjectAddress >>> 24));
            Array.Add((byte)(CurrentObjectAddress >>> 16));
            Array.Add((byte)(CurrentObjectAddress >>> 8));
            Array.Add((byte)(CurrentObjectAddress >>> 0));
            LengthArray++;
            PointerInts++;
        }
        private void StopInt()
        {
            Stop = true;
            if (StopCode == 0) {}
        }
        private void SaveRootObject()
        {
            IndexRootObject = CurrentObjectAddress;
            PointerInts++;
        }
    }
}