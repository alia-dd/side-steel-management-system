using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Side_Steel_Management_System
{
    static class Program
    {
         public static String firstname = "";
         public static String middlname = "";
         public static String usertp = "";
         public static int USRid = 0;
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new loginFr());
        }
    }
}
