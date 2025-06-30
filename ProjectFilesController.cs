using LC.DataTypes;

namespace LC
{
    public class ProjectFilesController
    {
        private string PathFolderProject = "";
        public ProjectFilesController(ref bool stop)
        {
            this.PathFolderProject = GetFolder(ProjectData.ConsoleOptions.PathToProject, ref stop);
        }
        public void InstanceFiles(ref bool stop)
        {
            ProjectData.DefaultGroupsFiles.LIFiles = InstanceGroupFiles(ProjectData.DefaultPathsFiles.LIFiles, ref stop);
            ProjectData.DefaultGroupsFiles.IRFiles = InstanceGroupFiles(ProjectData.DefaultPathsFiles.IRFiles, ref stop);
            ProjectData.DefaultGroupsFiles.SyFiles = InstanceGroupFiles(ProjectData.DefaultPathsFiles.SyFiles, ref stop);
            ProjectData.DefaultGroupsFiles.SeFiles = InstanceGroupFiles(ProjectData.DefaultPathsFiles.SeFiles, ref stop);
            ProjectData.DefaultGroupsFiles.CGFiles = InstanceGroupFiles(ProjectData.DefaultPathsFiles.CGFiles, ref stop);
            ProjectData.DefaultGroupsFiles.BCFiles = InstanceGroupFiles(ProjectData.DefaultPathsFiles.BCFiles, ref stop);
        }
        private GroupFiles InstanceGroupFiles(List<string> paths, ref bool stop)
        {
            GroupFiles gp = new();
            try {
                for (int i = 0; i < paths.Count; i++) {
                    FileCode f = new();
                    f.SetPath(paths[i]);
                    gp.Files.Add(f);
                }
            }
            catch (Exception e) {
                ErrorsHandler.Call(new() {
                    Sender = Sender.BaseCompile,
                    Type = TypeMassage.Error,
                    LinesMassage = new() {
                        "LC.ProjectFilesController.InstanceFiles()",
                        "Exception: ",
                        e.Message
                    }
                });
                stop = true;
                return gp;
            }
            return gp;
        }
        public void GetPathsDefaultFiles()
        {
            if (PathFolderProject == "") return;
            try {
                ProjectData.DefaultPathsFiles = new() {
                    LIFiles = Directory.GetFiles(PathFolderProject, "*.li", SearchOption.AllDirectories).ToList(),
                    BCFiles = Directory.GetFiles(PathFolderProject, "*.bc", SearchOption.AllDirectories).ToList(),
                    SyFiles = Directory.GetFiles(PathFolderProject, "*.sy", SearchOption.AllDirectories).ToList(),
                    SeFiles = Directory.GetFiles(PathFolderProject, "*.se", SearchOption.AllDirectories).ToList(),
                    IRFiles = Directory.GetFiles(PathFolderProject, "*.ir", SearchOption.AllDirectories).ToList(),
                    CGFiles = Directory.GetFiles(PathFolderProject, "*.cg", SearchOption.AllDirectories).ToList(),
                };
                ApplyPaths();
            }
            catch (Exception e) {
                ErrorsHandler.Call(new() {
                    Sender = Sender.BaseCompile,
                    Type = TypeMassage.Error,
                    LinesMassage = new() {
                        "LC.ProjectFilesController.GetPathsDefaultFiles()",
                        "Exception: ",
                        e.Message
                    }
                });
                return;
            }
        }
        private string GetFolder(string filePath, ref bool stop)
        {
            try { if (filePath != null) { return System.IO.Path.GetDirectoryName(filePath); } 
            }
            catch (Exception e) {
                ErrorsHandler.Call(new() {
                    Sender = Sender.BaseCompile,
                    Type = TypeMassage.Error,
                    LinesMassage = new() {
                        "LC.ProjectFilesController.GetFolder(string filePath)",
                        "Exception: ",
                        e.Message
                    }
                });
                stop = true;
                return "";
            }
            return "";
        }
        public void ApplyPaths()
        {
            try
            {
                string[] paths = ProjectData.ConsoleOptions.PathsToLangFolders;
                for (int i = 0; i < paths.Length; i++)
                {
                    ProjectData.DefaultPathsFiles.BCFiles.AddRange(Directory.GetFiles(paths[i], "*.bc", SearchOption.AllDirectories));
                    ProjectData.DefaultPathsFiles.LIFiles.AddRange(Directory.GetFiles(paths[i], "*.li", SearchOption.AllDirectories));
                    ProjectData.DefaultPathsFiles.SyFiles.AddRange(Directory.GetFiles(paths[i], "*.sy", SearchOption.AllDirectories));
                    ProjectData.DefaultPathsFiles.SeFiles.AddRange(Directory.GetFiles(paths[i], "*.se", SearchOption.AllDirectories));
                    ProjectData.DefaultPathsFiles.IRFiles.AddRange(Directory.GetFiles(paths[i], "*.ir", SearchOption.AllDirectories));
                    ProjectData.DefaultPathsFiles.CGFiles.AddRange(Directory.GetFiles(paths[i], "*.cg", SearchOption.AllDirectories));
                };
            }
            catch (Exception e)
            {
                ErrorsHandler.Call(new()
                {
                    Sender = Sender.BaseCompile,
                    Type = TypeMassage.Error,
                    LinesMassage = new()
                    {
                        "LC.Controllers.BaseCompile.ApplyPaths()",
                        "Exception: ",
                        e.Message
                    }
                });
                return;
            }
        }

        private List<string> PathsFoldersToSave = new();
        public int CreateGroupFiles(Word name) //name for name folder with files, scenario.name + groupfiles.name
        {
            ProjectData.GroupsFiles.Add(new GroupFiles());
            PathsFoldersToSave.Add(PathFolderProject + "/" + (string)name);
            return ProjectData.GroupsFiles.Count - 1;
        }
        public void CreateFileInGroup(int IndexGroupFiles, Word Name)
        {
            FileCode f = new();
            f.SetPath(PathsFoldersToSave[IndexGroupFiles] + "/" + (string)Name);
            f.Create();
            ProjectData.GroupsFiles[IndexGroupFiles].Files.Add(f);
        } 
    }
}