using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polygon_Triangulation {
    public partial class mainForm : Form {
        public mainForm() {
            InitializeComponent();
        }

        private void using_monotone_btn(object sender, EventArgs e) {
            Hide();
            main._playForm = new playForm("monotone");
        }

        private void ear_clip_btn_Click(object sender, EventArgs e) {
            Hide();
            main._playForm = new playForm("ear clip");
        }
    }
}