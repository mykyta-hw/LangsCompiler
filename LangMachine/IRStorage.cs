using LC.DataTypes;
using System.Collections.Generic;
namespace LC.LangMachine
{
    public class IRStorage
    {
        /*
        0-byte
        1-bool
        2-int
        3-string
        4-token
        5+ objects
        */
        public Tester tester = new();
        public void Log()
        {
            tester.Clear();
            tester.Log("IRStorage\n{");
            tester.TabLine();
            tester.Var(IndexArray, "IndexArray");
            tester.Dump(Array, "Array");
            tester.Dump(TableObjects.ToArray(), "TableObjects");
            tester.Dump(IndexesStartObjects.ToArray(), "IndexesStartObjects");
            tester.Log("++++++++++++++++++++++++++++++++++++++++++++++");
            tester.Var(IndexArrayArrays, "IndexArrayArrays");
            tester.Dump(ArrayArrays, "ArrayArrays");
            tester.Dump(TableObjectsArrays.ToArray(), "TableObjectsArrays");
            tester.Dump(CountElementsArrays.ToArray(), "CountElementsArrays");
            tester.Dump(IndexStartArrays.ToArray(), "IndexStartArrays");
            tester.Log("++++++++++++++++++++++++++++++++++++++++++++++");
            tester.Dump(Strings.ToArray(), "Strings");
            tester.Dump(Tokens.ToArray(), "Tokens");
            tester.UnTabLine();
            tester.Log("}");
        }

        private List<IRObject> Paterns;
        public IRStorage(ref List<IRObject> a) { Paterns = a; }

        private byte[] Array = new byte[1024];
        private int IndexArray = 0;
        private List<int> TableObjects = new();
        private List<int> IndexesStartObjects = new();

        public int AddObject(ref byte[] bytes, int indexPatern)
        {
            int SizeObject = Paterns[indexPatern].LengthBytes;

            CheckAndResize(SizeObject);

            IndexesStartObjects.Add(IndexArray);
            IndexArray += SizeObject;

            TableObjects.Add(indexPatern);
            AddBytes(SizeObject, ref bytes);

            return TableObjects.Count;
        }
        public void Write1Byte(byte data, int offset, int obj)
        {
            int addr = IndexesStartObjects[obj];
            Array[addr + offset] = data;
        }
        public byte Read1Byte(int offset, int obj)
        {
            int addr = IndexesStartObjects[obj];
            return Array[addr + offset];
        }
        public void Write4Bytes(int data, int offset, int obj)
        {
            int addr = IndexesStartObjects[obj];
            Array[addr + offset + 0] = (byte)(data >>> 24);
            Array[addr + offset + 1] = (byte)(data >>> 16);
            Array[addr + offset + 2] = (byte)(data >>> 8);
            Array[addr + offset + 3] = (byte)(data >>> 0);
        }
        public int Read4Bytes(int offset, int obj)
        {
            int addr = IndexesStartObjects[obj];
            int result = 0;
            result |= Array[addr + offset + 3];
            result = result << 8;
            result |= Array[addr + offset + 2];
            result = result << 8;
            result |= Array[addr + offset + 1];
            result = result << 8;
            result |= Array[addr + offset + 0];
            result = result << 8;
            return result;
        }
        public void RemoveLastObject()
        {
            if (TableObjects.Count == 0) return;
            IndexArray -= Paterns[TableObjects[TableObjects.Count - 1]].LengthBytes;
            TableObjects.RemoveAt(TableObjects.Count - 1);
            IndexesStartObjects.RemoveAt(IndexesStartObjects.Count - 1);
        }

        private void AddBytes(int SizeObject, ref byte[] bytes)
        {
            if (SizeObject < bytes.Length) return;
            for (int i = 0; i < bytes.Length; i++)
            {
                Array[IndexArray + i] = bytes[i];
            }
        }
        private void CheckAndResize(int SizeObject)
        {
            Start:
            if (IndexArray + SizeObject >= Array.Length)
            {
                ResizeObjects(Array.Length + 512);
                goto Start;
            }
        }
        private void ResizeObjects(int count)
        {
            byte[] array = new byte[count];
            int a = (count < Array.Length) ? count : Array.Length;
            for (int i = 0; i < a; i++)
            {
                array[i] = Array[i];
            }
            Array = array;
        }


        private byte[] ArrayArrays = new byte[1024];
        private int IndexArrayArrays = 0;
        private List<int> TableObjectsArrays = new();
        private List<int> CountElementsArrays = new();
        private List<int> IndexStartArrays = new();

        public int AddArray(ref byte[] bytes, int indexPatern, int count)
        {
            int SizeObject = 4;

            if (indexPatern == 0 || indexPatern == 1) { SizeObject = 1; }

            int SizeArray = SizeObject * count;

            CheckAndResizeArrays(SizeArray);

            IndexStartArrays.Add(IndexArrayArrays);
            IndexArrayArrays += SizeArray;

            TableObjectsArrays.Add(indexPatern);
            CountElementsArrays.Add(count);

            AddBytesArrays(SizeArray, ref bytes);
            return TableObjectsArrays.Count;
        }
        public void RemoveLastArray()
        {
            if (TableObjectsArrays.Count == 0) return;
            IndexArrayArrays -= CountElementsArrays[CountElementsArrays.Count - 1] * Paterns[TableObjectsArrays[TableObjectsArrays.Count - 1]].LengthBytes;
            TableObjectsArrays.RemoveAt(TableObjectsArrays.Count - 1);
            CountElementsArrays.RemoveAt(CountElementsArrays.Count - 1);
            IndexStartArrays.RemoveAt(IndexStartArrays.Count - 1);
        }
        private void AddBytesArrays(int SizeArray, ref byte[] bytes)
        {
            if (SizeArray < bytes.Length) return;
            for (int i = 0; i < bytes.Length; i++)
            {
                ArrayArrays[IndexArrayArrays + i] = bytes[i];
            }
        }
        private void CheckAndResizeArrays(int SizeArray)
        {
            Start:
            if (IndexArray + SizeArray >= ArrayArrays.Length)
            {
                ResizeArrays(ArrayArrays.Length + 512);
                goto Start;
            }
        }
        private void ResizeArrays(int count)
        {
            byte[] array = new byte[count];
            int a = (count < ArrayArrays.Length) ? count : ArrayArrays.Length;
            for (int i = 0; i < a; i++)
            {
                array[i] = ArrayArrays[i];
            }
            ArrayArrays = array;
        }

        private List<Word> Strings = new();
        public int AddString(Word s)
        {
            Strings.Add(s);
            return Strings.Count - 1;
        }

        private List<Token> Tokens = new();
        public int AddToken(Token s)
        {
            Tokens.Add(s);
            return Tokens.Count - 1;
        }
    }
}