using System;
using System.Collections;
using System.IO;
using System.Text;
using LC.DataTypes;
using LC.Parsers;
namespace LC
{
	public static class Lang
	{
		public static string LangCode = "en";
		private static LangKeys Keys = new();
		public static bool Init()
		{
			Keys = new();
			Console.OutputEncoding = System.Text.Encoding.Default;
			try 
			{
				if (File.Exists(Directory.GetCurrentDirectory() + "/lang"))
				{
					using (FileStream f = File.Open(Directory.GetCurrentDirectory() + "/lang", FileMode.Open))
					{
						byte[] lang = new byte[2];

						f.ReadExactly(lang);
						LangCode = Encoding.ASCII.GetString(lang);

						f.Close();
					}
				}
				else 
				{
					using (FileStream f = File.Create(Directory.GetCurrentDirectory() + "/lang"))
					{
						byte[] b = Encoding.ASCII.GetBytes(LangCode);
						f.Write(b, 0, b.Length);
						f.Close();
					}
				}
				string[] LocalizationPathsFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.lang", SearchOption.AllDirectories);
				bool x = false;
				List<string> Paths = new();

				FindFiles:
				for (int i = 0; i < LocalizationPathsFiles.Length; i++)
				{
					using (FileStream f = File.Open(LocalizationPathsFiles[i], FileMode.Open))
					{
						byte[] lang = new byte[2];
						f.ReadExactly(lang);
						if (lang.Length > 1)
						{
							if (Encoding.ASCII.GetString(lang) == LangCode)
							{
								Paths.Add(LocalizationPathsFiles[i]);
							}
						}
						f.Close();
					}
				}
				if (Paths.Count > 0)
				{
					new Localization().ParseFiles(Paths, Keys);
					return true;
				}
				else
				{
					if (x)
					{
						Console.WriteLine("Files localization with code: " + LangCode + " or en, not finded, compilator dont work.");
						Console.WriteLine("Press all key to exit.");
						Console.ReadKey();
						return false;
					}
					else
					{
						LangCode = "en";
						x = true;
						goto FindFiles;
					}
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
			return false;
		}
		public static void SetLang(string lc)
		{
			LangCode = lc;
			Save();
			Init();
		}
		public static void Save()
		{
			using (FileStream f = File.Open(Directory.GetCurrentDirectory() + "/lang", FileMode.OpenOrCreate))
			{
				byte[] b = Encoding.ASCII.GetBytes(LangCode);
				f.Write(b, 0, b.Length);
				f.Close();
			}
		}
		public static string Key(string key)
		{
			if (Keys.Keys.ContainsKey(key))
			{
				return Keys.Keys[key].ToString();
			}
			else return key;
		}
	}
}