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
using System.Security.Cryptography;

namespace skripsi
{
    public partial class Login : Form
    {
        private string conn;
        private MySqlConnection connect;
        private bool closed = true;
        private Main main = new Main();
        
        public Login()
        {
            
            InitializeComponent();
            this.MaximizeBox = false;
            style();
        }

        private void style()
        {
            btSignIn.ForeColor = Color.White;
            btSignIn.BackColor = Color.FromArgb(26, 188, 156);
            btSignIn.FlatStyle = FlatStyle.Flat;
            btSignIn.FlatAppearance.BorderColor = Color.FromArgb(26, 188, 156);
            btSignIn.FlatAppearance.BorderSize = 1;
        }

        private void db_connection()
        {
            try
            {
                conn = "Server=127.0.0.1;Database=dbtma;Uid=root;Pwd=;";
                connect = new MySqlConnection(conn);
                closed = false;
                connect.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                MessageBox.Show("Koneksi gagal!");
                closed = true;
            }
        }

        private bool validate_login(string user, string password)
        {
            db_connection();
            if(closed == false)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "Select * from petugas where username=@user and password=MD5(@pass)";
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@pass", password);
                cmd.Connection = connect;
                MySqlDataReader login = cmd.ExecuteReader();

                if (login.Read())
                {
                    main.id_pet(login.GetInt32(0));
                    connect.Close();
                    return true;
                }
                else
                {
                    connect.Close();
                    return false;
                }
            }
            else
            {
                return false;
            }
            
            
        }
               
      

        private void btSignIn_Click(object sender, EventArgs e)
        {
            string user = tbUsername.Text;
            string password = tbPassword.Text;

            if(user == "" || password == "")
            {
                MessageBox.Show("Form tidak boleh kosong");
                return;
            }
            
            bool r = validate_login(user, password);
            if (r)
            {
                main.Visible = true;
                this.Visible = false;
            }
            else if(closed == false && r == false)
                MessageBox.Show("Username atau Password anda salah");
            }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btSignIn.PerformClick();
                
            }
        }

  
                
        }

    }
