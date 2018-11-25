namespace TestApplication
{
    class Test
    {
        public int TestFunction(int x)
        {
            if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "1")
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
                if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "0")
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