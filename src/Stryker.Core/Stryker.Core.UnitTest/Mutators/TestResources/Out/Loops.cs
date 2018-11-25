namespace TestApplication
{
    class Test
    {
        public int TestFunction(int x)
        {
            if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "1")
            {
                while (x < 1)
                { }
            }
            else
            {
                if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "0")
                {
                    while (x < 1)
                    {
                        return default(int);
                    }
                }
                else
                {
                    while (x < 1)
                    {
                        x++;
                    }
                }
            }
            return x;
        }
    }
}