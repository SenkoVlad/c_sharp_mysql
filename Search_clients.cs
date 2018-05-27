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
    public partial class Search_clients : Form
    {
        MySqlCommand command;
        MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=Barbershop;password=147852369;charset=utf8;");
        DataTable tb;



        
        public Search_clients()
        {
            InitializeComponent();
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            string sql = "SELECT barbershop.clients.idClients as id,barbershop.clients.Surname as Фамилия,barbershop.clients.Name as Имя,barbershop.clients.Mid_name as Отчество" + "" +
",barbershop.clients.Modile_phone as Телефон,barbershop.clients.Visits as Посещений,barbershop.clients.Date_of_last_vis as Дата,barbershop.clients.Score as Баланс," + "" +
"barbershop.ranks.Rank as Звание FROM barbershop.clients INNER JOIN barbershop.ranks  using(IdRank);";
            ExecuteQuery(sql);

        }

        private void поФамилииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            label1.Text = "Фамилия";
            label1.Visible=true;
            label2.Visible = false;
            button1.Visible = false;
            label3.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;


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
                tb  = new DataTable();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (label1.Text == "Имя")
                {
                    string sql = "SELECT barbershop.clients.idClients as id,barbershop.clients.Surname as Фамилия,barbershop.clients.Name as Имя,barbershop.clients.Mid_name as Отчество" + "" +
",barbershop.clients.Modile_phone as Телефон,barbershop.clients.Visits as Посещений,barbershop.clients.Date_of_last_vis as Дата,barbershop.clients.Score as Баланс," + "" +
"barbershop.ranks.Rank as Звание FROM barbershop.clients INNER JOIN barbershop.ranks  using(IdRank) where Name like '" + textBox1.Text + "%';";
                    ExecuteQuery(sql);
                }
                else if (label1.Text == "Фамилия")
                {
                    string sql = "SELECT barbershop.clients.idClients as id,barbershop.clients.Surname as Фамилия,barbershop.clients.Name as Имя,barbershop.clients.Mid_name as Отчество" + "" +
",barbershop.clients.Modile_phone as Телефон,barbershop.clients.Visits as Посещений,barbershop.clients.Date_of_last_vis as Дата,barbershop.clients.Score as Баланс," + "" +
"barbershop.ranks.Rank as Звание FROM barbershop.clients INNER JOIN barbershop.ranks  using(IdRank) where Surname like '" + textBox1.Text + "%';";
                    ExecuteQuery(sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                throw;
            }

        }

        private void Search_clients_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void поИмениToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;       
            label1.Text = "Имя";
            label1.Visible = true;
            label2.Visible = false;
            label3.Visible = false;
            button1.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
        }

        private void поДатеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            dateTimePicker1.Visible = true;
            dateTimePicker2.Visible = true;
            label1.Visible = false;
            textBox1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT barbershop.clients.idClients as id,barbershop.clients.Surname as Фамилия,barbershop.clients.Name as Имя,barbershop.clients.Mid_name as Отчество" + "" +
",barbershop.clients.Modile_phone as Телефон,barbershop.clients.Visits as Посещений,barbershop.clients.Date_of_last_vis as Дата,barbershop.clients.Score as Баланс," + "" +
"barbershop.ranks.Rank as Звание FROM barbershop.clients INNER JOIN barbershop.ranks  using(IdRank) where  date(Date_of_last_vis) BETWEEN '" + dateTimePicker1.Value.Date.ToString("yyyy/MM/dd") + "' AND '" + dateTimePicker2.Value.Date.ToString("yyyy/MM/dd") + "';";
                ExecuteQuery(sql);
                //
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                throw;
            }
        }

        private void Search_clients_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32 (dataGridView1.CurrentRow.Cells[0].Value);
                DateTime time = new DateTime();
                time =  Convert.ToDateTime(dataGridView1.CurrentRow.Cells[6].Value.ToString());
                string sql = "update barbershop.clients set Surname='"+ dataGridView1.CurrentRow.Cells[1].Value.ToString() + "', " +
                    "Name='"+ dataGridView1.CurrentRow.Cells[2].Value.ToString() + "'," +
                    " Mid_name='"+ dataGridView1.CurrentRow.Cells[3].Value.ToString() + "'," +
                    " Modile_phone='"+ dataGridView1.CurrentRow.Cells[4].Value.ToString() + "'," +
                    " Visits="+ Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value) + ", Date_of_last_vis='"+ time.ToString("yyyy/MM/dd") + "', " +
                    "Score = "+ Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value) + ", IdRank=(SELECT " +
                    "barbershop.ranks.idRank from barbershop.ranks where barbershop.ranks.Rank = '"+ dataGridView1.CurrentRow.Cells[8].Value.ToString() + "') " +
                    "where idClients=" + index+";";
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
            try
            {
                
                string sql = "DELETE FROM barbershop.clients where idClients="+dataGridView1.CurrentRow.Cells[0].Value+ ";" +
                    "SELECT barbershop.clients.idClients,barbershop.clients.Surname,barbershop.clients.Name,barbershop.clients.Mid_name" + "" +
                     ",barbershop.clients.Modile_phone,barbershop.clients.Visits,barbershop.clients.Date_of_last_vis,barbershop.clients.Score," + "" +
                     "barbershop.ranks.Rank FROM barbershop.clients INNER JOIN barbershop.ranks  using(IdRank)";
                ExecuteQuery(sql);  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                throw;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Clients obj = new Clients();
            obj.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            label1.Text = "Фамилия";
            label1.Visible = true;
            label2.Visible = false;
            button1.Visible = false;
            label3.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            label1.Text = "Имя";
            label1.Visible = true;
            label2.Visible = false;
            label3.Visible = false;
            button1.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;    
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            button1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            dateTimePicker1.Visible = true;
            dateTimePicker2.Visible = true;
            label1.Visible = false;
            textBox1.Visible = false;

        }
    }
}
