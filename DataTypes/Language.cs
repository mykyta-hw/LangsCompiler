using System.Collections.Generic;
namespace LC.DataTypes
{
    public class Language
    {
        public LangPropierties Info { get; set; }
        public List<IRObject> IRObjects { get; set; }
        public List<CGFormat> CGFormats { get; set; }
        public Language()
        {
           IRObjects = new(); 
           Info = new();
           CGFormats = new();
        }
    }
}