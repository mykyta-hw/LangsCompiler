using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LC.DataTypes;
namespace LC
{
    public static class ErrorsHandler
    {
        public static void RewriteLine(string s)
        {
            Console.WriteLine("\r" + s);
        }
        public static void ReCall(MassageFull m)
        {
            string sout = "";
            int lines = 0;
            //sout += "[" + m.Sender + "/" + m.Type + "]\n";
            if (m.Path != "") { lines++; sout += "[" + m.Sender + "/" + m.Type + "] " + "File path: " + m.Path + "\n"; }
            if ( m.StartIndexPositions.Length > 0 ) { lines++; sout += "[" + m.Sender + "/" + m.Type + "] " + "Positions: "; m.StartIndexPositions.ToList().ForEach(x=>{ sout += (x+1) + ", "; }); sout += "\n"; }
            if ( m.LinesIndexes.Length > 0 ) { lines++; sout += "[" + m.Sender + "/" + m.Type + "] " + "Lines: "; m.LinesIndexes.ToList().ForEach(x=>{ sout += (x+1) + ", "; }); sout += "\n"; }
            if ( m.LinesMassage.Count > 0 ) { m.LinesMassage.ForEach(x => { lines++; sout += "[" + m.Sender + "/" + m.Type + "] " + x + "\n";});  }
            lines = Console.CursorTop - lines;
            Console.SetCursorPosition(0, lines > -1 ? lines : 0);
            Console.Write(sout);
        }
        public static void Call(Massage m, TypeMassage t, Sender s, string path, int p, int[] lsi)
        {
            string sout = "";
            sout += s + " " + t + "\n";
            sout += path + " Position: " + p + " Lines: ";
            if ( lsi.Length > 0 ) { lsi.ToList().ForEach(x=>{ sout += x + ", "; }); }
            sout += "\n";
            if (m.Lines.Count > 0){ m.Lines.ForEach(x => { sout += x + "\n";}); }
            Console.WriteLine(sout);
        }
        public static void Call(ErrorsHandlerMassage ehm, string path, int p, int[] lsi)
        {
            string sout = "";
            sout += ehm.s + " " + ehm.t + "\n";
            sout += path + " Position: " + p + " Lines: ";
            if ( lsi.Length > 0 ) { lsi.ToList().ForEach(x => { sout += x + ", "; }); }
            sout += "\n";
            if (ehm.m.Lines.Count > 0){ ehm.m.Lines.ForEach(x => { sout += x + "\n";}); }
            Console.WriteLine(sout);
        }
        public static void Call(ErrorsHandlerMassage ehm, PositionFileMassage pos)
        {
            string sout = "";
            sout += ehm.s + " " + ehm.t + "\n";
            sout += pos.Path + " Position: " + pos.StartIndexPosition + " Lines: ";
            if ( pos.LinesIndexes.Length > 0 ) { pos.LinesIndexes.ToList().ForEach(x=>{ sout += x + ", "; }); }
            sout += "\n";
            if ( ehm.m.Lines.Count > 0 ) { ehm.m.Lines.ForEach(x => { sout += x + "\n";});  }
            Console.WriteLine(sout);
        }
        public static void Call(MassageFull m)
        {
            string sout = "";
            //sout += "[" + m.Sender + "/" + m.Type + "]\n";
            if (m.Path != "") { sout += "[" + m.Sender + "/" + m.Type + "] " + "File path: " + m.Path + "\n"; }
            if ( m.StartIndexPositions.Length > 0 ) { sout += "[" + m.Sender + "/" + m.Type + "] " + "Positions: "; m.StartIndexPositions.ToList().ForEach(x=>{ sout += (x+1) + ", "; }); sout += "\n"; }
            if ( m.LinesIndexes.Length > 0 ) {sout += "[" + m.Sender + "/" + m.Type + "] " + "Lines: "; m.LinesIndexes.ToList().ForEach(x=>{ sout += (x+1) + ", "; }); sout += "\n"; }
            if ( m.LinesMassage.Count > 0 ) { m.LinesMassage.ForEach(x => { sout += "[" + m.Sender + "/" + m.Type + "] " + x + "\n";});  }
            Console.Write(sout);
        }
		public static void Call(MassageColored m, int a = 0)
		{
			string sout = "";
			if ( m.Path != "" )                     { sout += "[" + m.Sender + "/" + m.Type + "] " + "File path: " + m.Path + "\n"; }
			if ( m.StartIndexPositions.Length > 0 ) { sout += "[" + m.Sender + "/" + m.Type + "] " + "Positions: "; m.StartIndexPositions.ToList().ForEach(x=>{ sout += (x+1) + ", "; }); sout += "\n"; }
			if ( m.LinesIndexes.Length > 0 )        { sout += "[" + m.Sender + "/" + m.Type + "] " + "Lines: "; m.LinesIndexes.ToList().ForEach(x=>{ sout += (x+1) + ", "; }); sout += "\n"; }
			//if ( m.LinesMassage.Count > 0 )         { m.LinesMassage.ForEach(x => { sout += "[" + m.Sender + "/" + m.Type + "] " + x + "\n";});  }
			Console.Write(sout);
			for (int i = 0; i < m.LinesMassage.Count; i++)
			{
				List<WordMC> b = m.LinesMassage[i];
				Console.Write("[" + m.Sender + "/" + m.Type + "] ");
				for (int j = 0; j < b.Count; j++)
				{
					Console.ForegroundColor = (ConsoleColor)b[j].Color;
					Console.Write(b[j].Word);
				}
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Gray;
			}
			
		}
    }
	public class MassageColored
    {
    	public List<List<WordMC>> LinesMassage { get; set; }
    	public TypeMassage Type { get; set; }
    	public Sender Sender { get; set; }
    	public string Path { get; set; }
    	public int[] StartIndexPositions { get; set; }
    	public int[] LinesIndexes { get; set; }
    	public MassageColored()
    	{
    		Path = "";
            StartIndexPositions = new int[0];
            LinesIndexes = new int[0];
            LinesMassage = new();
            Type = new();
            this.Sender = new();
    	}
    }
	public class WordMC
	{
		public string Word;
		public int Color;
		public WordMC(string a, int b)
		{
			Word = a;
			Color = b;
		}
	}
    public class MassageFull
    {
    	public List<string> LinesMassage { get; set; }
    	public TypeMassage Type { get; set; }
    	public Sender Sender { get; set; }
    	public string Path { get; set; }
    	public int[] StartIndexPositions { get; set; }
    	public int[] LinesIndexes { get; set; }
    	public MassageFull()
    	{
    		Path = "";
            StartIndexPositions = new int[0];
            LinesIndexes = new int[0];
            LinesMassage = new();
            Type = new();
            this.Sender = new();
    	}
    }
    public class PositionFileMassage
    {
        public string Path {get; set;}
        public int StartIndexPosition {get; set;}
        public int[] LinesIndexes {get; set;}
        public PositionFileMassage()
        {
            Path = "";
            StartIndexPosition = 0;
            LinesIndexes = new int[0];
        }
    }
    public class ErrorsHandlerMassage
    {
        public Massage m {get; set;}
        public TypeMassage t {get; set;}
        public Sender s {get; set;}
        public ErrorsHandlerMassage()
        {
            m = new();
            t = new();
            s = new();
        }
    }
    public class Massage
    {
        public List<string> Lines {get; set;}
        public Massage()
        {
            Lines = new();
        }
    }
    public enum TypeMassage
    {
        Error,
        Warn,
        Info
    }
    public enum Sender
    {
        BuildConfigParser,
        ProjectFileParser,
        SyntaxParser,
        SemanticParser,
        CodeGenerationParser,
        IRParser,
        LIParser,
        FileCode,
        Lang,
        BaseCompile,
        MainController,
        ConsoleOptionsParser,
        Lexer,
        Tester,
        DocumentationParser,
        SyntaxRules
    }
}