namespace LC.DataTypes.SemanticRules
{
    public class Root 
    {
        public int IndexEntryRule { get; set; }
        public Rule[] Rules { get; set; }
        public Var[] Vars { get; set; }
    }
    public class Rule
    {
        public Error Error { get; set; }
        public int IndexTypes { get; set; }
        public Line[] Lines { get; set; }
    }
    public class Error
    {
        public Word[] Keys { get; set; }
        public int[] IndexesVars { get; set; }
        public bool[] IsKey { get; set; }
        public bool[] IsLocalVar { get; set; }
    }
    public class Line
    {
        public int IndexWorkVar { get; set; }
        public bool IsLocalWorkVar { get; set; }
        public Element[] Elements { get; set; }
    } 
    public class Element
    {
        public int IndexRule { get; set; }

        public int IndexVar { get; set; }
        public bool IsLocalVar { get; set; }
        public TypeOperator TypeOperator { get; set; }

        public Call Call { get; set; }

        public bool Direction { get; set; } // true - next, false - prew
        public int IndexLine { get; set; }
    }
    public class Call
    {
        public int Index { get; set; }
        public TypeCallOption[] Type { get; set; }
        public Word[] Literals { get; set; }
        public int[] IndexVar { get; set; }
    } 
    public class Var
    {
        public int IndexType { get; set; }
        public bool IsLocal { get; set; }
    }

    public enum TypeOperator
    {
        Save,
        Load,
        Equal,
        NotEqual,
        More,
        Less,
        MoreEqual,
        LessEqual,
        Add,
        Sub,
        Mul,
        Div,
    }
    public enum TypeCallOption
    {
        Literal,
        Var,
        LocalVar
    }
}