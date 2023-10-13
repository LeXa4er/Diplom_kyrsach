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
            string login = loginTextBox.Text;
            string password = passwordTextBox.Text;

            // Подключение к базе данных
            string connectionString = "Data Source=DESKTOP-V7FB61F\\SQLEXPRESS;Initial Catalog=RKRIPT;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL-запрос для проверки логина и пароля в таблице user
                string query = "SELECT COUNT(*) FROM [user] WHERE login = @login AND passwd = @password";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);

                    connection.Open();
                    int result = (int)command.ExecuteScalar(); // Выполнение запроса и получение результата

                    if (result > 0)
                    {
                        // Если совпадение найдено, авторизация успешна
                        MessageBox.Show("Авторизация успешна!");
                        this.Hide();
                        glav_forms glavForms = new glav_forms(this); // Передача текущей формы в конструктор glav_forms
                        glavForms.Show();
                    }
                    else
                    {
                        // Если нет совпадений, выводим сообщение об ошибке
                        MessageBox.Show("Неверный логин или пароль. Пожалуйста, попробуйте снова.");
                    }
                }
            }
        }
        }
    }
