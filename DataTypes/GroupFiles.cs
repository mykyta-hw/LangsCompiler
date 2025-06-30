using System.Collections.Generic;
namespace LC.DataTypes
{
    public class GroupFiles
    {
        public List<FileCode> Files { get; set; }
        public GroupFiles()
        {
            Files = new();
        }
    }
}