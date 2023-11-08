using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml.Linq;
using Table = Microsoft.Office.Interop.Word.Table;

namespace Приемная_комиссия_By_LeXa
{
    public partial class glav_forms : Form
    {
        private bool dataSaved = false;
        private string connectionString = "Data Source=HOME-PC\\SQLEXPRESS;Initial Catalog=RKRIPT;Integrated Security=True";
     
        public Ozenki ozenki = new Ozenki();

        public glav_forms()
        {
            InitializeComponent();
            FillSpecialtiesComboBox(comboBoxSpecialty1);
            FillSpecialtiesComboBox(comboBoxSpecialty2);
            FillSpecialtiesComboBox(comboBoxSpecialty3);

        }

        private void FillSpecialtiesComboBox(ComboBox comboBox)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT [name_special] FROM special";
                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox.Items.Add(reader["name_special"].ToString());
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке специальностей: " + ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sred_ball calculateAverageForm = new sred_ball();
            DialogResult result = calculateAverageForm.ShowDialog();

            // Если результат диалога - OK, сохраняем средний балл в текстовом поле
            if (result == DialogResult.OK)
            {
                txtAverageScore.Text = ozenki.totalScore.ToString();
             
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (!AreAllGradesFilled())
            {
                MessageBox.Show("Пожалуйста, заполните все оценки перед созданием файла.");
                return;
            }

            // Получение данных из класса Ozenki
            float averageScore = ozenki.CalculateAverageScore();
            string fio = txtFIO.Text;
            string selectedSpecialty1 = comboBoxSpecialty1.SelectedItem?.ToString();
            string selectedSpecialty2 = comboBoxSpecialty2.SelectedItem?.ToString();
            string selectedSpecialty3 = comboBoxSpecialty3.SelectedItem?.ToString();

            if (selectedSpecialty1 == null || selectedSpecialty2 == null || selectedSpecialty3 == null)
            {
                MessageBox.Show("Пожалуйста, выберите специальность во всех трех выпадающих списках.");
                return;
            }

            // Проверка на одинаковые специальности
            if (selectedSpecialty1.Equals(selectedSpecialty2) ||
                selectedSpecialty1.Equals(selectedSpecialty3) ||
                selectedSpecialty2.Equals(selectedSpecialty3))
            {
                MessageBox.Show("Выберите разные специальности в каждом выпадающем списке.");
                return;
            }
            // Путь к рабочему столу пользователя
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Имя файла
            string fileName = $"{fio}_AverageScore_{averageScore}.docx";

            // Полный путь к файлу на рабочем столе
            string filePath = Path.Combine(desktopPath, fileName);

            // Создание текста с оценками за предметы
            string content = $"ФИО: {fio}\n" +
                             $"Специальности: {selectedSpecialty1}, {selectedSpecialty2}, {selectedSpecialty3}\n\n" +
                             $"Оценки за предметы:\n" +
                             $"Русский язык: {ozenki.russkii}\n" +
                             $"Литература: {ozenki.literatura}\n" +
                             $"Родной язык: {ozenki.rodnoiYazik}\n" +
                             $"Родная литература: {ozenki.rodnoiLiteratura}\n" +
                             $"Иностранный язык: {ozenki.inostranniiYazik}\n" +
                             $"История: {ozenki.istoria}\n" +
                             $"География: {ozenki.geografia}\n" +
                             $"Алгебра: {ozenki.algebra}\n" +
                             $"Геометрия: {ozenki.geometria}\n" +
                             $"Информатика: {ozenki.informatika}\n" +
                             $"Физика: {ozenki.fizika}\n" +
                             $"Биология: {ozenki.biologia}\n" +
                             $"Химия: {ozenki.himia}\n" +
                             $"Изобразительное искусство: {ozenki.izobrazitelnoeIskusstvo}\n" +
                             $"Музыка: {ozenki.muzyka}\n" +
                             $"Технология: {ozenki.tekhnologia}\n" +
                             $"Физическая культура: {ozenki.fizicheskayaKultura}\n" +
                             $"ОБЖ: {ozenki.obz}\n" +
                             // Добавьте остальные предметы
                             $"\nСредний балл: {averageScore}";

            // Сохранение текста с оценками в файл Word на рабочем столе
            CreateAndOpenWordDocument(content, filePath);
        }

