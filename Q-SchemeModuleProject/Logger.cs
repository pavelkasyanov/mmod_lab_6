using System.IO;

namespace Q_SchemeModuleProject
{
    public class Logger
    {
        public static void Write(string str)
        {
            using (StreamWriter file = new System.IO.StreamWriter("log.txt", true))
            {
                file.Write(str);
            }
        }
    }
}
