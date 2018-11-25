using System;

namespace TestApplication
{
    class Test
    {
        public int TestFunction(int x)
        {
            if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "3")
            {
                try
                {
                    return x;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                { }
            }
            else
            {
                if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "2")
                {
                    try
                    {
                        return x;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        return default(int);
                    }
                }
                else
                {
                    if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "1")
                    {
                        try
                        {
                            return x;
                        }
                        catch (Exception ex)
                        {
                            return default(int);
                        }
                        finally
                        {
                            x++;
                        }
                    }
                    else
                    {
                        if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "0")
                        {
                            try
                            {
                                return default(int);
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            finally
                            {
                                x++;
                            }
                        }
                        else
                        {
                            try
                            {
                                return x;
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            finally
                            {
                                x++;
                            }
                        }
                    }
                }
            }
        }
    }
}