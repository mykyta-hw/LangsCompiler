using LC.DataTypes;
using LC.Parsers;
using System.IO;
namespace LC.Controllers
{
    public class BaseCompile
    {
        public bool Start()
        {
            bool stop = false;
            ProjectFilesController PFC = new(ref stop);
            PFC.GetPathsDefaultFiles();
            PFC.InstanceFiles(ref stop);
            if (stop) { ProjectData.Clear(); return false; }

            ///------------------------------------------------------------------------
            ///Parsing LI files and creating Langs
            var lf = ProjectData.DefaultGroupsFiles.LIFiles;
            ProjectData.Langs = new LanguageInfo().Parse(ref lf, ref stop);
            if (stop) { ProjectData.Clear(); return false; }

            ///------------------------------------------------------------------------
            ///Parsing IR files
            var irf = ProjectData.DefaultGroupsFiles.IRFiles;
            IRFiles IRParser = new();
            IRParser.Parse(ref irf, ref stop);
            if (stop) { ProjectData.Clear(); return false; }

            ///------------------------------------------------------------------------
            ///Parsing Sy files
            var syf = ProjectData.DefaultGroupsFiles.SyFiles;
            SyntaxFiles SyParser = new();
            SyParser.Parse(ref syf, ref stop);
            if (stop) { ProjectData.Clear(); return false; }

            ///------------------------------------------------------------------------
            ///Parsing Se files
            var sef = ProjectData.DefaultGroupsFiles.SeFiles;
            SemanticFiles SeParser = new();
            SeParser.Parse(ref sef, ref stop);
            if (stop) { ProjectData.Clear(); return false; }

            ///------------------------------------------------------------------------
            ///Parsing CG files
            CodeGenerationFiles CGParser = new();
            var cgf = ProjectData.DefaultGroupsFiles.CGFiles;
            CGParser.Parse(ref cgf, ref stop);
            if (stop) { ProjectData.Clear(); return false; }

            ///------------------------------------------------------------------------
            ///Parsing BC files
            BuildConfigFiles BCParser = new();
            var bcf = ProjectData.DefaultGroupsFiles.BCFiles;
            BCParser.Parse(ref bcf, ref stop);
            if (stop) { ProjectData.Clear(); return false; }

            //Start compilation
            BCRunner BCRunner = new();
            BCRunner.Start(ref stop);
            ProjectData.Clear();
            if (stop) { return false; }
            else return true;
        }
    }
}