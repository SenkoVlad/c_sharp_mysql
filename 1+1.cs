using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class _1_1 : Form
    {
        string[] Names = null;
        string[] Names1 = null;
        MySqlCommand command;
        MySqlDataReader reader;
        double price1 = 0;
        double price2= 0;

        double cash_back1 = 0;
        double cash_back2 = 0;
        string sql_clients = null;
        string sql_category = null;
        MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=Barbershop;password=147852369;charset=utf8;");

        public _1_1()
        {
            InitializeComponent();
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
            AddService_Clients();
        }


        public void AddService_Clients()
        {
            sql_clients = "select barbershop.clients.Surname, Name, Mid_name from barbershop.clients;";
            sql_category = "SELECT category FROM barbershop.services;";
            OpenConnection();

            MySqlCommand command_category = new MySqlCommand(sql_category, conn);
            command = new MySqlCommand(sql_clients, conn);

            reader = command_category.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader[0].ToString());
                comboBox4.Items.Add(reader[0].ToString());
            }
            reader.Close();

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString() + " " + reader[1].ToString() + " "
                    + reader[2].ToString());
                comboBox3.Items.Add(reader[0].ToString() + " " + reader[1].ToString() + " "
                    + reader[2].ToString());
            }
            reader.Close();

            CloseConnection();
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

        private void _1_1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Service obj = new Service();
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 obj = new Form1();
            obj.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label6.BorderStyle = BorderStyle.FixedSingle;
            label7.BorderStyle = BorderStyle.FixedSingle;
            Names = comboBox1.Text.ToString().Split(' ');
            Names1 = comboBox3.Text.ToString().Split(' ');
            button4.Text = "Добавить на счёт";

            OpenConnection();

            sql_category = "SELECT price FROM barbershop.services where category= '" + comboBox2.Text + "';";
            command = new MySqlCommand(sql_category, conn);
            price1 = Convert.ToDouble(command.ExecuteScalar());
            label6.Text = price1.ToString();
            label9.Text = (price1 - 5).ToString();

            sql_category = "SELECT price FROM barbershop.services where category= '" + comboBox4.Text + "';";
            command = new MySqlCommand(sql_category, conn);
            price2 = Convert.ToDouble(command.ExecuteScalar());
            label7.Text = price2.ToString();
            label10.Text = (price2 - 5).ToString();

            sql_category = "SELECT Discount FROM barbershop.ranks where barbershop.ranks.Rank=" +
     "(SELECT barbershop.ranks.Rank FROM barbershop.clients INNER JOIN barbershop.ranks " +
     "using(IdRank) where Surname='" + Names[0] + "' and Name='" + Names[1] + "' and Mid_name='" + Names[2] + "');";
            command = new MySqlCommand(sql_category, conn);
            int discount1 = Convert.ToInt32(command.ExecuteScalar());
            cash_back1 = (price1-5) * discount1 / 100;


            sql_category = "SELECT Discount FROM barbershop.ranks where barbershop.ranks.Rank=" +
   "(SELECT barbershop.ranks.Rank FROM barbershop.clients INNER JOIN barbershop.ranks " +
   "using(IdRank) where Surname='" + Names1[0] + "' and Name='" + Names1[1] + "' and Mid_name='" + Names1[2] + "');";
            command = new MySqlCommand(sql_category, conn);
            int discount2 = Convert.ToInt32(command.ExecuteScalar());
            cash_back2 = (price2-5) * discount2 / 100;
            button4.Text = button4.Text + " " + cash_back1.ToString()+ " и " + cash_back2.ToString();


            CloseConnection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                OpenConnection();

                sql_category = "update barbershop.clients set Date_of_last_vis='" + dateTimePicker1.Value.Date.ToString("yyyy/MM/dd")
               + "', Visits=Visits+1,Score=Score+" + cash_back1 + " where Surname='" + Names[0] + "' and Name='" + Names[1] + "' and Mid_name='" + Names[2] + "';";
                command = new MySqlCommand(sql_category, conn);
                command.ExecuteScalar();

                sql_category = "update barbershop.clients set Date_of_last_vis='" + dateTimePicker1.Value.Date.ToString("yyyy/MM/dd")
                + "', Visits=Visits+ 1,Score=Score+" + cash_back2 + " where Surname='" + Names1[0] + "' and Name='" + Names1[1] + "' and Mid_name='" + Names1[2] + "';";
                command = new MySqlCommand(sql_category, conn);
                command.ExecuteScalar();
                double total = price1 + price2;
                string sql_report = "INSERT INTO `barbershop`.`report` (`Date`, `Visitors`, `Money`,`Consumables`) VALUES ('" + dateTimePicker1.Value.Date.ToString("yyyy/MM/dd") + "'," +
" '2', '"+ total + "', 0);";
                MySqlCommand command_report = new MySqlCommand(sql_report, conn);
                command_report.ExecuteScalar();

                MessageBox.Show("Кэщ-Бэк добавлен. Данные обновлены ;)", "Сообщение");

                CloseConnection();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
                throw;
            }

        }

        private void _1_1_Load(object sender, EventArgs e)
        {

        }
    }
}
