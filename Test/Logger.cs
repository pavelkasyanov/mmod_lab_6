using System.IO;

namespace Test
{
    public class Logger
    {
        public static void Write(string str)
        {
            using (StreamWriter file = new System.IO.StreamWriter("log.txt", true))
            {
                file.WriteLine(str);
            }
        }
    }
}
