namespace StrykerNet.UnitTest.Mutants.TestResources
{
    class TestClass
    {
        void TestMethod()
        {
            int i = 0;
            if ((System.Environment.GetEnvironmentVariable("ActiveMutation") == "0" ? (i - 8) : (i + 8)) == 8)
            {
                if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "1")
                {
                    i = i - 1;
                }
                else
                {
                    i = i + 1;
                }
                if ((System.Environment.GetEnvironmentVariable("ActiveMutation") == "2" ? (i - 8) : (i + 8)) == 9)
                {
                    if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "3")
                    {
                        i = i - 1;
                    }
                    else
                    {
                        i = i + 1;
                    }
                };
            }
            else
            {
                if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "4")
                {
                    i = i - 3;
                }
                else
                {
                    i = i + 3;
                }
                if (i == (System.Environment.GetEnvironmentVariable("ActiveMutation") == "5" ? (i - i) : (i + i)) - 8)
                {
                    if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "6")
                    {
                        i = i - 1;
                    }
                    else
                    {
                        i = i + 1;
                    }
                };
            }
            if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "7")
            {
                i = i - i;
            }
            else
            {
                i = i + i;
            }
        }
    }
}