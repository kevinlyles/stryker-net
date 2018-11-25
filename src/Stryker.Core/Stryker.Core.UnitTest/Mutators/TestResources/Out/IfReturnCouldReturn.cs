namespace TestApplication
{
    class Test
    {
        public int TestFunction(int x)
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
                        return x + 1;
                    }
                }
            }
            return x;
        }
    }
}