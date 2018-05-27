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
    public partial class Service : Form
    {
        public Service()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Стандарт objc = new Стандарт();
            objc.Show();
        }

        private void Service_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            _1_1 objc = new _1_1();
            objc.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Батя_Сын objc = new Батя_Сын();
            objc.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Premium obj = new Premium();
            obj.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 obj = new Form1();
            obj.Show();
        
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Edit_Service obj = new Edit_Service();
            obj.Show();
        }
    }
}
