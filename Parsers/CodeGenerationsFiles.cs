using LC.DataTypes;
using System.Collections.Generic;
using static LC.DataTypes.BCParserStates;
namespace LC.Parsers 
{
    public class CodeGenerationFiles
    {
        private GroupFiles SourceCode = null;
        private int IndexFile = 0;
        private FileCode CurrentFile;
        private Token t = new();
        private bool Stop = false;
        private Lexer Lexer = new();

        public void Parse(ref GroupFiles fs, ref bool stop)
        {
            SourceCode = fs;
            for (; IndexFile < fs.Files.Count; IndexFile++)
            {
                Reset();
                Lexer.Reset();
                CurrentFile = fs.Files[IndexFile];
                File();
            }
            Semantic();
            if (Stop) { stop = true; return; }
        }
        public void File()
        {}
        public void Semantic()
        {}
        public void Reset()
        {}
    }
}