using System.Collections;
namespace LC.DataTypes
{
    public static class ProjectData
    {
        public static ConsoleOptions ConsoleOptions = new();
        public static ProjectProperties ProjectProperties = new();
        public static List<Language> Langs = new();
        public static List<Scenario> Scenarios = new();
        public static PathsFiles DefaultPathsFiles = new();
        public static DefaultGroupsFiles DefaultGroupsFiles = new();
        public static List<GroupFiles> GroupsFiles = new();
        public static void Clear()
        {
            ConsoleOptions = new();
            ProjectProperties = new();
            Langs = new();
            Scenarios = new();
            DefaultPathsFiles = new();
            DefaultGroupsFiles = new();
            GroupsFiles = new();
            GC.Collect();
        }
    }
}