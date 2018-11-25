using System;

namespace TestApplication
{
    class Test
    {
        public int TestFunction1(int x)
        {
            if (x < 1)
            {
                x++;
            }
            return x;
        }

        public int TestFunction2(int x)
        {
            if (x < 1)
            {
                return x + 1;
            }
            return x;
        }

        public void TestFunction3(int x)
        {
            if (x < 1)
            {
                x++;
            }
            Console.WriteLine(x);
        }

        public void TestFunction4(int x)
        {
            if (x < 1)
            {
                return;
            }
            Console.WriteLine(x);
        }

        public int TestFunction5(int x)
        {
            if (x < 0)
            {
                return -x;
            }
            else
            {
                return x;
            }
        }
    }
}