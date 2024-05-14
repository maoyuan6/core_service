namespace Service
{
    public class Class1
    {
        private string sql = string.Empty;

        private void test()
        {
            sql = " insert Enterprise_BlackList values('@vip.163.com',GETDATE())   "; 
        }

    }
}