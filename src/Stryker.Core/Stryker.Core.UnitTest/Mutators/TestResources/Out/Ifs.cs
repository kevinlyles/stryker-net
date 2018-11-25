using System;

namespace TestApplication
{
    class Test
    {
        public int TestFunction1(int x)
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
                        return default(int);
                    }
                }
                else
                {
                    if (x < 1)
                    {
                        x++;
                    }
                }
            }
            return x;
        }

        public int TestFunction2(int x)
        {
            if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "3")
            {
                if (x < 1)
                { }
            }
            else
            {
                if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "2")
                {
                    if (x < 1)
                    {
                        return default(int);
                    }
                }
                else
                {
                    if (x < 1)
                    {
                        return x + 1;
                    }
                }
            }
            return x;
        }

        public void TestFunction3(int x)
        {
            if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "5")
            {
                if (x < 1)
                { }
            }
            else
            {
                if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "4")
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
                        x++;
                    }
                }
            }
            Console.WriteLine(x);
        }

        public void TestFunction4(int x)
        {
            if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "7")
            {
                if (x < 1)
                { }
            }
            else
            {
                if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "6")
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

        public int TestFunction5(int x)
        {
            if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "9")
            {
                if (x < 0)
                {
                    return -x;
                }
                else
                {
                    return default(int);
                }
            }
            else
            {
                if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "8")
                {
                    if (x < 0)
                    {
                        return default(int);
                    }
                    else
                    {
                        return x;
                    }
                }
                else
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
    }
}