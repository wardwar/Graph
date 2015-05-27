using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace skripsi
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btSignIn_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Visible = true;
            this.Visible = false;
            
        }

    }
}
