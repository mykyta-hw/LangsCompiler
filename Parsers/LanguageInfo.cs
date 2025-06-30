using System.Collections.Generic;
using LC.DataTypes;
using LC;
namespace LC.Parsers
{
    public class LanguageInfo
    {
        public List<Language> Parse(ref GroupFiles g, ref bool stop)
        {
            Lexer lx = new();
            List<Language> Langs = new();
            int c = g.Files.Count;
            for (int i = 0; i < c; i++)
            {
                Language Lang = new();
                FileCode f = g.Files[i];
                LangPropierties LangProperties = new();
                Token t;
                Word Key = new();
                Word Value = new();
				int State = 0;
                bool end = false;

                lx.Reset();
                f.StartStream();

                while (true)
                {
                    t = f.GetNextToken(ref lx, out end);
                    if (end) { break; }

                    if ( State == 0 ) {
						if (t.Type == TokenType.NewLine) { continue; }
						if (t.Type == TokenType.Space) { continue; }
						else { State = 1; }
					}
					if (State == 1) {
						if (t.Type == TokenType.NewLine) { Error(t.IndexStartLine, "LC-Enter-cant-use.", f.Info.Name); break; }
						if (t.Type == TokenType.Space) { 
							if (Key.Length > 0) { State = 2; continue; }
							else { Error(t.IndexStartLine, "LC-Expected-Key.", f.Info.Name); break; }
						}
						else { Key += t.UValue; continue; }
					}
					if (State == 2) {
						if (t.Type == TokenType.NewLine) { 
							if (Key == KeyWords.Name) { LangProperties.Name = (string)Value; }
							State = 0;
							LangProperties.AddProperty((string)Key, (string)Value);
							Key = new();
							Value = new();
							continue; 
						}
						else { Value += t.UValue; continue; }
					}
				} 
				if (State == 1) { Error(t.IndexStartLine, "LC-Expected-Key.", f.Info.FullName); }
				if (State == 2) { LangProperties.AddProperty((string)Key, (string)Value); }

				Lang.Info = LangProperties;
				Langs.Add(Lang);
				f.CloseStream();
				Info(LangProperties.Name, LangProperties.Count);
            }
            return Langs;
        }
        private void Error(int lineIndex, string key, string path)
        {
            ErrorsHandler.Call(new()
            {
                Sender = Sender.LIParser,
                Type = TypeMassage.Error,
                Path = path,
                LinesIndexes = new int[] { lineIndex + 1 },
                LinesMassage = new()
                {
                    Lang.Key(key)
                }
            });
        }
        private void Info(string NameLang, int CountPairs)
        {
            ErrorsHandler.Call(new()
            {
                Sender = Sender.LIParser,
                Type = TypeMassage.Info,
                LinesMassage = new()
                {
                    Lang.Key("LC-Added-Lang: ") + NameLang + ", " + Lang.Key("LC-Pairs-count: ") + CountPairs
                }
            });
        }
    }
}