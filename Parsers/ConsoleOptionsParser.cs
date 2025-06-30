using LC.DataTypes;
namespace LC.Parsers
{
    public class ConsoleOptionsParser
    {
        public DataTypes.ConsoleOptions Parse(string[] args, ref bool stop)
        {
            DataTypes.ConsoleOptions co = new();
            int state = 0;
            string lineFoldersLangs = "";
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "mlcc") { continue; }
                else if (args[i] == "build") {    if (state == 0) {    state = 1; co.Mode = CompileType.Build; continue; } }
                else if (args[i] == "hellmode") { if (state == 0) {    state = 1; co.Mode = CompileType.Hell;  continue; } }
                else if (args[i] == "-o") {       if (state == 2) {    state = 3; continue; } }
                else if (args[i] == "-l") {       if (state == 2) {    state = 4; continue; } }
                else if (args[i] == "-c") {       if (state == 2) {    state = 5; continue; } }
                else if (state == 1) { co.PathToProject     = args[i]; state = 2; continue; }
                else if (state == 3) { co.PathToOutFolder   = args[i]; state = 2; continue; }
                else if (state == 4) { lineFoldersLangs     = args[i]; state = 2; continue; }
                else if (state == 5) { co.PathToCacheFolder = args[i]; state = 2; continue; }
                else { Console.WriteLine(Lang.Key("LC-Option-not-correct.") + args[i]);                                  stop = true; return co; }
            }
            if (co.PathToProject == "")                                              { Error1();                     stop = true; return co; }
            if (!File.Exists(co.PathToProject) && co.PathToProject != "")            { Error2(co.PathToProject);     stop = true; return co; }
            if (!File.Exists(co.PathToOutFolder) && co.PathToOutFolder != "")        { Error3(co.PathToOutFolder);   stop = true; return co; }
            if (!File.Exists(co.PathToCacheFolder) && co.PathToCacheFolder != "")    { Error5(co.PathToCacheFolder); stop = true; return co; }
            if (!CheckLangFolder(lineFoldersLangs, ref co) && lineFoldersLangs != "")                              { stop = true; return co; }
            return co;
        }
        private bool CheckLangFolder(string line, ref DataTypes.ConsoleOptions co)
        {
            if (line == "") { return true; }
            string[] lines = line.Split(',');
            foreach (string path in lines)
            {
                if (!File.Exists(path))
                {
                    Error4(path);
                    return false;
                }
            }
            co.PathsToLangFolders = lines;
            return true;
        }
        private void Error1()
        {
            ErrorsHandler.Call(new()
            {
                Sender = Sender.ConsoleOptionsParser,
                Type = TypeMassage.Error,
                LinesMassage = new()
                {
                    Lang.Key("LC-Project-file-path-not-writed.")
                }
            });
        }
        private void Error2(string path)
        {
            ErrorsHandler.Call(new()
            {
                Sender = Sender.ConsoleOptionsParser,
                Type = TypeMassage.Error,
                LinesMassage = new()
                {
                    Lang.Key("LC-Writed-path-project-file: ") + path,
                    Lang.Key("LC-Is-not-correct.")
                }
            });
        }
        private void Error3(string path)
        {
            ErrorsHandler.Call(new()
            {
                Sender = Sender.ConsoleOptionsParser,
                Type = TypeMassage.Error,
                LinesMassage = new()
                {
                    Lang.Key("LC-Writed-path-out-folder: ") + path,
                    Lang.Key("LC-Is-not-correct.")
                }
            });
        }
        private void Error4(string path)
        {
            ErrorsHandler.Call(new()
            {
                Sender = Sender.ConsoleOptionsParser,
                Type = TypeMassage.Error,
                LinesMassage = new()
                {
                    Lang.Key("LC-Writed-path-lang-folder:  ") + path,
                    Lang.Key("LC-Is-not-correct.")
                }
            });
        }
        private void Error5(string path)
        {
            ErrorsHandler.Call(new()
            {
                Sender = Sender.ConsoleOptionsParser,
                Type = TypeMassage.Error,
                LinesMassage = new()
                {
                    Lang.Key("LC-Writed-path-cache-folder: ") + path,
                    Lang.Key("LC-Is-not-correct.")
                }
            });
        }
    }
}