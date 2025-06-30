using System.Linq;
using LC;
using LC.DataTypes;
namespace LC.Parsers 
{
	public class Documentation
	{
		private Lexer lx = new();
		private FileCode f = new();
		private Token t = new();
		private int State = 0;
		private bool stop = false;

		private Word KeyPage = new();
		private List<List<Word>> PageKeysText = new();
		private List<List<int>> PageColors = new();
		private List<List<bool>> PageIsKeys = new();

		private List<Word> KeysTextLine = new();
		private List<int> ColorsLine = new();
		private List<bool> IsKeyLine = new();

		private int IndexColorInLine = 0;
		private Word Key = new();
		private string CurrentFilePath = "";
		public void Parse(LC.Documentation docs)
		{
			try 
			{
				string[] DocsFilesPaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.doc", SearchOption.AllDirectories);
				for (int i = 0; i < DocsFilesPaths.Length; i++)
				{
					CurrentFilePath = DocsFilesPaths[i];
					ParseFile(docs);
				}
			}
			catch (Exception e)
			{
				ErrorsHandler.Call(new()
				{
					Sender = Sender.DocumentationParser,
					Type = TypeMassage.Error,
					LinesMassage = new()
					{
						"Exception: ",
						e.Message
					}
				});
			}
		}
		private void ParseFile(LC.Documentation docs)
		{
			Reset();
			f.SetPath(CurrentFilePath);
			f.StartStream();
			while(true)
			{
				t = f.GetNextToken(ref lx, out stop);
				if (stop) { break; }
				UpdateState();
				if (stop) { break; }
			}
			AddPageToDocs(docs);
			f.CloseStream();
		}
		private void Reset()
		{
			lx.Reset();
			State = 0;
			stop = false;
			KeyPage = new();
			t = new();
			PageKeysText = new();
			PageColors = new();
			PageIsKeys = new();
			KeysTextLine = new();
			ColorsLine = new();
			IsKeyLine = new();
			IndexColorInLine = 0;
			Key = new();
		}
		public void AddPageToDocs(LC.Documentation docs)
		{
			docs.AddPage(new Page(PageKeysText, PageColors, PageIsKeys), (string)KeyPage);
		}
		private void AddLineToPage(List<Word> lineKeysWords, List<int> lineColors, List<bool> lineiskey)
		{
			PageKeysText.Add(lineKeysWords);
			PageColors.Add(lineColors);
			PageIsKeys.Add(lineiskey);
		}
		private void AddWordToLine(Word w, int c, bool iskey)
		{
			KeysTextLine.Add(w);
			ColorsLine.Add(c);
			IsKeyLine.Add(iskey);
		}
		private void UpdateState()
		{
			if (State == 0)//null
			{
				if (t.Type == TokenType.NewLine) { State = 1; return; }
				else { KeyPage += t.UValue; return; }
			}
			if (State == 1) //key-page [NewLine]    excepted NewLine|continue
			{
				if (t.Type == TokenType.Space) { return; }
				if (t.Type == TokenType.NewLine) { return; }
				else { State = 2; }
			}
			if (State == 2)//key-page [NewLine]   excepted element
			{
				if (t.Type == TokenType.Space) { return; }
				if (t.Type == TokenType.TwoQuote)
				{
					State = 3;
					AddWordToLine(t.UValue, 0, false);
					return;
				}
				if (t.Type == TokenType.BacwrdSlash)
				{
					State = 4;
					return;
				}
				if (t.Type == TokenType.Word)
				{
					if (t.UValue == KeyWords.NewLine)
					{
						AddWordToLine(new Word(new U(10)), 7, false);
						State = 3;
						return;
					}
				}
				else { Error(t.IndexStartLine, "LC-Unknown-token: " + t.ToString(), CurrentFilePath); stop = true; return; }
			}
			if (State == 3) //key-page [NewLine] element   excepted ,
			{
				if (t.Type == TokenType.Space) { return; }
				if (t.Type == TokenType.Comma)
				{
					State = 2;
					return;
				}
				if (t.Type == TokenType.OpenBrackets)
				{
					if (KeysTextLine.Count == 0) { Error(t.IndexStartLine, "LC-Number-of-keys-in-line-is-0.", CurrentFilePath); stop = true; return; }
					State = 5;
					return;
				}
				else { Error(t.IndexStartLine, "LC-Expected-comma.", CurrentFilePath); stop = true; return; }
			}
			if (State == 4) //key-page [NewLine] \   excepted key
			{
				if (t.Type == TokenType.NewLine) { Error(t.IndexStartLine, "LC-Enter-cant-use.", CurrentFilePath); stop = true; return; }
				if (t.Type == TokenType.BacwrdSlash)
				{
					State = 3;
					AddWordToLine(Key, 0, true);
					Key = new();
					return;
				}
				else { Key += t.UValue; return; }
			}
			if (State == 5) //key-page [NewLine] elements [   excepted ] for error
			{
				if (t.Type == TokenType.CloseBrackets) { Error(t.IndexStartLine, "LC-Empty-colors.", CurrentFilePath); stop = true; return; }
				else { State = 6; }
			}
			if (State == 6) //key-page [NewLine] elements [   excepted word|minus|]
			{
				if (t.Type == TokenType.Space) { return; }
				if (t.Type == TokenType.Word || t.Type == TokenType.Minus)
				{
					if (IndexColorInLine < ColorsLine.Count)
					{
						State = 7;
						ColorsLine[IndexColorInLine++] = CaseColor((string)t.UValue);
						return;
					}
					else { Error(t.IndexStartLine, "LC-Count-colors-not-equals-count-elements.", CurrentFilePath); stop = true; return; }
				}
				else { Error(t.IndexStartLine, "LC-Expected-word-or-minus.", CurrentFilePath); stop = true; return; }
			}
			if (State == 7) //key-page [NewLine] elements [word|minus   excepted ,|]
			{
				if (t.Type == TokenType.Space) { return; }
				if (t.Type == TokenType.Comma)
				{
					State = 6;
					return;
				}
				if (t.Type == TokenType.CloseBrackets)
				{
					State = 8;
					return;
				}
				else { Error(t.IndexStartLine, "LC-Expected-,-or-].", CurrentFilePath); stop = true; return; }
			}
			if (State == 8) //key-page [NewLine] elements [words]   expected newline
			{
				if (t.Type == TokenType.NewLine)
				{
					State = 1;
					AddLineToPage(KeysTextLine, ColorsLine, IsKeyLine);
					KeysTextLine = new();
					ColorsLine = new();
					IsKeyLine = new();
					IndexColorInLine = 0;
					return;
				}
				else { Error(t.IndexStartLine, "LC-Expected-enter.", CurrentFilePath); stop = true; return; }
			}
		}
		private int CaseColor(string s)
		{
			switch (s)
			{
				case "Black": return 0;
				case "DarkBlue": return 1;
				case "DarkGreen": return 2;
				case "DarkCyan": return 3;
				case "DarkRed": return 4;
				case "DarkMagenta": return 5;
				case "DarkYellow": return 6;
				case "Gray": return 7;
				case "DarkGray": return 8;
				case "Blue": return 9;
				case "Green": return 10;
				case "Cyan": return 11;
				case "Red": return 12;
				case "Magenta": return 13;
				case "Yellow": return 14;
				case "White": return 15;
				default: return 7;
			}
		}
		private void Error(int lineIndex, string keyLang, string path)
		{
			ErrorsHandler.Call(new()
			{
				Sender = Sender.DocumentationParser,
				Type = TypeMassage.Error,
				Path = path,
				LinesIndexes = new int[] { lineIndex + 1 },
				LinesMassage = new()
				{
					Lang.Key(keyLang)
				}
			});
		}
	}
}