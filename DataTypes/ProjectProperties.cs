using System.Collections;
namespace LC.DataTypes
{
    public class ProjectProperties
    {
        private Hashtable ht;
        public ProjectProperties()
        {
            ht = new();
        }
        public int Count 
        {
            get 
            {
                return ht.Count;
            }
        }
        public void AddProperty(string name, string value)
        {
            try
            {
                ht.Add(name, value);
            }
            catch
            {
                System.Console.WriteLine("ProjectProperties, Property: " + name + " already exists.");
            }
        }
        public string GetProperty(string value)
        {
            if (ht.ContainsKey(value))
            {
                return ht[value].ToString();
            }
            return "";
        }
        public override string ToString()
        {
            return "\n" +
            "Project Data:\n";//+
            //"└─Path Project Folder: " + PathProjectFolder + "\n";
        }
    }
}