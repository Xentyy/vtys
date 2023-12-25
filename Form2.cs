using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace vtys
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=vtysProje; user ID=postgres; password=1234");
        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from sehirler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into sehirler (sehirid,ad,nufus) values (@p1, @p2, @p3)", baglanti);
            komut1.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            komut1.Parameters.AddWithValue("@p2", textBox2.Text);
            komut1.Parameters.AddWithValue("@p3", int.Parse(textBox3.Text));
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Sehirler veri ekleme işlemi başarı ile gerçekleşti!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("Delete From sehirler where sehirid=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Sehirler silme işlemi başarılı bir şekilde gerçekteşti!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }


        private void button5_Click_1(object sender, EventArgs e)
        {
            // Kullanıcının girdiği şehir ID'sini alınır.
            int sehirid = int.Parse(textBox1.Text);

            baglanti.Open();

            string sorgu = "SELECT * FROM sehirler WHERE sehirler.sehirid = @p1";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);

            // Specify the parameter type and size based on your database schema
            da.SelectCommand.Parameters.AddWithValue("@p1", sehirid);

            DataSet ds = new DataSet();

            // Specify the table name to avoid errors
            da.Fill(ds, "sehirler");

            dataGridView1.DataSource = ds.Tables[0];

            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update sehirler set ad=@p1,nufus=@p2 where sehirid=@p3", baglanti);
            komut3.Parameters.AddWithValue("@p1", textBox2.Text);
            komut3.Parameters.AddWithValue("@p2", int.Parse(textBox3.Text));
            komut3.Parameters.AddWithValue("@p3", int.Parse(textBox1.Text));
            komut3.ExecuteNonQuery();
            MessageBox.Show("Sehirler guncelleme işlemi başarılı bir şekilde gerçekteşti!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();
        }
    }
}
