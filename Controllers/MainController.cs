using LC.DataTypes;
using LC.Parsers;
namespace LC.Controllers
{
    public class MainController
    {
        public void Start()
        {
            try
            {
                bool stop = false;
                ProjectFile pf = new();
                ProjectData.ProjectProperties = pf.Parse(ProjectData.ConsoleOptions.PathToProject, ref stop);
                if (ProjectData.ProjectProperties == null || stop)
                {
                    Console.WriteLine(Lang.Key("LC-Compile-stoped."));
                    return;
                }
                if (ProjectData.ConsoleOptions.Mode == CompileType.Build)
                {
                    BaseCompile bc = new();
                    if (bc.Start()) { Console.WriteLine(Lang.Key("LC-Compile-succes.")); return; } else { Console.WriteLine(Lang.Key("LC-Compile-stoped.")); return; }
                }
                if (ProjectData.ConsoleOptions.Mode == CompileType.Hell)
                {
                    HellMode hm = new();
                    if (hm.Start()) { Console.WriteLine(Lang.Key("LC-Compile-succes.")); return; } else { Console.WriteLine(Lang.Key("LC-Compile-stoped.")); return; }
                }
            }
            catch (Exception e)
            {
                ErrorsHandler.Call(new()
                {
                    Sender = Sender.MainController,
                    Type = TypeMassage.Error,
                    LinesMassage = new()
                    {
                        "LC.Controllers.MainController.Start()",
                        "Exception: ",
                        e.ToString()
                    }
                });
            }
        }
    }
}