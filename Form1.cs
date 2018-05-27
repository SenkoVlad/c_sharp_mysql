using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class Form1 : Form
    {

        MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=Barbershop;password=147852369;");
        MySqlConnection conn2 = new MySqlConnection("server=localhost;user=root;database=Barbershop;password=147852369;");

        MySqlCommand command;
        MySqlDataReader reader;

        public Form1()
        {
            InitializeComponent();
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            LoadData();
        }

        private void LoadData()
        {
            conn.Open();
            conn2.Open();
            string sql = "SELECT idClients,IdRank,Visits FROM barbershop.clients;";
            command = new MySqlCommand(sql, conn);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string update = null;
                int id = Convert.ToInt16(reader[0]);
                int visits =Convert.ToInt16(reader[2]);
                if (visits >= 0 && visits < 5)
                {
                    update = "update barbershop.clients set IdRank=1 where idClients="+id+";";
                }
                else if (visits >= 5 && visits < 10)
                {
                    update = "update barbershop.clients set IdRank=2 where idClients=" + id + ";";
                }
                else if (visits >= 10 && visits < 15)
                {
                    update = "update barbershop.clients set IdRank=3 where idClients=" + id + ";";
                }
                else if (visits >= 15 && visits < 20)
                {
                    update = "update barbershop.clients set IdRank=4 where idClients=" + id + ";";
                }
                else
                {
                    update = "update barbershop.clients set IdRank=5 where idClients=" + id + ";";
                }
                command = new MySqlCommand(update, conn2);
                command.ExecuteScalar();
            }
            reader.Close();
            conn.Close();
            conn2.Clone();
        }



        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void добавитьКлиентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddUser User = new AddUser();
            User.Show();
        }

     

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Service obj = new Service();
            obj.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Clients obj = new Clients();
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Report obj = new Report();
            obj.Show();
        }
    }
}
