namespace LC.DataTypes.SyntaxRules
{
    public class Root
    {
        /*
        00-jump                                   --int = index instruction    --int,int = data ints, set index pointer: data ints
        01-jump if true                           --int = index instruction
        02-jump if false                          --int = index instruction
        
        03-literal equal                          --int = index lteral
        04-type equal                             --int = index TokenType
        05-byte equal                             --int = index byte
        06-reset equal

        07-create object                          --int = index IR patern object
        08-write console error                  

        09-save int in var                        --int = offset bytes
        10-save word in var                       --int = offset bytes
        11-save byte in var                       --int = offset bytes
        12-save token in var                      --int = offset bytes
        13-save object in last object in stack    --int = offset bytes

        14-push object in stack
        15-pop object from stack

        16-save tokens position
        17-return to last tokens position
        18-set data pointer                       --int = index data
        19-get next token

        20-save last equal
        21-clear equal history
        
        22-create array                           --int = index IR object
        23-add array to storage
        24-add to array byte
        25-add to array int
        26-add to array word
        27-add to array token
        28-add to array object

        29-stop
        30-save root object
        */
        public List<byte> Instructions { get; set; }
        public List<int> Data { get; set; }
        public List<Word> Literals { get; set; }
        public List<TokenType> Types { get; set; }
        public List<byte> Bytes { get; set; }
    }
}