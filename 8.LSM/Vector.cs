using System;

namespace Matrix_Calculator_F2
{
    internal class Vector
    {
        public int[] VectorComponents { get; set; } = new int[3];
        public int Length { get; private set; }
        public bool EqualsZero
        {
            get { bool f = true; foreach (var item in VectorComponents) if (item != 0) f = false; return f; } 
        }

        public Vector(int[] vectorComponents)
        {
            VectorComponents = vectorComponents;
            Length = vectorComponents.Length;
        }

        public Vector(int length)
        {
            VectorComponents = new int[length];
            Length = length;
        }
        public static Vector operator + (Vector a, Vector b)
        {
            if (a.Length != b.Length)
                throw new Exception("Vectors' length are different!");

            Vector res = new Vector(a.Length);
            for (int i = 0; i < a.Length; i++)
                res[i] = (a[i] + b[i]) % 2;

            return res;
        }
        public int this[int idx]
        {
            get => VectorComponents[idx];
            set => VectorComponents[idx] = value;
        }
        public static bool operator == (Vector a, Vector b)
        {
            if(a.Length != b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i])
                    return false;
            return true;
        }
        public static bool operator != (Vector a, Vector b)
        {
            if (a.Length != b.Length)
                return true;
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i])
                    return true;
            return false;
        }
    }
}
