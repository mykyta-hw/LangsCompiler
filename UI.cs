using System;
using LC.Controllers;
using LC.DataTypes;
using LC.Parsers;
namespace LC
{
    public class UI 
    {
        string input = "";
        ConsoleOptionsParser cop = new();
        ConsoleOptions co = new();
        MainController mc = new();
        LC.Documentation Docs = new();
        Parsers.Documentation DocumentationParser = new();
        public void Start()
        {
            ReloadDocs();
            Console.WriteLine(Lang.Key("start"));
            while (true)
            {
                input = Console.ReadLine();
                if (input == "exit") { break; }
                else if (input == "") { continue; }
                else if (input == "docs") { continue; }
                else if (input == "docs reload") { ReloadDocs(); Console.WriteLine(Lang.Key("LC-Docs-Reloaded")); continue; }
                else if ((input.Length > 4 ? input.Substring(0, 4) : "") == "docs") 
                {
                    var page = Docs[input.Substring(4, input.Length - 4).Trim()];
                    if (page != null) { page.Print(); }
                    else { Console.WriteLine(Lang.Key("LC-Page-not-found.")); } 
                    continue; 
                }
                else if ((input.Length > 6 ? input.Substring(0, 4) : "") == "lang")
                {
                    Lang.SetLang(input.Substring(5, 2));
                    Console.WriteLine(Lang.Key("start"));
                }
                else 
                {
                    bool stop = false;
                    ProjectData.ConsoleOptions = cop.Parse(input.Split(' '), ref stop);
                    ProjectData.ConsoleOptions.CalledWithUI = true;
                    if (stop) { continue; }
                    mc.Start();
                }
            }
        }
        private void ReloadDocs()
        {
            Docs = new();
            DocumentationParser.Parse(Docs);
        }
    }
}