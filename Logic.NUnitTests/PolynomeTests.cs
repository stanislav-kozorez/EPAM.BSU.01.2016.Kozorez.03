using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Logic.NUnitTests
{
    [TestFixture]
    public class PolynomeTests
    {
        [TestCase(Result = "- 4X^4 - 6X^3 - 4,56X + 14")]
        public string ToStringTest()
        {
            return new Polynome(0, 0, -4, -6, 0, -4.56, 14).ToString();
        }

        [TestCase(Result = 6)]
        public int PowerTest()
        {
            return new Polynome(0, 2, 0, 5, -5, 0, -53.6, -0.53).Power;
        }


        public IEnumerable<TestCaseData> EqualsTestData
        {
            get
            {
                yield return new TestCaseData(new Polynome(0, 0, -4, -6, 0, -4.56, 14), new Polynome(0, 0, -4, -6, 0, -4.56, 14)).Returns(true);
                yield return new TestCaseData(new Polynome(0, 0, -4, -6, 0, -4.56, 14), new Polynome(0, 0, -4, -6, 0, -4.6, 14)).Returns(false);
                yield return new TestCaseData(new Polynome(0, 0, -4, -6, 0), new Polynome(0, 0, 0, 0, 0, -4, -6, 0)).Returns(true);
                yield return new TestCaseData(new Polynome(0, 0, -4, -6), null).Returns(false);
            }
        }

        [Test, TestCaseSource(nameof(EqualsTestData))]
        public bool EqualsTest(Polynome p1, Polynome p2)
        {
            return p1.Equals(p2);
        }

        public IEnumerable<TestCaseData> GetHashCodeTestData
        {
            get
            {
                yield return new TestCaseData(new Polynome(0, 0, -4, -6, 0, -4.56, 14), new Polynome(0, 0, -4, -6, 0, -4.56, 14)).Returns(true);
                yield return new TestCaseData(new Polynome(0, 0, -3, -6, 0, -4.56, 14), new Polynome(0, 0, -4, -6, 0, -4.56, 14)).Returns(false);
                yield return new TestCaseData(new Polynome(0, 0, -4, -6, 0), new Polynome(0, 0, 0, 0, 0, -4, -6, 0)).Returns(true);
            }
        }

        [Test, TestCaseSource(nameof(GetHashCodeTestData))]
        public bool GetHashCodeTest(Polynome p1, Polynome p2)
        {
            return p1.GetHashCode() == p2.GetHashCode();
        }

        public IEnumerable<TestCaseData> AddTestData
        {
            get
            {
                yield return new TestCaseData(new Polynome(0, 0, -4, -6, 0, -4.5, 14), new Polynome(0, 0, -4, -6, 0, -4.5, 14)).Returns(new Polynome(-8, -12, 0, -9, 28));
                yield return new TestCaseData(new Polynome(0, 0, -3, -6, 0, -4.5, 14), new Polynome(-7, 0, 11)).Returns(new Polynome(-3, -6, -7, -4.5, 25));
                
            }
        }

        [Test, TestCaseSource(nameof(AddTestData))]
        public Polynome AddTest(Polynome p1, Polynome p2)
        {
            return p1 + p2;
        }

        public IEnumerable<TestCaseData> SubstractTestData
        {
            get
            {
                yield return new TestCaseData(new Polynome(0, 0, -4, -6, 0, -4.5, 14), new Polynome(-4, 0, 0, 0, 0, 12, 14)).Returns(new Polynome(4,0,-4,-6, 0, -16.5, 0));
                yield return new TestCaseData(new Polynome(0, 0, -4, -6, 0), new Polynome(0, 0, 0, 0, 0, 4, 6, 0)).Returns(new Polynome(-8, -12, 0));
            }
        }

        [Test, TestCaseSource(nameof(SubstractTestData))]
        public Polynome SubstractTest(Polynome p1, Polynome p2)
        {
            return p1 - p2;
        }

        public IEnumerable<TestCaseData> MultiplyTestData
        {
            get
            {
                yield return new TestCaseData(new Polynome(3, 0, -4), new Polynome(2, 0, 0, -1)).Returns(new Polynome(6, 0, -8, -3, 0, 4));
            }
        }

        [Test, TestCaseSource(nameof(MultiplyTestData))]
        public Polynome MultiplyTest(Polynome p1, Polynome p2)
        {
            return p1 * p2;
        }

    }
}
