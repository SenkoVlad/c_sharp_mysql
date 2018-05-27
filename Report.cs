using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class Report : Form
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=Barbershop;password=147852369;charset=utf8;");

        public Report()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenConnection();
            DateTime thisDay = DateTime.Today;
            int visits = 0;
            double score = 0;
            double Consumables = 0;
            string sql = "select Money, Visitors, Date,Consumables from report where " +
                "TIMESTAMPDIFF(DAY,Date,'" + thisDay.ToString("yyyy.MM.dd") + "') < "+Convert.ToInt16(comboBox1.Text)+";";
            MySqlCommand command = new MySqlCommand(sql,conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Consumables+= Convert.ToDouble(reader[3]);
                score += Convert.ToDouble(reader[0]);
                visits+=Convert.ToInt16(reader[1]);
            }
            label5.Text = score.ToString();
            label7.Text = visits.ToString();
            label10.Text = (Consumables / Convert.ToInt32(comboBox1.Text)).ToString();
            
            if (visits > 0)
            {
                label4.Text = Math.Round((score / visits), 2).ToString();
            }
            else
            {
                MessageBox.Show("За данный период не было клиентов", "Отчёт");
            }
            CloseConnection();
            reader.Close();
        }


        public void OpenConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 obj = new Form1();
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Report_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Edit_Reprot obj = new Edit_Reprot();
            obj.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateTime thisDay = DateTime.Today;
            string sql_report = "INSERT INTO `barbershop`.`report` (`Date`, `Visitors`, `Money`,`Consumables`) VALUES ('" + thisDay.ToString("yyyy.MM.dd") + "'," +
   " '0', '0', '"+Convert.ToDouble(textBox1.Text)+"');";
            try
            {
                OpenConnection();
                MySqlCommand command = new MySqlCommand(sql_report, conn);
                command.ExecuteScalar();
                CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
                throw;
            }

        }
    }
}
