namespace TestApplication
{
    class Test
    {
        public int TestFunction(int x)
        {
            if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "3")
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
                        { }
                }
            }
            else
            {
                if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "2")
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
                                return default(int);
                            }
                    }
                }
                else
                {
                    if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "1")
                    {
                        switch (x)
                        {
                            case 0:
                                x++;
                                break;
                            case 1:
                                { }
                            case 2:
                                return 1;
                            case 3:
                                {
                                    return 3;
                                }
                        }
                    }
                    else
                    {
                        if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "0")
                        {
                            switch (x)
                            {
                                case 0:
                                    x++;
                                    break;
                                case 1:
                                    {
                                        return default(int);
                                    }
                                case 2:
                                    return 1;
                                case 3:
                                    {
                                        return 3;
                                    }
                            }
                        }
                        else
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
                        }
                    }
                }
            }
            return x;
        }
    }
}