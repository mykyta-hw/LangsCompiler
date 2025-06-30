using System;
using LC.Controllers;
using LC.DataTypes;
using LC.Parsers;
namespace LC
{
    public class Program
    {
        private static void Main(string[] args)
        {
            if (!Lang.Init()) return;
            if (args.Length > 0)
            {
                bool stop = false;
                ConsoleOptionsParser cop = new();
                ProjectData.ConsoleOptions = cop.Parse(args, ref stop);
                if (!stop) 
                {
                    MainController mc = new();
                    mc.Start();
                    return;
                }
                else return;
            }
            UI ui = new();
            ui.Start();
        }
    }
}