        private bool AreAllGradesFilled()
        {
            // Создайте экземпляр класса Ozenki и получите значения оценок

            return ozenki.russkii >= 2 && ozenki.russkii <= 5 &&
             ozenki.literatura >= 2 && ozenki.literatura <= 5 &&
            ozenki.rodnoiYazik >= 2 && ozenki.rodnoiYazik <= 5 &&
            ozenki.rodnoiLiteratura >= 2 && ozenki.rodnoiLiteratura <= 5 &&
            ozenki.inostranniiYazik >= 2 && ozenki.inostranniiYazik <= 5 &&
            ozenki.istoria >= 2 && ozenki.istoria <= 5 &&
            ozenki.geografia >= 2 && ozenki.geografia <= 5 &&
            ozenki.algebra >= 2 && ozenki.algebra <= 5 &&
            ozenki.geometria >= 2 && ozenki.geometria <= 5 &&
             ozenki.informatika >= 2 && ozenki.informatika <= 5 &&
         ozenki.fizika >= 2 && ozenki.fizika <= 5 &&
         ozenki.biologia >= 2 && ozenki.biologia <= 5 &&
        ozenki.himia >= 2 && ozenki.himia <= 5 &&
        ozenki.izobrazitelnoeIskusstvo >= 2 && ozenki.izobrazitelnoeIskusstvo <= 5 &&
        ozenki.muzyka >= 2 && ozenki.muzyka <= 5 &&
        ozenki.tekhnologia >= 2 && ozenki.tekhnologia <= 5 &&
        ozenki.fizicheskayaKultura >= 2 && ozenki.fizicheskayaKultura <= 5 &&
        ozenki.obz >= 2 && ozenki.obz <= 5 && true;
            

        }

        private void CreateAndOpenWordDocument(string content, string filePath)
        {
            try
            {
                Word.Application wordApp = new Word.Application();
                Word.Document doc = wordApp.Documents.Add();

                // Добавление содержимого в документ
                doc.Content.Text = content;

                // Сохранение документа в файл
                doc.SaveAs2(filePath);
                doc.Close();
                wordApp.Quit();

                // Открытие файла
                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при создании или открытии документа: " + ex.Message);
            }
        }
        private void SaveToDatabase(string fio, string selectedSpecialty1, string selectedSpecialty2, string selectedSpecialty3, float averageScore)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO student (FIO, osnov_gruppa, dop_gruppa1, dop_gruppa2, sred_ball) " +
                                   "VALUES (@FIO, @OsnovGruppa, @DopGruppa1, @DopGruppa2, @AverageScore)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FIO", fio);
                    command.Parameters.AddWithValue("@OsnovGruppa", selectedSpecialty1);
                    command.Parameters.AddWithValue("@DopGruppa1", selectedSpecialty2);
                    command.Parameters.AddWithValue("@DopGruppa2", selectedSpecialty3);
                    command.Parameters.AddWithValue("@AverageScore", averageScore);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно сохранены в базе данных.");
                    }
                    else
                    {
                        MessageBox.Show("Не удалось сохранить данные в базе данных.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при сохранении данных в базе данных: " + ex.Message);
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            special special = new special(); // Передача текущей формы в конструктор glav_forms
            special.Show();
        }

        

        private void AddRowToTable(Word.Table table, int row, string subject, string grade)
        {
            table.Cell(row, 1).Range.Text = subject;
            table.Cell(row, 2).Range.Text = grade;
        }
    }
}