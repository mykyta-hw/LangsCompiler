using LC.DataTypes;
using LC;
namespace LC.Parsers
{
    public class ProjectFile
    {
        public ProjectProperties Parse(string path, ref bool stop)
        {
            Lexer lx = new();
            FileCode f = new();
            ProjectProperties Data = new();
            Token t;
            int State = 0;
            Word Key = new();
            Word Value = new();
            bool End = false;

            lx = new();
            if (!File.Exists(path))
            {
                return Data;
            }
            f.SetPath(path);
            f.StartStream();

            while (true)
            {
                t = f.GetNextToken(ref lx, out End);
                if (End) { break; }

                if ( State == 0 ) {
                    if (t.Type == TokenType.NewLine) { continue; }
					if (t.Type == TokenType.Space) { continue; }
					else { State = 1; }
                }
				if (State == 1) {
                    if (t.Type == TokenType.NewLine) { Error(t.IndexStartLine, "LC-Enter-cant-use.", path); break; }
					if (t.Type == TokenType.Space) { 
						if (Key.Length > 0) { State = 2; continue; }
						else { Error(t.IndexStartLine, "LC-Expected-Key.", path); break; }
					}
					else { Key += t.UValue; continue; }
				}
				if (State == 2) {
					if (t.Type == TokenType.NewLine) { 
						State = 0;
						Data.AddProperty((string)Key, (string)Value);
						Key = new();
						Value = new();
						continue; 
					}
					else { Value += t.UValue; continue; }
				}
            }
			if (State == 1) { Error(t.IndexStartLine, "LC-Expected-Key.", path); }
			if (State == 2) { Data.AddProperty((string)Key, (string)Value); }
            f.CloseStream();
            return Data;
        }
        private void Error(int lineIndex, string key, string path)
        {
            ErrorsHandler.Call(new()
            {
                Sender = Sender.ProjectFileParser,
                Type = TypeMassage.Error,
                Path = path,
                LinesIndexes = new int[] { lineIndex + 1 },
                LinesMassage = new()
                {
                    Lang.Key(key)
                }
            });
        }
    }
}