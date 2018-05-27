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
    public partial class Edit_Reprot : Form
    {
        MySqlCommand command;
        MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=Barbershop;password=147852369;charset=utf8;");
        DataTable tb;
        public Edit_Reprot()
        {
            InitializeComponent();
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            string sql = "SELECT * FROM barbershop.report;";
            ExecuteQuery(sql);
        }

        private void Edit_Reprot_Load(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string sql = "DELETE FROM barbershop.report where Date='" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[0].Value).ToString("yyyy/MM/dd")
                    + "' and Visitors='" + dataGridView1.CurrentRow.Cells[1].Value + "' and " +
                    " Money = '" + dataGridView1.CurrentRow.Cells[2].Value + "'; SELECT* FROM barbershop.report;" ;
                ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Report obj = new Report();
            obj.Show();
        }

        private void Edit_Reprot_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
