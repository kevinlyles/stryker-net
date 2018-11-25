using System;

namespace TestApplication
{
    class Test
    {
        public int TestFunction(int x)
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