namespace LC.DataTypes
{
    public class Token
	{
		public Token ()
		{
			Type = TokenType.Null;
			Value = "";
			UValue = new();
			ValueByte = -1;
			IndexStartLine = -1;
			IndexStartPos = -1;
			IndexEndLine = -1;
			IndexEndPos = -1;
		}
		public TokenType Type {get; set;}
		public string Value {get; set;}
		public DataTypes.Word UValue {get; set;}
		public int ValueByte {get;set;}
		public int IndexStartLine {get; set;}
		public int IndexStartPos {get; set;}
		public int IndexEndLine {get; set;}
		public int IndexEndPos {get; set;}
		public static string LineToString(System.Collections.Generic.List<Token> line)
		{
			string s = "";
			line.ForEach(x => {s+=(string)x.UValue + ", ";});
			return s;
		}
		public override string ToString()
		{
			string s = "";
			s += "Type: " + Type;
			s += ", Value: " + Value;
			s += ", UValue: " + (string)UValue;
			s += ", Value Byte: " + ValueByte;
			s += ", IndexStartLine: " + IndexStartLine;
			s += ", IndexEndLine: " + IndexEndLine;
			s += ", IndexStartPos: " + IndexStartPos;
			s += ", IndexEndPos: " + IndexEndPos;
			return s;
		}
	}
	public enum TokenType
	{
		Null = 0,
		Number,
		NewLine,
		Byte,
		Word,
		Tab = 9,
		Space = 32,
		Excl = 33,
		TwoQuote = 34,
		Hashtag = 35,
		Dolar = 36,
		Percent = 37,
		And = 38,
		OneQuote = 39,
		OpenParentheses = 40,
		CloseParentheses = 41,
		Asterisk = 42,
		Plus = 43,
		Comma = 44,
		Minus = 45,
		Dot = 46,
		ForwrdSlash = 47,
		Colon = 58,
		Semicolon = 59,
		a60 = 60,
		Equal = 61,
		a62 = 62,
		Quest = 63,
		Dog = 64,
		OpenBrackets = 91,
		BacwrdSlash = 92,
		CloseBrackets = 93,
		a94 = 94,
		UnderScore = 95,
		a96 = 96,
		OpenBraces = 123,
		Pipe = 124,
		CloseBraces = 125,
		a126 = 126,
		Unicode, 

		CloseRoundBrackets,
		OpenSquareBrackets,
		CloseSquareBrackets,
		OpenRoundBrackets,
		OpenFigureBrackets,
		CloseFigureBrackets,
		Operator,
		TwoDot 
	}
}