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
using MetroFramework.Components;
using MetroFramework.Forms;

namespace Forms
{
    public partial class AddUser : Form
    {

        MySqlCommand command;
        MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=Barbershop;password=147852369;charset=utf8;");
        public AddUser()
        {
            InitializeComponent();
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
       //     AddRanks();

        }

       /* public void AddRanks()
        {
            string sql = "SELECT Rank FROM barbershop.ranks;";
            OpenConnection();
            command = new MySqlCommand(sql,conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString());
            }
            reader.Close();
            CloseConnection();

        }*/

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

        private void AddUser_Load(object sender, EventArgs e)
        {

        }

        private void AddUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Surname = textBox1.Text.ToString();
                string Name = textBox2.Text.ToString();
                string Mid_name = textBox3.Text.ToString();
                string Phone = textBox4.Text.ToString();
          //      int count_vis = int.Parse(textBox5.Text.ToString());
                DateTime Date = dateTimePicker1.Value.Date;     
          //    int Score = int.Parse(textBox6.Text.ToString());
          //    string Rank = comboBox1.Text.ToString();

                conn.Open();

            //    string convertRankToIdRank = "SELECT IdRank FROM barbershop.ranks WHERE Rank='" + Rank + "';";
                string sql_max_id = "SELECT MAX(idClients) FROM barbershop.clients;";
                
            //    MySqlCommand command_convert = new MySqlCommand(convertRankToIdRank, conn);
                MySqlCommand command_max_id = new MySqlCommand(sql_max_id, conn);

                //   string id_Rank = command_convert.ExecuteScalar().ToString();
                int max_id = 0;
                try
                {
                    max_id = Convert.ToInt32(command_max_id.ExecuteScalar());
                }
                catch (Exception)
                {
                    max_id = 0;
                }
                string insert_into = "INSERT INTO `barbershop`.`clients` (`idClients`, `Surname`, `Name`, `Mid_name`," + 
        " `Modile_phone`, `Visits`, `Date_of_last_vis`, `Score`, `IdRank`) VALUES ('" + (max_id + 1) + "', '" + Surname + "', '" + Name + "', '" + Mid_name + "'," + "" +
        " '" + Phone + "', '" + 0 + "', '" + Date.ToString("yyyy/MM/dd") + "', '" + 0 + "', '1');";

                MySqlCommand command_insert = new MySqlCommand(insert_into, conn);
                command_insert.ExecuteScalar();
                MessageBox.Show("Клиент добавлен", "Сообщение");
                conn.Close();

                Clients Back = new Clients();
                this.Hide();
                Back.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                throw;
            }


          
      

            


            ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Clients obj = new Clients();
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
