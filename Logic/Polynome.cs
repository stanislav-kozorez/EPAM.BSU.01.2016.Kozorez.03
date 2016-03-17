using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Polynome
    {
        private const byte DEFAULT_CAPACITY = 5;
        private double[] indexes;
        private int power;

        public Polynome(params double[] indexes)
        {
            this.indexes = indexes.Reverse().ToArray() ?? new double[DEFAULT_CAPACITY];
        }

        public int Power { get { return GetPower(); } }

        public double[] Indexes { get { return (double[])indexes.Clone(); } }

        public Polynome Add(Polynome polynome)
        {
            if (polynome == null)
                throw new ArgumentNullException($"{nameof(polynome)} is null");

            int length = Math.Max(this.indexes.Length, polynome.indexes.Length);
            double[] resultArr = new double[length];

            for (int i = 0; i < length; i++)
            {
                resultArr[i] += i < this.indexes.Length ? this.indexes[i] : 0;
                resultArr[i] += i < polynome.indexes.Length ? polynome.indexes[i] : 0;
            }
            
            return new Polynome(resultArr.Reverse().ToArray());
        }

        public Polynome Substract(Polynome polynome)
        {
            if (polynome == null)
                throw new ArgumentNullException($"{nameof(polynome)} is null");

            int length = Math.Max(this.indexes.Length, polynome.indexes.Length);
            double[] resultArr = new double[length];

            for (int i = 0; i < length; i++)
            {
                resultArr[i] += i < this.indexes.Length ? this.indexes[i] : 0;
                resultArr[i] -= i < polynome.indexes.Length ? polynome.indexes[i] : 0;
            }

            return new Polynome(resultArr.Reverse().ToArray());
        }
        public Polynome Multiply(Polynome polynome)
        {
            if (polynome == null)
                throw new ArgumentNullException($"{nameof(polynome)} is null");

            int length = this.indexes.Length + polynome.indexes.Length;
            double[] resultArr = new double[length];

            for (int i = 0; i < this.indexes.Length; i++)
                for (int j = 0; j < polynome.indexes.Length; j++)
                    resultArr[i + j] += this.indexes[i] * polynome.indexes[j];

            return new Polynome(resultArr.Reverse().ToArray());
        }

        public static Polynome operator +(Polynome p1, Polynome p2)
        {
            return p1.Add(p2);
        }

        public static Polynome operator -(Polynome p1, Polynome p2)
        {
            return p1.Substract(p2);
        }

        public static Polynome operator *(Polynome p1, Polynome p2)
        {
            return p1.Multiply(p2);
        }

        public override string ToString()
        {
            string result = string.Empty;

            for (int i = indexes.Length - 1; i >= 0; i--)
                if (indexes[i] != 0)
                {
                    if (i != GetPower() && indexes[i] >= 0)
                        result += "+ ";
                    if (indexes[i] < 0)
                        result += "- ";
                    if (i == 1)
                        result += String.Format("{0}X ", Math.Abs(indexes[i]));
                    else if (i != 0)
                        result += String.Format("{0}X^{1} ", Math.Abs(indexes[i]), i);
                    else
                        result += Math.Abs(indexes[i]);
                }
            return result;
        }

        public override bool Equals(object obj)
        {
            Polynome p;
            if (obj == null)
                return false;
            if (this == obj)
                return true;
            if(obj is Polynome)
            {
                p = (Polynome)obj;
                if (this.Power != p.Power)
                    return false;
                for (int i = this.Power; i >= 0; i--)
                    if (indexes[i] != p.Indexes[i])
                        return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            unchecked
            {
                foreach (var elem in indexes)
                    if (elem != 0)
                        hash = hash ^ elem.GetHashCode();
                hash = hash ^ (DEFAULT_CAPACITY * 200) ^ (Power * 140);
                return hash;
            }
        }

        private int GetPower()
        {
            for (int i = 0; i < indexes.Length; i++)
                if (indexes[i] != 0)
                    power = i;
            return power;
        }

    }
}
