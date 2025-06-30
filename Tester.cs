using LC.DataTypes;
public class Tester
{
	public List<string> Lines = new();
	private int Tabs = 0;

	public void Clear() => Lines = new();
	public void EmpetyLine() => Lines.Add("");
	public void TabLine() => Tabs++;
	public void UnTabLine() => Tabs--;
	public void Log(string a) => Lines.Add(GetTabs() + "<Log>: " + a + ";");
	public void ExecutePoint(string a) => Lines.Add(GetTabs() + "<ExecutePoint>: " + a + ";");
	public void Var(int a, string name) => Lines.Add(GetTabs() + "<Var> int " + name + " = " + a + ";");
	public void Var(bool a, string name) => Lines.Add(GetTabs() + "<Var> bool " + name + " = " + a + ";");
	public void Var(byte a, string name) => Lines.Add(GetTabs() + "<Var> byte " + name + " = " + a + ";");
	public void Var(long a, string name) => Lines.Add(GetTabs() + "<Var> long " + name + " = " + a + ";");
	public void Var(ulong a, string name) => Lines.Add(GetTabs() + "<Var> ulong " + name + " = " + a + ";");
	public void Var(string a, string name) => Lines.Add(GetTabs() + "<Var> string " + name + " = " + a + ";");
	public void Var(U u, string name) => Lines.Add(GetTabs() + "<Var> U " + name + " { C = " + u.C + ", B = " + u.B + ", A = " + u.A + " } ");
	public void Var(Token a, string name) { if (a != null) { Lines.Add(GetTabs() + "<Var> Token " + name + " = " + a.ToString() + ";"); } }
	public void Dump(bool[] array, string nameArray)
	{
		string b = GetTabs() + "<Dump>: byte[] " + nameArray + " { ";
		for (int i = 0; i < array.Length; i++)
		{
			b += array[i] + " , ";
		}
		b += "end };";
		Lines.Add(b);
	}
	public void Dump(byte[] array, string nameArray)
	{
		string b = GetTabs() + "<Dump>: byte[] " + nameArray + " { ";
		for (int i = 0; i < array.Length; i++)
		{
			b += array[i] + " , ";
		}
		b += "end };";
		Lines.Add(b);
	}
	public void Dump(int[] array, string nameArray)
	{
		string b = GetTabs() + "<Dump>: int[] " + nameArray + " { ";
		for (int i = 0; i < array.Length; i++)
		{
			b += array[i] + " , ";
		}
		b += "end };";
		Lines.Add(b);
	}
	public void Dump(long[] array, string nameArray)
	{
		string b = GetTabs() + "<Dump>: long[] " + nameArray + " { ";
		for (int i = 0; i < array.Length; i++)
		{
			b += array[i] + " , ";
		}
		b += "end };";
		Lines.Add(b);
	}
	public void Dump(string[] array, string nameArray)
	{
		string t = GetTabs();
		Lines.Add(t + "<Dump>: string[] " + nameArray + " { ");
		TabLine();
		t = GetTabs();
		for (int i = 0; i < array.Length; i++)
		{
			Lines.Add(t + i + ": " + array[i]);
		}
		UnTabLine();
		t = GetTabs();
		Lines.Add(t + "}");
	}
	public void Dump(Token[] array, string nameArray)
	{
		if (array == null) return;
		string t = GetTabs();
		Lines.Add(t + "<Dump>: Token[] " + nameArray + " { ");
		TabLine();
		t = GetTabs();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == null)
			{
				Lines.Add(t + i + ": " + "null");
				continue;
			}
			Lines.Add(t + i + ": " + array[i].ToString());
		}
		UnTabLine();
		t = GetTabs();
		Lines.Add(t + "}");
	}
	public void Dump(Word[] array, string nameArray)
	{
		if (array == null) return;
		string t = GetTabs();
		Lines.Add(t + "<Dump>: Token[] " + nameArray + " { ");
		TabLine();
		t = GetTabs();
		for (int i = 0; i < array.Length; i++)
		{
			Lines.Add(t + i + ": " + (string)array[i]);
		}
		UnTabLine();
		t = GetTabs();
		Lines.Add(t + "}");
	}
	public void Dump(List<int> array, string nameArray)
	{
		if (array == null) return;
		string t = GetTabs();
		Lines.Add(t + "<Dump>: List<int> " + nameArray + " { ");
		TabLine();
		t = GetTabs();
		for (int i = 0; i < array.Count; i++)
		{
			Lines.Add(t + i + ": " + array[i]);
		}
		UnTabLine();
		t = GetTabs();
		Lines.Add(t + "}");
	}
	private string GetTabs() { string s = ""; for (int i = 0; i < Tabs; i++) { s += "\t"; } return s; }
}
