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
        public double[] polynomCoefs;
        public Polynom(int coefs)
        {
            // конструктор
            polynomCoefs = new double[coefs];
        }
        public Polynom(int number, int nBase)
        {
            // еще конструктор
            List<double> polynomCoefsList = new List<double>();
            int rem = number;
            while(rem !=0)
            {
                polynomCoefsList.Add(rem % nBase);
                rem = rem / nBase;
            }
            polynomCoefs = polynomCoefsList.ToArray();
        }
        public Polynom(double[] coefs)
        {
            // тоже конструктор
            polynomCoefs = (double[])coefs.Clone();
        }
        public override string ToString()
        {
            // вывод в строку
            string ret = "";
            for(int i = 0; i < polynomCoefs.Length; i++)
            {
                ret = Math.Round(polynomCoefs[i], 2) + "x^" + i + " " + ret;
            }
            return ret;
        }
        public Polynom Composition(Polynom p2)
        {
            // вычисление композиции
            Polynom resultPolynom = new Polynom((p2.polynomCoefs.Length - 1) * (this.polynomCoefs.Length - 1) + 1);
            for (int i = 0; i < this.polynomCoefs.Length; i++)
            {
                resultPolynom += (p2 ^ i) * this.polynomCoefs[i];
            }
            return resultPolynom;
        }
        public double Value(int x)
        {
            // вычисление значения от аргумента х
            double result = 0;
            for (int i = 0; i < polynomCoefs.Length; i++)
            {
                result += Convert.ToInt64(Math.Pow(x, i)) * polynomCoefs[i];
            }
            return result;
        }
        public static Polynom operator *(Polynom A, Polynom B)
        {
            // умножение
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
            // сложение
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

        public static Polynom operator -(Polynom A, Polynom B)
        {
            // вычитание
            //лень нормально делать
            for (int i = 0; i < B.polynomCoefs.Length; i++)
            {
                B.polynomCoefs[i] = -B.polynomCoefs[i];
            }
            if (B.polynomCoefs.Length > A.polynomCoefs.Length)
            {
                Polynom tmp = A;
                A = B;
                B = tmp;
            }
            Polynom C = new Polynom(A.polynomCoefs.Length);
            for (int i = 0; i < C.polynomCoefs.Length; i++)
            {
                if (i < B.polynomCoefs.Length)
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

        public static Polynom operator *(Polynom A, double number)
        {
            // умножение на число
            Polynom B = new Polynom(A.polynomCoefs.Length);
            for (int i = 0; i < B.polynomCoefs.Length; i++)
            {
                    B.polynomCoefs[i] = A.polynomCoefs[i] * number;
            }
            return B;
        }

        public static Polynom[] operator /(Polynom A, Polynom B)
        {
            // деление
            // возвращает массив из результата и остатка
            //хрень короче, можно проще сделать (наверно)
            Polynom tmpA = new Polynom((double[]) A.polynomCoefs.Clone());
            int lengthA = A.polynomCoefs.Length;
            int highCoefA = A.polynomCoefs.Length - 1;
            int highCoefB = B.polynomCoefs.Length - 1;
            Polynom[] C = new Polynom[2];
            C[0] = new Polynom(highCoefA - highCoefB + 1);
            while(highCoefA >= highCoefB)
            {
                if (tmpA.polynomCoefs[highCoefA] != 0)
                {
                    double coef = tmpA.polynomCoefs[highCoefA] / B.polynomCoefs[highCoefB];
                    int exp = highCoefA - highCoefB;
                    Polynom tmp = new Polynom(new double[lengthA]);
                    tmp.polynomCoefs[exp] = coef;
                    C[0] += tmp;
                    tmp = tmp * B;
                    tmpA -= tmp;
                }
                highCoefA -= 1;
            }
            C[1] = tmpA;
            return C;
        }
      
        public static Polynom operator ^(Polynom A, int power)
        {
            // пох что оператор ^ должен делать XOR, тут это возведение в степень
            Polynom B;
            B = new Polynom(1);
            B.polynomCoefs[0] = 1;
            for (int i = 0; i < power; i++ )
            {
                B = B * A;
            }
            return B;
        }

        public static bool operator ==(Polynom A, Polynom B)
        {
            // оператор равенства
            if (A.polynomCoefs.Length != B.polynomCoefs.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < B.polynomCoefs.Length; i++)
                {
                    if (A.polynomCoefs[i] != B.polynomCoefs[i])
                        return false;
                }
            }
            return true;

        }
        public static bool operator !=(Polynom A, Polynom B)
        {
            // оператор неравенства
            return !(A==B);

        }
    }
}
