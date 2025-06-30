using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LC.DataTypes;

namespace LC 
{
    public class BlocksCodeLexer
	{
		//ref int state, ref Token t, ref Token next, int posIndex, int lineIndex, string s, int byte1, Lexer l
		public LC.DataTypes.LexerOut aa(Lexer l)
		{
			return new() { EndToken = true, NextToken = false, NextTokenLong = false };
		}
        /*---------------------------------------------------------*/
		public LC.DataTypes.LexerOut ByteS0(Lexer l)
		{
			l.Token = new()
			{
				Type = TokenType.Byte,
				ValueByte = l.Byte,
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = false, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut OneQuoteS0(Lexer l)
		{
			l.State = 1;
			l.Token.Type = TokenType.OneQuote;
            l.Token.UValue = new((U)l.Byte);
			l.Token.IndexStartLine = l.LineIndex;
			l.Token.IndexStartPos = l.PosIndex;
			return new() { EndToken = false, NextToken = false, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut TwoQuoteS0(Lexer l)
		{
			l.State = 2;
			l.Token.Type = TokenType.TwoQuote;
            l.Token.UValue = new((U)l.Byte);
			l.Token.IndexStartLine = l.LineIndex;
			l.Token.IndexStartPos = l.PosIndex;
			return new() { EndToken = false, NextToken = false, NextTokenLong = false };
		}
		public LC.DataTypes.LexerOut NumS0(Lexer l)
		{
			l.State = 3;
			l.Token.Type = TokenType.Number;
            l.Token.UValue = new((U)l.Byte);
			l.Token.IndexStartLine = l.LineIndex;
			l.Token.IndexStartPos = l.PosIndex;
			return new() { EndToken = false, NextToken = false, NextTokenLong = false };
		}
		public LC.DataTypes.LexerOut NumCharS0(Lexer l)
		{
			l.Token = new()
			{
				Type = TokenType.Number,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = false, NextTokenLong = false };
		}
		public LC.DataTypes.LexerOut WordS0(Lexer l)
		{
			l.State = 4;
			l.Token.Type = TokenType.Word;
            l.Token.UValue = new((U)l.Byte);
			l.Token.IndexStartLine = l.LineIndex;
			l.Token.IndexStartPos = l.PosIndex;
			return new() { EndToken = false, NextToken = false, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut WordCharS0(Lexer l)
		{
			l.Token = new()
			{
				Type = TokenType.Word,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = false, NextTokenLong = false };
		}
		public LC.DataTypes.LexerOut CRS0(Lexer l)
		{
			l.State = 5;
			l.Token.UValue = new(new U(13));
			l.Token.Type = TokenType.NewLine;
			l.Token.IndexStartLine = l.LineIndex;
			l.Token.IndexStartPos = l.PosIndex;
			return new() { EndToken = false, NextToken = false, NextTokenLong = false };
		}
		public LC.DataTypes.LexerOut LFS0(Lexer l)
		{
			l.Token = new()
			{
				Type = TokenType.NewLine,
				UValue = new(new U(10)),
				ValueByte = l.Byte,
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
            l.LineIndex++;
            l.PosIndex = -1;
			return new() { EndToken = true, NextToken = false, NextTokenLong = false };
		}
		public LC.DataTypes.LexerOut ShortTokenS0(Lexer l)
		{
			l.Token = new()
			{
				Type = (TokenType)l.Byte,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = false, NextTokenLong = false };
		}
        /*---------------------------------------------------------*/
		public LC.DataTypes.LexerOut OneQuoteS1(Lexer l)
		{
			l.State = 0;
            l.Token.UValue += (U)l.Byte;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			return new() { EndToken = true, NextToken = false, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut OneQuoteCharsS1(Lexer l)
		{
            l.Token.UValue += (U)l.Byte;
			return new() { EndToken = false, NextToken = false, NextTokenLong = false };
		}
        /*---------------------------------------------------------*/
		public LC.DataTypes.LexerOut TwoQuoteS2(Lexer l)
		{
			l.State = 0;
            l.Token.UValue += (U)l.Byte;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			return new() { EndToken = true, NextToken = false, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut TwoQuoteCharsS2(Lexer l)
		{
            l.Token.UValue += (U)l.Byte;
			return new() { EndToken = false, NextToken = false, NextTokenLong = false };
		}
        /*---------------------------------------------------------*/
		public LC.DataTypes.LexerOut ByteS3(Lexer l)
		{
            l.State = 0;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.Byte,
				ValueByte = l.Byte,
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut OneQuoteS3(Lexer l)
		{
            l.State = 1;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.OneQuote,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexStartPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = true };
		}
        public LC.DataTypes.LexerOut TwoQuoteS3(Lexer l)
		{
            l.State = 2;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.TwoQuote,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexStartPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = true };
		}
        public LC.DataTypes.LexerOut NumS3(Lexer l)
		{
            l.Token.UValue += (U)l.Byte;
			return new() { EndToken = false, NextToken = false, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut WordS3(Lexer l)
		{
            l.State = 4;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.Word,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexStartPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = true };
		}
        public LC.DataTypes.LexerOut WordCharS3(Lexer l)
		{
            l.State = 0;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.Word,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut ShortTokenS3(Lexer l)
		{
            l.State = 0;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = (TokenType)l.Byte,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut CRS3(Lexer l)
		{
            l.State = 5;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.NewLine,
				UValue = new(new U(13)),
				ValueByte = l.Byte,
				IndexStartLine = l.LineIndex,
				IndexStartPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = true };
		}
        public LC.DataTypes.LexerOut LFS3(Lexer l)
		{
            l.State = 0;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.NewLine,
				UValue = new(new U(10)),
				ValueByte = l.Byte,
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
            l.LineIndex++;
            l.PosIndex = -1;
			return new() { EndToken = true, NextToken = true, NextTokenLong = false };
		} 
        /*---------------------------------------------------------*/
		public LC.DataTypes.LexerOut ByteS4(Lexer l)
		{
            l.State = 0;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.Byte,
				ValueByte = l.Byte,
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut ShortTokenS4(Lexer l)
		{
            l.State = 0;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = (TokenType)l.Byte,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut OneQuoteS4(Lexer l)
		{
            l.State = 1;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.OneQuote,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexStartPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = true };
		}
        public LC.DataTypes.LexerOut TwoQuoteS4(Lexer l)
		{
            l.State = 2;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.TwoQuote,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexStartPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = true };
		}
        public LC.DataTypes.LexerOut NumS4(Lexer l)
		{
            l.State = 3;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.Number,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexStartPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = true };
		}
        public LC.DataTypes.LexerOut NumCharS4(Lexer l)
		{
            l.State = 0;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.Number,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut WordS4(Lexer l)
		{
            l.Token.UValue += (U)l.Byte;
			return new() { EndToken = false, NextToken = false, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut LFS4(Lexer l)
		{
            l.State = 0;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.NewLine,
				UValue = new(new U(10)),
				ValueByte = l.Byte,
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
            l.LineIndex++;
            l.PosIndex = -1;
			return new() { EndToken = true, NextToken = true, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut CRS4(Lexer l)
		{
            l.State = 5;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.NewLine,
				UValue = new(new U(13)),
				IndexStartLine = l.LineIndex,
				IndexStartPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = true };
		}
        /*---------------------------------------------------------*/
		public LC.DataTypes.LexerOut ByteS5(Lexer l)
		{
            l.State = 0;
            l.Token.Type = TokenType.Byte;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.Byte,
				ValueByte = l.Byte,
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut ShortTokenS5(Lexer l)
		{
            l.State = 0;
            l.Token.Type = TokenType.Byte;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = (TokenType)l.Byte,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut OneQuoteS5(Lexer l)
		{
            l.State = 1;
            l.Token.Type = TokenType.Byte;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.OneQuote,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexStartPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = true };
		}
        public LC.DataTypes.LexerOut TwoQuoteS5(Lexer l)
		{
            l.State = 2;
            l.Token.Type = TokenType.Byte;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.TwoQuote,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexStartPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = true };
		}
        public LC.DataTypes.LexerOut NumS5(Lexer l)
		{
            l.State = 3;
            l.Token.Type = TokenType.Byte;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.Number,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexStartPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = true };
		}
        public LC.DataTypes.LexerOut NumCharS5(Lexer l)
		{
            l.State = 0;
            l.Token.Type = TokenType.Byte;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.Number,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut WordS5(Lexer l)
		{
            l.State = 4;
            l.Token.Type = TokenType.Byte;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.Word,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexStartPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = true };
		}
        public LC.DataTypes.LexerOut WordCharS5(Lexer l)
		{
            l.State = 0;
            l.Token.Type = TokenType.Byte;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.Word,
				UValue = new((U)l.Byte),
				IndexStartLine = l.LineIndex,
				IndexEndLine = l.LineIndex,
				IndexStartPos = l.PosIndex,
				IndexEndPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut LFS5(Lexer l)
		{
            l.State = 0;
			l.Token.UValue += new U(10);
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
            l.LineIndex++;
            l.PosIndex = -1;
			return new() { EndToken = true, NextToken = false, NextTokenLong = false };
		}
        public LC.DataTypes.LexerOut CRS5(Lexer l)
		{
            l.Token.Type = TokenType.Byte;
			l.Token.IndexEndLine = l.LineIndex;
			l.Token.IndexEndPos = l.PosIndex;
			l.NextToken = new()
			{
				Type = TokenType.NewLine,
				UValue = new(new U(13)),
				IndexStartLine = l.LineIndex,
				IndexStartPos = l.PosIndex
			};
			return new() { EndToken = true, NextToken = true, NextTokenLong = true };
		}
        /*---------------------------------------------------------*/
	}
}