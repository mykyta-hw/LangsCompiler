using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LC.DataTypes;

namespace LC
{
	public sealed class Lexer
	{ 
		private bool wordLine = true;
		private bool numberLine = true;
		private bool oneQuoteLine = true;
		private bool twoQuoteLine = true;
		private bool newLine = true;
		private bool numberAsWordAfterWord = true;
		private bool oneQuoteAsWordAfterWord = true;
		private bool twoQuoteAsWordAfterWord = true;
		private bool charAsWordAfterWord = true;

		public delegate LC.DataTypes.LexerOut BlockCode(Lexer l);

		private StateCode[] StatesCode = new StateCode[6];
		BlocksCodeLexer Blocks = new();

		///State fields
		public int Byte = 0;
		public int LineIndex = 0;
		public int PosIndex = 0;
		public int State = 0;
		public int StateUnicode = 0;
		public Token NextToken = new();
		public Token Token = new();
		public int[] Bytes = new int[4];
		public int CountBytes = 0;
		public bool NewU = false;
		public int ByteNewU = 0;
		public DataTypes.U U = new();
		public LC.DataTypes.LexerOut Lo = new();
		public Token Prew = new();

		public void Reset()
		{
			Byte = 0;
			LineIndex = 0;
			PosIndex = 0;
			State = 0;
			StateUnicode = 0;
			CountBytes = 0;
			NextToken = new();
			Token = new();
			Bytes = new int[4];
			NewU = false;
			ByteNewU = 0;
			U = new();
			Lo = new();
			Prew = new();
		}
		public Lexer() { InitCode(); }
		public Lexer(bool wordLine, bool numberLine, bool oneQuoteLine, bool twoQuoteLine, bool newLine, bool numberAsWordAfterWord, bool oneQuoteAsWordAfterWord, bool twoQuoteAsWordAfterWord, bool charAsWordAfterWord)
		{
			this.wordLine = wordLine;
			this.numberLine = numberLine;
			this.oneQuoteLine = oneQuoteLine;
			this.twoQuoteLine = twoQuoteLine;
			this.newLine = newLine;
			this.numberAsWordAfterWord = numberAsWordAfterWord;
			this.oneQuoteAsWordAfterWord = oneQuoteAsWordAfterWord;
			this.twoQuoteAsWordAfterWord = twoQuoteAsWordAfterWord;
			this.charAsWordAfterWord = charAsWordAfterWord;
			InitCode();
		}
		private void InitCode()
		{
			for (int j = 0; j < 6; j++)
			{
				StateCode ss = new();
				if (j == 0) { InitCodeS0(ref ss); }
				if (j == 1) { InitCodeS1(ref ss); }
				if (j == 2) { InitCodeS2(ref ss); }
				if (j == 3) { InitCodeS3(ref ss); }
				if (j == 4) { InitCodeS4(ref ss); }
				if (j == 5) { InitCodeS5(ref ss); }
				StatesCode[j] = ss;
			}
		}
		private void InitCodeS0(ref StateCode s)
		{
			for (int i = 0; i < 32; i++)
			{
				s.Code[i] = Blocks.ByteS0;
			}
			////
			for (int i = 32; i < 48; i++)
			{
				s.Code[i] = Blocks.ShortTokenS0;
			}
			for (int i = 58; i < 65; i++)
			{
				s.Code[i] = Blocks.ShortTokenS0;
			}
			for (int i = 91; i < 97; i++)
			{
				s.Code[i] = Blocks.ShortTokenS0;
			}
			for (int i = 123; i < 127; i++)
			{
				s.Code[i] = Blocks.ShortTokenS0;
			}
			s.Code[9] = Blocks.ShortTokenS0;
			////
			if (oneQuoteLine)
			{
				s.Code[39] = Blocks.OneQuoteS0;
			}
			else 
			{
				s.Code[39] = Blocks.ShortTokenS0;
			}
			////
			if (twoQuoteLine)
			{
				s.Code[34] = Blocks.TwoQuoteS0;
			}
			else 
			{
				s.Code[34] = Blocks.ShortTokenS0;
			}
			////
			if (numberLine)
			{
				for (int i = 48; i < 58; i++)
				{
					s.Code[i] = Blocks.NumS0;
				}
			}
			else 
			{
				for (int i = 48; i < 58; i++)
				{
					s.Code[i] = Blocks.NumCharS0;
				}
			}
			////
			if (wordLine)
			{
				for (int i = 65; i < 91; i++)
				{
					s.Code[i] = Blocks.WordS0;
				}
				for (int i = 97; i < 123; i++)
				{
					s.Code[i] = Blocks.WordS0;
				}
			}
			else 
			{
				for (int i = 65; i < 91; i++)
				{
					s.Code[i] = Blocks.WordCharS0;
				}
				for (int i = 97; i < 123; i++)
				{
					s.Code[i] = Blocks.WordCharS0;
				}
			}
			////
			if (newLine)
			{
				s.Code[10] = Blocks.LFS0;
				s.Code[13] = Blocks.CRS0;
			}
			////
			for (int i = 127; i < 256; i++)
			{
				s.Code[i] = Blocks.ByteS0;
			}
		}
		private void InitCodeS1(ref StateCode s)
		{
			for (int i = 0; i < 256; i++)
			{
				s.Code[i] = Blocks.OneQuoteCharsS1;
			}
			s.Code[39] = Blocks.OneQuoteS1;
		}
		private void InitCodeS2(ref StateCode s)
		{
			for (int i = 0; i < 256; i++)
			{
				s.Code[i] = Blocks.TwoQuoteCharsS2;
			}
			s.Code[34] = Blocks.TwoQuoteS2;
		}
		private void InitCodeS3(ref StateCode s)
		{
			for (int i = 0; i < 32; i++)
			{
				s.Code[i] = Blocks.ByteS3;
			}
			////
			for (int i = 32; i < 48; i++)
			{
				s.Code[i] = Blocks.ShortTokenS3;
			}
			for (int i = 58; i < 65; i++)
			{
				s.Code[i] = Blocks.ShortTokenS3;
			}
			for (int i = 91; i < 97; i++)
			{
				s.Code[i] = Blocks.ShortTokenS3;
			}
			for (int i = 123; i < 127; i++)
			{
				s.Code[i] = Blocks.ShortTokenS3;
			}
			s.Code[9] = Blocks.ShortTokenS3;
			////
			if (oneQuoteLine)
			{
				s.Code[39] = Blocks.OneQuoteS3;
			}
			else 
			{
				s.Code[39] = Blocks.ShortTokenS3;
			}
			////
			if (twoQuoteLine)
			{
				s.Code[34] = Blocks.TwoQuoteS0;
			}
			else 
			{
				s.Code[34] = Blocks.ShortTokenS3;
			}
			////
			for (int i = 48; i < 58; i++)
			{
				s.Code[i] = Blocks.NumS3;
			}
			////
			if (wordLine)
			{
				for (int i = 65; i < 91; i++)
				{
					s.Code[i] = Blocks.WordS3;
				}
				for (int i = 97; i < 123; i++)
				{
					s.Code[i] = Blocks.WordS3;
				}
			}
			else 
			{
				for (int i = 65; i < 91; i++)
				{
					s.Code[i] = Blocks.WordCharS3;
				}
				for (int i = 97; i < 123; i++)
				{
					s.Code[i] = Blocks.WordCharS3;
				}
			}
			////
			if (newLine)
			{
				s.Code[10] = Blocks.LFS3;
				s.Code[13] = Blocks.CRS3;
			}
			////
			for (int i = 127; i < 256; i++)
			{
				s.Code[i] = Blocks.ByteS3;
			}
		}
		private void InitCodeS4(ref StateCode s)
		{
			for (int i = 0; i < 32; i++)
			{
				s.Code[i] = Blocks.ByteS4;
			}
			////
			for (int i = 32; i < 48; i++)
			{
				s.Code[i] = Blocks.ShortTokenS4;
			}
			for (int i = 58; i < 65; i++)
			{
				s.Code[i] = Blocks.ShortTokenS4;
			}
			for (int i = 91; i < 97; i++)
			{
				s.Code[i] = Blocks.ShortTokenS4;
			}
			for (int i = 123; i < 127; i++)
			{
				s.Code[i] = Blocks.ShortTokenS4;
			}
			s.Code[9] = Blocks.ShortTokenS4;
			////
			if (oneQuoteAsWordAfterWord)
			{
				s.Code[39] = Blocks.WordS4;
			}
			else 
			{
				if (oneQuoteLine)
				{
					s.Code[39] = Blocks.OneQuoteS4;
				}
				else 
				{
					s.Code[39] = Blocks.ShortTokenS4;
				}
			}
			////
			if (twoQuoteAsWordAfterWord)
			{
				s.Code[34] = Blocks.WordS4;
			}
			else
			{
				if (twoQuoteLine)
				{
					s.Code[34] = Blocks.TwoQuoteS4;
				}
				else 
				{
					s.Code[34] = Blocks.ShortTokenS4;
				}
			}
			
			////
			if (numberAsWordAfterWord)
			{
				for (int i = 48; i < 58; i++)
				{
					s.Code[i] = Blocks.WordS4;
				}
			}
			else
			{
				if (numberLine)
				{
					for (int i = 48; i < 58; i++)
					{
						s.Code[i] = Blocks.NumS4;
					}
				}
				else 
				{
					for (int i = 48; i < 58; i++)
					{
						s.Code[i] = Blocks.NumCharS4;
					}
				}
			}
			////
			for (int i = 65; i < 91; i++)
			{
				s.Code[i] = Blocks.WordS4;
			}
			for (int i = 97; i < 123; i++)
			{
				s.Code[i] = Blocks.WordS4;
			}
			////
			if (newLine)
			{
				s.Code[10] = Blocks.LFS4;
				s.Code[13] = Blocks.CRS4;
			}
			////
			for (int i = 127; i < 256; i++)
			{
				s.Code[i] = Blocks.ByteS4;
			}
		}
		private void InitCodeS5(ref StateCode s)
		{
			for (int i = 0; i < 32; i++)
			{
				s.Code[i] = Blocks.ByteS5;
			}
			////
			for (int i = 32; i < 48; i++)
			{
				s.Code[i] = Blocks.ShortTokenS5;
			}
			for (int i = 58; i < 65; i++)
			{
				s.Code[i] = Blocks.ShortTokenS5;
			}
			for (int i = 91; i < 97; i++)
			{
				s.Code[i] = Blocks.ShortTokenS5;
			}
			for (int i = 123; i < 127; i++)
			{
				s.Code[i] = Blocks.ShortTokenS5;
			}
			s.Code[9] = Blocks.ShortTokenS5;
			////
			if (oneQuoteLine)
			{
				s.Code[39] = Blocks.OneQuoteS5;
			}
			else 
			{
				s.Code[39] = Blocks.ShortTokenS5;
			}
			////
			if (twoQuoteLine)
			{
				s.Code[34] = Blocks.TwoQuoteS5;
			}
			else 
			{
				s.Code[34] = Blocks.ShortTokenS5;
			}
			////
			if (numberLine)
			{
				for (int i = 48; i < 58; i++)
				{
					s.Code[i] = Blocks.NumS5;
				}
			}
			else 
			{
				for (int i = 48; i < 58; i++)
				{
					s.Code[i] = Blocks.NumCharS5;
				}
			}
			////
			if (wordLine)
			{
				for (int i = 65; i < 91; i++)
				{
					s.Code[i] = Blocks.WordS5;
				}
				for (int i = 97; i < 123; i++)
				{
					s.Code[i] = Blocks.WordS5;
				}
			}
			else 
			{
				for (int i = 65; i < 91; i++)
				{
					s.Code[i] = Blocks.WordCharS5;
				}
				for (int i = 97; i < 123; i++)
				{
					s.Code[i] = Blocks.WordCharS5;
				}
			}
			////
			if (newLine)
			{
				s.Code[10] = Blocks.LFS5;
				s.Code[13] = Blocks.CRS5;
			}
			////
			for (int i = 127; i < 256; i++)
			{
				s.Code[i] = Blocks.ByteS5;
			}
		}
		public LC.DataTypes.LexerOut MadeToken(int b)
		{
			Byte = b;
			goto Decode;
			Decode:
			{
				if (NewU) { Bytes[0] = ByteNewU; NewU = false; }
				if (StateUnicode == 0) 
				{
					if ((b & 0b10000000) == 0b00000000) { Bytes[0] = b; goto Extract0; }
					if ((b & 0b11100000) == 0b11000000) { Bytes[0] = b; StateUnicode = 1; CountBytes = 1; return new(); }
					if ((b & 0b11110000) == 0b11100000) { Bytes[0] = b; StateUnicode = 2; CountBytes = 1; return new(); }
					if ((b & 0b11111000) == 0b11110000) { Bytes[0] = b; StateUnicode = 4; CountBytes = 1; return new(); }
					else                                { Bytes[0] = b; goto Extract0; }
				}
				if (StateUnicode == 1)
				{
					if ((b & 0b11000000) == 0b10000000) { Bytes[1] = b; StateUnicode = 0; CountBytes = 2; goto Extract1; }
					else                                { Bytes[1] = b; StateUnicode = 0; CountBytes = 2; goto NewU; }
				}
				if (StateUnicode == 2)
				{
					if ((b & 0b11000000) == 0b10000000) { Bytes[1] = b; StateUnicode = 3; CountBytes = 2; return new(); }
					else                                { Bytes[1] = b; StateUnicode = 0; CountBytes = 2; goto NewU; }
				}
				if (StateUnicode == 3)
				{
					if ((b & 0b11000000) == 0b10000000) { Bytes[2] = b; StateUnicode = 0; CountBytes = 3; goto Extract2; }
					else                                { Bytes[2] = b; StateUnicode = 0; CountBytes = 3; goto NewU; }
				}
				if (StateUnicode == 4)
				{
					if ((b & 0b11000000) == 0b10000000) { Bytes[1] = b; StateUnicode = 5; CountBytes = 2; return new(); }
					else                                { Bytes[1] = b; StateUnicode = 0; CountBytes = 2; goto NewU; }
				}
				if (StateUnicode == 5)
				{
					if ((b & 0b11000000) == 0b10000000) { Bytes[2] = b; StateUnicode = 6; CountBytes = 3; return new(); }
					else                                { Bytes[2] = b; StateUnicode = 0; CountBytes = 3; goto NewU; }
				}
				if (StateUnicode == 6)
				{
					if ((b & 0b11000000) == 0b10000000) { Bytes[3] = b; StateUnicode = 0; CountBytes = 4; goto Extract3; } 
					else                                { Bytes[3] = b; StateUnicode = 0; CountBytes = 4; goto NewU; }
				}
				return new();
			}
			NewU:
			{
				Lo.CountBytes = (byte)CountBytes;
				Lo.NewU = true;
				if (State != 0)
				{
					Token.IndexEndLine = LineIndex;
					Token.IndexEndPos = PosIndex - CountBytes;
					Lo.PrewToken = true;
					Prew = Token;
					Token = new();
					State = 0;
				}
				if ((b & 0b10000000) == 0b00000000) { goto Extract; }
				if ((b & 0b11100000) == 0b11000000) { ByteNewU = b; NewU = true; StateUnicode = 1; CountBytes = 1; return Lo; }
				if ((b & 0b11110000) == 0b11100000) { ByteNewU = b; NewU = true; StateUnicode = 2; CountBytes = 1; return Lo; }
				if ((b & 0b11111000) == 0b11110000) { ByteNewU = b; NewU = true; StateUnicode = 4; CountBytes = 1; return Lo; }
				else                                { goto Extract; }
			}
			Extract:
			{
				var a = StatesCode[State].Code[Byte](this);
				PosIndex++;
				Lo.EndToken = a.EndToken;
				return Lo;
			}
			Extract0:
			{
				var a = StatesCode[State].Code[Byte](this);
				PosIndex++;
				return a;
			}
			Extract1:
			{
				U.A = (byte)((Bytes[1] & 63) | ((Bytes[0] & 3) << 6));
				U.B = (byte)((Bytes[0] & 28) >> 2);
				U.C = 0;
				if ((U.A | (U.B << 8)) < 0x80)
				{
					if (State != 0)
					{
						Token.IndexEndLine = LineIndex;
						Token.IndexEndPos = PosIndex - CountBytes;
						Prew = Token;
						Token = new();
						State = 0;
						return new() { NewU = true, CountBytes = 2, PrewToken = true };
					}
					return new() { NewU = true, CountBytes = 2, };
				}
				CountBytes = 0;
				PosIndex++;
				goto Build;
			}
			Extract2:
			{
				U.C = 0;
				U.A = (byte)(((Bytes[1] & 3) << 6) | (Bytes[2] & 63));
				U.B = (byte)(((Bytes[1] & 60) >> 2) | ((Bytes[0] & 15) << 4));
				if ((U.A | (U.B << 8)) < 0x800) 
				{
					if (State != 0)
					{
						Token.IndexEndLine = LineIndex;
						Token.IndexEndPos = PosIndex - CountBytes;
						Prew = Token;
						Token = new();
						State = 0;
						return new() { NewU = true, CountBytes = 3, PrewToken = true };
					}
					return new() { NewU = true, CountBytes = 3 };
				}
				CountBytes = 0;
				PosIndex++;
				goto Build;
			}
			Extract3:
			{
				U.A = (byte)(((Bytes[2] & 3) << 6) | (Bytes[3] & 63));
				U.B = (byte)(((Bytes[2] & 60) >> 2) | ((Bytes[1] & 15) << 4));
				U.C = (byte)(((Bytes[0] & 7) << 4) | ((Bytes[1] & 48) >> 4));
				if ((((U.A | (U.B << 8)) | (U.C << 16)) < 0x1000)/* || (((U.A | (U.B << 8)) | (U.C << 16)) > 0x10FFFF)*/) 
				{ 
					if (State != 0)
					{
						Token.IndexEndLine = LineIndex;
						Token.IndexEndPos = PosIndex - CountBytes;
						Prew = Token;
						Token = new();
						State = 0;
						return new() { NewU = true, CountBytes = 4, PrewToken = true };
					}
					return new() { NewU = true, CountBytes = 4 };
				}
				CountBytes = 0;
				PosIndex++;
				goto Build;
			}
			Build:
			{
				if (Token.Type == TokenType.Word)
				{
					Token.UValue += U;
					U = new();
					return new();
				}
				else if (Token.Type == TokenType.OneQuote)
				{
				    if (oneQuoteLine)
				    {
				        Token.UValue += U;
					    U = new();
					    return new();
				    }
				}
				else if (Token.Type == TokenType.TwoQuote)
				{
				    if (twoQuoteLine)
				    {
				        Token.UValue += U;
					    U = new();
					    return new();
				    }
				}
				else if (State == 0)
				{
					State = 4;
				    Token.Type = TokenType.Word;
				    Token.UValue += U;
				    Token.IndexStartLine = LineIndex;
				    Token.IndexStartPos = PosIndex;
				    return new();
				}
				else
				{
				    Token.IndexEndLine = LineIndex;
				    Token.IndexEndPos = PosIndex - 1;
				    NextToken.Type = TokenType.Word;
				    NextToken.UValue += U;
				    NextToken.IndexStartLine = LineIndex;
				    NextToken.IndexStartPos = PosIndex;
				    return new() { EndToken = true, NextToken = true, NextTokenLong = true };
				}
			}
			return Lo;
		}
	}
	public class StateCode 
	{
		public Lexer.BlockCode[] Code { get; set; }
		public StateCode()
		{
			Code = new Lexer.BlockCode[256];
		}
	}
}
