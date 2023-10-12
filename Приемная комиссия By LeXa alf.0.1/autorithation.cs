using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Приемная_комиссия_By_LeXa
{
    public partial class autorithation : Form
    {
        public autorithation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection slqconnection = new SqlConnection($"Data Source=LEXA;Initial Catalog=RKRIPT;Integrated Security=True");

            string query = $"Select * from [RKRIPT] where login_user = '" + textBox1.Text.Trim() + "' and passwd_user = '" + textBox2.Text.Trim() + "'";

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, slqconnection);

            DataTable dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count == 1)
            {
              
            this.Hide();
            glav_forms glavForms = new glav_forms(this); // Передача текущей формы в конструктор glav_forms
            glavForms.Show();
            
            }
            else
            {
                MessageBox.Show("Проверьте введённые данные!", "Ошибка");
            }
            
        }
    }
}
