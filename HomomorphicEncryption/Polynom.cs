using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace HomomorphicEncryption
{
    class Polynom
    {
        int[] polynomCoefs;
        public Polynom(int coefs)
        {
            polynomCoefs = new int[coefs];
        }
        public Polynom(int number, int nBase)
        {
            List<int> polynomCoefsList = new List<int>();
            int rem = number;
            while(rem !=0)
            {
                polynomCoefsList.Add(rem % nBase);
                rem = rem / nBase;
            }
            polynomCoefs = polynomCoefsList.ToArray();
        }
        public Polynom(int[] coefs)
        {
            polynomCoefs = (int[]) coefs.Clone();
        }
        public override string ToString()
        {
            string ret = "";
            for(int i = 0; i < polynomCoefs.Length; i++)
            {
                ret = polynomCoefs[i] + "x^" + i + " " + ret;
            }
            return ret;
        }
        public Polynom Composition(Polynom p2)
        {
            Polynom resultPolynom = new Polynom((p2.polynomCoefs.Length - 1) * (this.polynomCoefs.Length - 1) + 1);
            for (int i = 0; i < this.polynomCoefs.Length; i++)
            {
                resultPolynom += (p2 ^ i) * this.polynomCoefs[i];
            }
            return resultPolynom;
        }
        public int Value(int x)
        {
            int result = 0;
            for (int i = 0; i < polynomCoefs.Length; i++)
            {
                result += Convert.ToInt32(Math.Pow(x, i)) * polynomCoefs[i];
            }
            return result;
        }
        public static Polynom operator *(Polynom A, Polynom B)
        {
            int maxResultLength = (A.polynomCoefs.Length - 1) + (B.polynomCoefs.Length - 1) + 1;
            Polynom C = new Polynom(maxResultLength);
            for (int i = 0; i < A.polynomCoefs.Length; i++)
            {
                for (int k = 0; k < B.polynomCoefs.Length; k++)
                {
                    C.polynomCoefs[i + k] += A.polynomCoefs[i] * B.polynomCoefs[k];
                }
            }
            return C;
        }

        public static Polynom operator +(Polynom A, Polynom B)
        {
            if(B.polynomCoefs.Length > A.polynomCoefs.Length)
            {
                Polynom tmp = A;
                A = B;
                B = tmp;
            }
            Polynom C = new Polynom(A.polynomCoefs.Length);
            for (int i = 0; i < C.polynomCoefs.Length; i++)
            {
                if(i < B.polynomCoefs.Length)
                {
                    C.polynomCoefs[i] = A.polynomCoefs[i] + B.polynomCoefs[i];
                }
                else
                {
                    C.polynomCoefs[i] = A.polynomCoefs[i];
                }
            }
            return C;
        }

        public static Polynom operator *(Polynom A, int number)
        {
            Polynom B = new Polynom(A.polynomCoefs.Length);
            for (int i = 0; i < B.polynomCoefs.Length; i++)
            {
                    B.polynomCoefs[i] = A.polynomCoefs[i] * number;
            }
            return B;
        }
        public static Polynom operator ^(Polynom A, int power)
        {
            // пох что оператор ^ должен делать XOR, тут это возведение в степень
            Polynom B;
            if(power !=0)
            {
                B = new Polynom(A.polynomCoefs.Length * power);
            }
            else 
            { 
                B = new Polynom(1);
            }
            B.polynomCoefs[0] = 1;
            for (int i = 0; i < power; i++ )
            {
                B = B * A;
            }
            return B;
        }
    }
}
