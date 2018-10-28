using System;

namespace StrykerNet.UnitTest.Mutants.TestResources
{
    class TestClass
    {
        int Sum(int first, int second)
        {
            int result = (System.Environment.GetEnvironmentVariable("ActiveMutation") == "0" ? (first - second) : (first + second));
            return result;
        }
    }
}
