namespace StrykerNet.UnitTest.Mutants.TestResources
{
	public class TestClass
	{
		void TestMethod()
		{
			int a = 1;
			int b = a += 1 + 2;
			b = a *= 2 - 3;
			System.Console.WriteLine(b);
		}
	}
}