using System.Collections;
namespace LC.DataTypes
{
    public class LangPropierties
    {
        public string Name { get; set; }
        private Hashtable ht;
        public LangPropierties()
        {
            ht = new();
            Name = "";
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
                System.Console.WriteLine("LangProperties, Property: " + name + " already exists.");
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
    }
}