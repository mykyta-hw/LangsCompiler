using LC.DataTypes;

namespace LC.Parsers
{
    public class Localization
	{
		public void ParseFiles(List<string> paths, LangKeys keysTable)
		{
			foreach (string s in paths)
			{
				Parse(s, keysTable);
			}
		}
		public void Parse(string filePath, LangKeys KeysTable)
		{
			FileCode File = new();
			Token t = new();
			Lexer lx = new();
			int State = 0;
			Word Temp = new();
			Word Key = new();
			int LastLineIndex = 0;
			bool end = false;

			File.SetPath(filePath);
			File.StartStream();
			while (true)
			{
				t = File.GetNextToken(ref lx, out end);
				if (end) break;
				LastLineIndex = t.IndexStartLine;
				//Console.WriteLine("State: " + State + ", " + (string)Key + ", " + (string)Temp + ", " + (string)t.UValue);
				if (State == 0)//...
				{
					if (t.Type == TokenType.Word) { State = 2; continue; }
					if (t.Type == TokenType.Space) { continue; }
					else { Error(t.IndexStartLine, "LC-Expected-lang-code.", filePath); break; }
				}
				if (State == 2)//aa
				{
					if (t.Type == TokenType.Space) { continue; }
					if (t.Type == TokenType.NewLine) { State = 3; continue; }
					else { Error(t.IndexStartLine, "LC-Expected-enter.", filePath); break; }
				}
				if (State == 3)//aa [newline] {none}
				{
					if (t.Type == TokenType.NewLine) { continue; }
					else { Key = t.UValue; State = 4; continue; }
				}
				if (State == 4)//aa [newline] {Key-build}
				{
					if (t.Type == TokenType.Colon) { State = 5; continue; }
					if (t.Type == TokenType.NewLine) { Error(t.IndexStartLine, "LC-Enter-cant-use.", filePath); break; }
					else { Key += t.UValue; continue; }
				}
				if (State == 5)//aa [newline] {Key}:
				{
					if (t.Type == TokenType.Colon) { State = 6; continue; }
					if (t.Type == TokenType.NewLine) { Error(t.IndexStartLine, "LC-Enter-cant-use.", filePath); break; }
					else { State = 4; Key += new U(58); Key += t.UValue; continue; }
				}
				if (State == 6)//aa [newline] ... :: ...
				{
					if (t.Type == TokenType.Semicolon) { State = 7; continue; }
					else { Temp += t.UValue; continue; }
				}
				if (State == 7)//aa [newline] ... :: ... ;
				{
					if (t.Type == TokenType.Semicolon) 
					{
						State = 8;
						try 
						{
							if (!KeysTable.Keys.ContainsKey(Key))
							{
								KeysTable.Keys.Add((string)Key, (string)Temp);
							}
						}
						catch (Exception e)
						{
							ErrorsHandler.Call(new()
							{
								Sender = Sender.Lang,
								Type = TypeMassage.Error,
								LinesMassage = new()
								{
									"Exception: ",
									e.Message
								}
							});
						}
						Temp = new();
						continue; 
					}
					else { State = 6; Temp += new U(59); Temp += t.UValue; continue; }
				}
				if (State == 8)//aa [newline] ... :: ... ;;
				{
					if (t.Type == TokenType.NewLine) { State = 3; continue; }
					else { State = 3; Key = t.UValue; continue; }
				}
			}
			if (State == 2) Error(LastLineIndex, "LC-Expected-enter.", filePath);
			if (State == 4) Error(LastLineIndex, "LC-Expected-colon.", filePath);
			if (State == 5) Error(LastLineIndex, "LC-Expected-value.", filePath);
			File.CloseStream();
		}
		private void Error(int lineIndex, string keyLang, string path)
		{
			ErrorsHandler.Call(new()
			{
				Sender = Sender.Lang,
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