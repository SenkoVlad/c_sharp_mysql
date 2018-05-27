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
    public partial class Стандарт : Form
    {
        string[] Names = null;
        MySqlCommand command;
        double price = 0;
        double cash_back = 0;
        string sql_clients = null;
        string sql_category = null;
        MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=Barbershop;password=147852369;charset=utf8;");

        public Стандарт()
        {
            InitializeComponent();
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            AddService_Clients();

        }


        public void AddService_Clients()
        {
            sql_clients = "select barbershop.clients.Surname, Name, Mid_name from barbershop.clients;";
            sql_category = "SELECT category FROM barbershop.services;";
            OpenConnection();

            MySqlCommand command_category = new MySqlCommand(sql_category, conn);
            command = new MySqlCommand(sql_clients, conn);

            MySqlDataReader reader_category = command_category.ExecuteReader();
            while (reader_category.Read())
            {
                comboBox2.Items.Add(reader_category[0].ToString());
            }
            reader_category.Close();

            MySqlDataReader reader_clients = command.ExecuteReader();
            while (reader_clients.Read())
            {
                comboBox1.Items.Add(reader_clients[0].ToString() + " " + reader_clients[1].ToString() + " "
                    + reader_clients[2].ToString());
            }
            reader_clients.Close();

            CloseConnection();
        }

    
     

        private void Стандарт_Load(object sender, EventArgs e)
        {

        }

        private void Стандарт_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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

        private void button1_Click(object sender, EventArgs e)
        {
            label8.BorderStyle = BorderStyle.FixedSingle;
            OpenConnection();
            Names = comboBox1.Text.ToString().Split(' ');
            button4.Text = "Добавить на счёт";

            sql_category = "SELECT price FROM barbershop.services where category='"+comboBox2.Text+"';";
            command = new MySqlCommand(sql_category, conn);
            price=Convert.ToDouble(command.ExecuteScalar());
            label8.Text = price.ToString();

            sql_category = "SELECT Discount FROM barbershop.ranks where barbershop.ranks.Rank=" +
                "(SELECT barbershop.ranks.Rank FROM barbershop.clients INNER JOIN barbershop.ranks " +
                "using(IdRank) where Surname='" + Names[0] + "' and Name='" + Names[1] + "' and Mid_name='" + Names[2] + "');";
            command = new MySqlCommand(sql_category, conn);
            int discount =Convert.ToInt32(command.ExecuteScalar());
            cash_back =     price * discount / 100;
            button4.Text = button4.Text+ " "+(cash_back).ToString();//слишком большая скидка


            CloseConnection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 obj = new Form1();
            obj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Service obj = new Service();
            obj.Show();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                OpenConnection();
       
                string sql_report = "INSERT INTO `barbershop`.`report` (`Date`, `Visitors`, `Money`,`Consumables`) VALUES ('" + dateTimePicker1.Value.Date.ToString("yyyy/MM/dd") + "'," +
    " '1', '"+price+"', 0);";
                MySqlCommand command_report = new MySqlCommand(sql_report,conn);
                command_report.ExecuteScalar();


                sql_category = "update barbershop.clients set Date_of_last_vis='" + dateTimePicker1.Value.Date.ToString("yyyy/MM/dd") + "', Visits=Visits+" +
    "" + 1 + ",Score=Score+" + cash_back + " where Surname='" + Names[0] + "' and Name='" + Names[1] + "' and Mid_name='" + Names[2] + "';";
                command = new MySqlCommand(sql_category, conn);
                command.ExecuteScalar();

                MessageBox.Show("Кэщ-Бэк добавлен. Данные обновлены ;)", "Сообщение");

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
