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
    public partial class Edit_Service : Form
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=Barbershop;password=147852369;charset=utf8;");
        DataTable tb;
        MySqlCommand command;
        public Edit_Service()
        {
            InitializeComponent();
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            string sql = "SELECT id,category as название,price as стоимость FROM barbershop.services;";
            ExecuteQuery(sql);
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

        public void ExecuteQuery(string query)
        {
            try
            {
                dataGridView1.DataSource = null;
                OpenConnection();
                tb = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.Fill(tb);
                dataGridView1.DataSource = tb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        private void Edit_Service_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                string sql = "update barbershop.services set category='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "', " +
                    "price =" + Convert.ToDouble(dataGridView1.CurrentRow.Cells[2].Value) + " " +
                    "where id=" + index + ";";
                OpenConnection();
                command = new MySqlCommand(sql, conn);
                command.ExecuteScalar();
                CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Service obj = new Service();
            obj.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Name = textBox1.Text;
                double Price = Convert.ToDouble(textBox2.Text);
                conn.Open();

                string sql_max_id = "SELECT MAX(id) FROM barbershop.services;";

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




                string insert_into = "INSERT INTO `barbershop`.`services` (`id`, `category`, `price`) VALUES ('"+(max_id+1)+"', '" + Name + "', '" + Price + "');";

                MySqlCommand command_insert = new MySqlCommand(insert_into, conn);
                command_insert.ExecuteScalar();
                MessageBox.Show("Услуга добавлена", "Сообщение");
                conn.Close();

                Service Back = new Service();
                this.Hide();
                Back.Show();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                throw;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
              
                string sql = "DELETE FROM barbershop.services where id=" + dataGridView1.CurrentRow.Cells[0].Value + ";" +
                    "SELECT id,category as название,price as стоимость FROM barbershop.services;";
                ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                throw;
            }
        }
    }
}

