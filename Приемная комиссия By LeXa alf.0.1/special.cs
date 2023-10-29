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
    public partial class special : Form
    {
        public special()
        {
            InitializeComponent();
            LoadSpecialties();
        }

        private void LoadSpecialties()
        {
            listBoxSpecialties.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id_special, name_special FROM special";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listBoxSpecialties.Items.Add(new SpecialItem
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
            }
        }

        private string connectionString = "Data Source=LEXA;Initial Catalog=RKRIPT;Integrated Security=True";
        private void button3_Click(object sender, EventArgs e)
        {
            
            glav_forms glavForms = new glav_forms();
            this.Hide();
            glavForms.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string code = txtCode.Text;
            int seats = Convert.ToInt32(txtSeats.Text);
            string fullName = txtFullName.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO special (name_special, kod_special, mesta, poln_name) VALUES (@name, @code, @seats, @fullName)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@seats", seats);
                command.Parameters.AddWithValue("@fullName", fullName);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    LoadSpecialties();
                    ClearForm();
                }
            }
        }
        public class SpecialItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
        private void ClearForm()
        {
            txtName.Text = "";
            txtCode.Text = "";
            txtSeats.Text = "";
            txtFullName.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBoxSpecialties.SelectedItem != null)
            {
                SpecialItem selectedSpecialty = (SpecialItem)listBoxSpecialties.SelectedItem;
                int specialId = selectedSpecialty.Id;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM special WHERE id_special = @specialId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@specialId", specialId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        LoadSpecialties();
                    }
                }
            }
        }
    }
}
