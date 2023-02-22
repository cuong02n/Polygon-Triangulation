using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace Polygon_Triangulation {
    static class main {
        public static mainForm mainform;
        
        public static playForm _playForm;

        public static string type;
        
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mainform = new mainForm();
            Application.Run(mainform);

        }
    }
}