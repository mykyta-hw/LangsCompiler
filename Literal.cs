using LC.DataTypes;
namespace LC
{
    public static class KeyWords
    {
        public static Word lang = new Word(new U(108), new U(97), new U(110), new U(103));
        public static Word Namespace = new Word(new U(110), new U(97), new U(109), new U(101), new U(115), new U(112), new U(97), new U(99), new U(101));
        public static Word BoolL = new Word(new U(98), new U(111), new(111), new U(108));
        public static Word BoolH = new Word(new U(66), new U(111), new(111), new U(108));
        public static Word StringH = new Word(new U(83), new U(116), new U(114), new U(105), new U(110), new U(103));
        public static Word StringL = new Word(new U(115), new U(116), new U(114), new U(105), new U(110), new U(103));
        public static Word IntL = new Word(new U(105), new(110), new(116));
        public static Word IntH = new Word(new U(73), new(110), new(116));
        public static Word ByteL = new Word(new U(98), new(121), new(116), new(101));
        public static Word ByteH = new Word(new U(66), new(121), new(116), new(101));
        public static Word NewLine = new Word(new U(78), new U(101), new U(119), new U(76), new U(105), new U(110), new U(101));
        public static Word Name = new Word(new U(78), new U(97), new U(109), new U(101));
        public static Word GroupFiles = new Word(new U(71), new U(114), new U(111), new U(117), new U(112), new U(70), new U(105), new U(108), new U(101), new U(115));
        public static Word GroupLangs = new Word(new U(71), new U(114), new U(111), new U(117), new U(112), new U(76), new U(97), new U(110), new U(103), new U(115));
        public static Word Scenario = new Word(new U(83), new U(99), new U(101), new U(110), new U(97), new U(114), new U(105), new U(111));
        public static Word Input = new Word(new U(73), new U(110), new U(112), new U(117), new U(116));
        public static Word Output = new Word(new U(79), new U(117), new U(116), new U(112), new U(117), new U(116));
        public static U Dot = new U(46);
        public static U Slash = new U(47);
        public static U BckwrdSlash = new U(92);
        public static U Asterisk = new U(42);
    }
}