namespace TestApplication
{
    class Test
    {
        public int TestFunction(int x)
        {
            switch (x)
            {
                case 0:
                    x++;
                    break;
                case 1:
                    {
                        x++;
                        break;
                    }
                case 2:
                    return 1;
                case 3:
                    {
                        return 3;
                    }
            }

            return x;
        }
    }
}