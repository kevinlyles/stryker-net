using System;

namespace TestApplication
{
    class Test
    {
        public void TestFunction(int x)
        {
            if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "1")
            {
                if (x < 1)
                { }
            }
            else
            {
                if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "0")
                {
                    if (x < 1)
                    {
                        return;
                    }
                }
                else
                {
                    if (x < 1)
                    {
                        return;
                    }
                }
            }
            Console.WriteLine(x);
        }
    }
}