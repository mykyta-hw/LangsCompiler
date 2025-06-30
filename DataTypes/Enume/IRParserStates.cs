namespace LC.DataTypes
{
    public static class IRParserStates
    {
        public const int START = -2;
        public const int FIRST_WORD = -1;
        public const int LANG_WORD = 0;
        public const int LANG_SPACE = 1;
        public const int DEFAULT = -4;
        public const int SECOND_WORD = 2;
        public const int NAMESPACE = -3;
    }
}