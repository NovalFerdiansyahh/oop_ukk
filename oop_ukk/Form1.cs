using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.IO;

namespace oop_ukk
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string connection = "server=localhost;user id=root;password=;database=data_laptop";
            string query = "INSERT INTO laptop(merek,seri,jenis_cpu,harga,stock,image)VALUES('" + this.MEREK.Text + "','" + this.SERI.Text + "','" + this.JENISCPU.Text + "','" + this.HARGA.Text + "','" + this.STOCK.Text + "','" + Path.GetFileName(pictureBox1.ImageLocation) + "')"; 
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            MessageBox.Show("Berhasil tersimpan");
            conn.Close();
            File.Copy(imageText.Text, Application.StartupPath + @"\image\" + Path.GetFileName(pictureBox1.ImageLocation));
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connection = "server=localhost;user id=root;password=;database=data_laptop";
            string query = "UPDATE laptop SET MEREK='" + this.MEREK.Text + "',SERI='" + this.SERI.Text + "',JENIS_CPU='" + this.JENISCPU.Text + "',HARGA='" + this.HARGA.Text + "',STOCK='" + this.STOCK.Text + "' WHERE KODE_BARANG='" + this.KODEBARANG.Text + "'";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            MessageBox.Show("Data Berhasil diupdate");
            conn.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connection = "server=localhost;user id=root;password=;database=data_laptop";
            string query = "DELETE FROM laptop WHERE KODE_BARANG='" + this.KODEBARANG.Text + "'";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            MessageBox.Show("Data Berhasil dihapus!!");
            conn.Close();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            string connection = "server=localhost;user id=root;password=;database=data_laptop";
            string query = "SELECT * FROM laptop";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.Add("PICTURE", Type.GetType("System.Byte[]"));

            foreach (DataRow row in dt.Rows)
            {
                row["PICTURE"] = File.ReadAllBytes(Application.StartupPath + @"\Image\" + Path.GetFileName(row["IMAGE"].ToString()));
            }
            dataGridView1.DataSource = dt;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string connection = "server=localhost;user id=root;password=;database=data_laptop";
            MySqlConnection con = new MySqlConnection(connection);
            MySqlDataAdapter da;
            DataTable dt;
            con.Open();
            da = new MySqlDataAdapter("SELECT * FROM laptop WHERE SERI LIKE'" + this.textBox1.Text + "%'", con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dt.Columns.Add("PICTURE", Type.GetType("System.Byte[]"));

            foreach (DataRow row in dt.Rows)
            {
                row["PICTURE"] = File.ReadAllBytes(Application.StartupPath + @"\Image\" + Path.GetFileName(row["IMAGE"].ToString()));
            }
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connection = "server=localhost;user id=root;password=;database=data_laptop";
            string query = "SELECT * FROM laptop";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.Add("PICTURE", Type.GetType("System.Byte[]"));

            foreach (DataRow row in dt.Rows)
            {
                row["PICTURE"] = File.ReadAllBytes(Application.StartupPath + @"\Image\" + Path.GetFileName(row["IMAGE"].ToString()));
            }
            dataGridView1.DataSource = dt;
            KODEBARANG.Enabled = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfd = new OpenFileDialog();
            openfd.Filter = "Image Files(*.jpg;*.jepg;*.gif;) | *.jpg;*.jpeg;*.gif;";
            if(openfd.ShowDialog()==DialogResult.OK)
            {
                imageText.Text = openfd.FileName;
                pictureBox1.Image = new Bitmap(openfd.FileName);
                pictureBox1.ImageLocation = openfd.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String KodeBarang = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            String Merek = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            String Seri = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            String JenisCpu = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            String Harga = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            String Stock = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            this.KODEBARANG.Text = KodeBarang;
            this.MEREK.Text = Merek;
            this.SERI.Text = Seri;
            this.JENISCPU.Text = JenisCpu;
            this.HARGA.Text = Harga;
            this.STOCK.Text = Stock;


        }

        private void KODEBARANG_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
