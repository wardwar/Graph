using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using MySql.Data.MySqlClient;

namespace skripsi
{
    public partial class formGen : Form
    {
        private string conn;
        private MySqlConnection connect;
        private bool closed = true;
        private DataTable tabel = new DataTable();
        private int id_petugas,id_pos;
        private string jam, menit;

        private List<tma> data = new List<tma>();
        public formGen()
        {
            InitializeComponent();

            btnKirim.ForeColor = Color.White;
            btnKirim.BackColor = Color.FromArgb(26, 188, 156);
            btnKirim.FlatStyle = FlatStyle.Flat;
            btnKirim.FlatAppearance.BorderColor = Color.FromArgb(26, 188, 156);
            btnKirim.FlatAppearance.BorderSize = 1;
            

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
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                MessageBox.Show("Koneksi gagal!");
                closed = true;
            }
        }



        public void passList(List<tma> data)
        {
            this.data = data;
        }

        public void id_pet(int id_petugas)
        {
            this.id_petugas = id_petugas;
        }

        private void formGen_Load(object sender, EventArgs e)
        {
            tabel.Columns.Add("Tanggal");
            tabel.Columns.Add("Waktu");
            tabel.Columns.Add("Tinggi");

            foreach (tma value in data)
            {
                
                if (value.jam < 10 && value.jam >= 0)
                {
                    jam = "0" + value.jam;
                }
                else
                {
                    jam = value.jam.ToString();
                }
                if (value.menit < 10 && value.menit >= 0)
                {
                    menit = "0" + value.menit;
                }
                else
                {
                    menit = value.menit.ToString();
                }
                tabel.Rows.Add(value.date.ToString("dddd, dd-MMMM-yyyy"), jam+":"+menit, value.tinggi);
            }
            dgTMA.DataSource = tabel;
            dgTMA.Columns[0].Width = 120;
        }

        private void btnKirim_Click(object sender, EventArgs e)
        {
            label1.Text = id_petugas.ToString();
            db_connection();
            int count = 0;


            pbInsert.Visible = true;
            pbInsert.Maximum = tabel.Rows.Count;
            if (closed == false)
            {
                MySqlCommand cmd = new MySqlCommand();
                MySqlCommand detail = new MySqlCommand();
                MySqlCommand kirim = new MySqlCommand();
                MySqlCommand pos = new MySqlCommand();

                pos.CommandText = "Select id_pos from petugas where id_petugas = @id_petugas";

                pos.Parameters.Clear();
                pos.Parameters.AddWithValue("@id_petugas", id_petugas);
                pos.Connection = connect;

                MySqlDataReader posRread = pos.ExecuteReader();
                if (posRread.Read())
                {
                    id_pos = posRread.GetInt32(0);
                    posRread.Close();
                }

                kirim.CommandText = "Insert into kirim (id_pos) values(@pos)";

                kirim.Parameters.Clear();
                kirim.Parameters.AddWithValue("@pos", id_pos);
                kirim.Connection = connect;

                int cek_kirim = kirim.ExecuteNonQuery();
                long id_kirim = kirim.LastInsertedId;

                cmd.CommandText = "Insert into tma (tanggal,waktu,tinggi) values(@tanggal,@waktu,@tinggi)";
                detail.CommandText = "insert into detail_tma(id_petugas,id_tma,id_kirim) values(@id_petugas,@id_tma,@id_kirim)";
                foreach (tma value in data)
                {
                    string tanggal = value.date.ToString("yyyy-MM-dd");
                    string waktu = value.jam + ":" + value.menit;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@tanggal", tanggal);
                    cmd.Parameters.AddWithValue("@waktu", waktu);
                    cmd.Parameters.AddWithValue("@tinggi", value.tinggi);
                    cmd.Connection = connect;


                    int insert = cmd.ExecuteNonQuery();
                    long id = cmd.LastInsertedId;

                    detail.Parameters.Clear();
                    detail.Parameters.AddWithValue("@id_petugas", id_petugas);
                    detail.Parameters.AddWithValue("@id_tma", id);
                    detail.Parameters.AddWithValue("@id_kirim", id_kirim);
                    detail.Connection = connect;
                    detail.ExecuteNonQuery();

                    if (insert == 1)
                        count++;
                    pbInsert.Value++;
                }
                if (count == tabel.Rows.Count)
                {
                    MessageBox.Show("Data TMA berhasil dikirim");
                    btnKirim.Enabled = false;
                    pbInsert.Value = 0;
                    pbInsert.Visible = false;
                    connect.Close();
                }
                else
                {
                    MessageBox.Show("Gagal Mengirim data TMA");
                    connect.Close();
                }
            }
        }

    }
}
