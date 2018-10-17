namespace StrykerNet.UnitTest.Mutants.TestResources
{
    class TestClass
    {
        void TestMethod()
        {
            int i = 0;
            if ((System.Environment.GetEnvironmentVariable("ActiveMutation") == "0" ? (i - 8) : (i + 8)) == 8)
            {
                i = (System.Environment.GetEnvironmentVariable("ActiveMutation") == "1" ? (i - 1) : (i + 1));
                if ((System.Environment.GetEnvironmentVariable("ActiveMutation") == "2" ? (i - 8) : (i + 8)) == 9)
                {
                    i = (System.Environment.GetEnvironmentVariable("ActiveMutation") == "3" ? (i - 1) : (i + 1));
                };
            }
            else
            {
                i = (System.Environment.GetEnvironmentVariable("ActiveMutation") == "4" ? (i - 3) : (i + 3));
                if (i == (System.Environment.GetEnvironmentVariable("ActiveMutation") == "5" ? (i - i) : (i + i)) - 8)
                {
                    i = (System.Environment.GetEnvironmentVariable("ActiveMutation") == "6" ? (i - 1) : (i + 1));
                };
            }
            i = (System.Environment.GetEnvironmentVariable("ActiveMutation") == "7" ? (i - i) : (i + i));
        }
    }
}