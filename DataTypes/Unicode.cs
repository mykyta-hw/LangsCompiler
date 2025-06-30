namespace LC.DataTypes
{
    public struct Word
    {
        private U[] val;

        public int Length
        {
            get => val.Length; 
        }
        public U this[int index]
        {
            get { return val[index]; }
            set { val[index] = value; }
        }
        public Word() { val = new U[0]; }
        public Word(params U[] a) { val = a; }
        public Word(Word a, params U[] b) 
        { 
            val = new U[a.Length + b.Length]; 
            for ( int i = 0; i < a.Length; i++) { val[i] = a[i]; } 
            int c = a.Length;
            for ( int i = 0; i < b.Length; i++) { val[i + c] = b[i]; } 
        }
        public static Word operator +(Word a, Word b)
        {
            U[] w = new U[a.Length + b.Length]; 
            for ( int i = 0; i < a.Length; i++) { w[i] = a[i]; } 
            int c = a.Length;
            for ( int i = 0; i < b.Length; i++) { w[i + c] = b[i]; } 
            return new Word(w);
        }
        public static Word operator +(Word a, U b) => new Word(a, b);
        public static bool operator ==(Word a, Word b)
        {
            if (a.Length == b.Length)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] == b[i]) continue;
                    else return false;
                }
                return true;
            }
            return false;
        }
        public static bool operator !=(Word a, Word b)
        {
            if (a.Length == b.Length)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] == b[i]) continue;
                    else return true;
                }
                return false;
            }
            return true;
        }
        public static explicit operator string(Word a) 
        {
            string b = "";
            for (int i = 0; i < a.Length; i++)
            {
                var u = a[i];
                if (((u.A | (u.B << 8)) | (u.C << 16)) < 0x110000)
                    b += char.ConvertFromUtf32((u.A | (u.B << 8)) | (u.C << 16));
                else 
                    b += " ";
                // if (a[i].C == 0)
                // {
                //     b += (char)((a[i].B << 8) | a[i].A);
                // }
            }
            return b;
        }
        public override bool Equals(object a)
        {
            if (a == null || !(a is Word))
                return false;
            Word b = (Word)a;
            if (val.Length == b.Length)
            {
                for (int i = 0; i < val.Length; i++)
                {
                    if (val[i] == b[i]) continue;
                    else return false;
                }
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            int hash = Int32.MaxValue;
            bool a = true;
            for (int i = 0; i < val.Length; i++)
            {
                if (a) { hash ^= val[i].GetHashCode(); a = false; }
                else { hash ^= val[i].GetHashCode() << 8; a = true; }
            }
            return hash;
        }
    }
    public struct U
    {
        public byte A { get; set; }
        public byte B { get; set; }
        public byte C { get; set; }
        public U() { A = 0; B = 0; C = 0; }
        public U(byte a) { A = a; B = 0; C = 0; }
        public U(byte a, byte b) { A = a; B = b; C = 0; }
        public U(byte a, byte b, byte c) { A = a; B = b; C = c; }
        public static explicit operator U(byte a) => new U(a);
        public static bool operator ==(U a, U b) => a.A == b.A && a.B == b.B && a.C == b.C ;
        public static bool operator !=(U a, U b) => a.A != b.A || a.B != b.B || a.C != b.C ;
        public override int GetHashCode() => (A | (B << 8)) | (C << 16);
        public override bool Equals(object a) 
        {
            if (a == null || !(a is U))
                return false;
            else
            {
                U temp = (U)a;
                return this.A == temp.A && this.B == temp.B && this.C == temp.C;
            }
        }
    }
}