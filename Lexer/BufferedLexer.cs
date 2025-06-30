using System.Collections.Generic;
using LC.DataTypes;

namespace LC
{
	public sealed class BufferedLexer
    {
        public Tester tester = new();
        public void Log()
        {
            tester.Clear();
            tester.Log("BufferedLexer\n{");
            tester.TabLine();
            tester.Var(R, "R");
            tester.Var(W, "W");
            tester.Var(Count, "Count");
            tester.Dump(CacheTokens, "CacheTokens");
            tester.Dump(IndexesTokens.ToArray(), "IndexesTokens");
            tester.Dump(PositionsByte.ToArray(), "PositionsByte");
            tester.UnTabLine();
            tester.Log("}");
        }
        public int BufferSize { get; private set; }
        private Lexer Lex;
        private Token[] CacheTokens;
        private Stack<int> IndexesTokens = new();
        private Stack<long> PositionsByte = new();

        private int R = 0; //PointerRead
        private int W = 0; //PointerWrite
        private int Count = 0;

        public BufferedLexer(ref Lexer lx, int BufferSize = 128)
        {
            Lex = lx;
            this.BufferSize = BufferSize;
            CacheTokens = new Token[BufferSize];
        }
        public Token GetToken(ref FileCode f, out bool stop)
        {
            Token t;
            if (R == W) 
            {
                t = f.GetNextToken(ref Lex, out stop);
                if (W == BufferSize) { W = 0; R = 0; }
                CacheTokens[W] = t;
                Count++;
                W++;
                R++;
                return t;
            }
            else 
            {
                t = CacheTokens[R];
                if (R == BufferSize - 1) { R = 0; } else { R++; }
                stop = false;
                return t;
            }
        }
        public void SavePosition(ref FileCode f)
        {
            Token t;
            if (Count != 0)
            {
                IndexesTokens.Push(R - 1);
                t = CacheTokens[R - 1];
                if (t != null) 
                {
                    PositionsByte.Push(f.Position); 
                }
            }
        }
        public void ReturnToLastPosition(ref FileCode f)
        {
            if (IndexesTokens.Count != 0)
            {
                int a = IndexesTokens.Pop();
                long b = PositionsByte.Pop();
                if (Count > BufferSize)
                {
                    Lex.Reset();
                    f.Position = b;
                    Count = 0;
                    R = 0;
                    W = 0;
                }
                else 
                {
                    R = a;
                }
            }
        }
        public void Reset()
        {
            Lex.Reset();
            CacheTokens = new Token[BufferSize];
            IndexesTokens = new();
            PositionsByte = new();
            R = 0;
            W = 0;
            Count = 0;
        }
        public void Clear()
        {
            CacheTokens = null;
        }
    }
}

/*
            o
- - - - - - a b f 1 2 3 4 5 6 7 8 8
-
+

r 0
w 0
*/