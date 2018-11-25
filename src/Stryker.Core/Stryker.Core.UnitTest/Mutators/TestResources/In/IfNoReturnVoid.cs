using System;

namespace TestApplication
{
    class Test
    {
        public void TestFunction(int x)
        {
            if (x < 1)
            {
                x++;
            }
            Console.WriteLine(x);
        }
    }
}