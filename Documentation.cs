using System.Collections;
using System.Collections.Generic;
using LC.DataTypes;
namespace LC 
{
	public class Documentation
	{
		private List<Page> Pages = new();
		private Hashtable KeyIndexes = new();
		public Page this[string key]
		{
			get 
			{
				if (KeyIndexes.ContainsKey(key))
					return Pages[(int)KeyIndexes[key]];
				return null;
			}
		}
		public void AddPage(Page p, string key)
		{
			Pages.Add(p);
			KeyIndexes.Add(key, Pages.Count -1);
		}
	}
	public class Page 
	{
		private List<List<Word>> KeysText = new();
		private List<List<int>> Colors = new();
		private List<List<bool>> IsKey = new();
		public Page() {}
		public Page(List<List<Word>> text, List<List<int>> colors, List<List<bool>> iskey)
		{
			this.KeysText = text;
			this.Colors = colors;
			this.IsKey = iskey;
		}
		public void Print()
		{
			for (int i = 0; i < KeysText.Count; i++)
			{
				for (int j = 0; j < KeysText[i].Count; j++)
				{
					Console.ForegroundColor = (ConsoleColor)Colors[i][j];
					if (IsKey[i][j])
					{
						Console.Write(Lang.Key((string)KeysText[i][j]));
					}
					else 
					{
						Console.Write(((string)KeysText[i][j]).Trim('\"'));
					}
				}
				Console.WriteLine();
			}
			Console.ForegroundColor = ConsoleColor.Gray;
		}
	}
}