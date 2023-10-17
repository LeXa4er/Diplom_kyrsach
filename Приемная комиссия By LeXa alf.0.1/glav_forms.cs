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

namespace Приемная_комиссия_By_LeXa
{
    public partial class glav_forms : Form
    {
        private string connectionString = "Data Source=DESKTOP-V7FB61F\\SQLEXPRESS;Initial Catalog=RKRIPT;Integrated Security=True";
        private bool dataSaved;

        public glav_forms()
        {

            InitializeComponent();
            LoadSpecialties();

        }

        private void LoadSpecialties()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT name_special FROM special";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBoxSpecialty1.Items.Add(reader["name_special"].ToString());
                        comboBoxSpecialty2.Items.Add(reader["name_special"].ToString());
                        comboBoxSpecialty3.Items.Add(reader["name_special"].ToString());
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке специальностей: " + ex.Message);
            }
        }






        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string fio = txtFIO.Text;
            string selectedSpecialty1 = comboBoxSpecialty1.SelectedItem.ToString();
            string selectedSpecialty2 = comboBoxSpecialty2.SelectedItem.ToString();
            string selectedSpecialty3 = comboBoxSpecialty3.SelectedItem.ToString();

            // Сохранение данных в базе данных
            SaveToDatabase(fio, selectedSpecialty1, selectedSpecialty2, selectedSpecialty3);

            // Создание и открытие документа Word
            CreateAndOpenWordDocument(fio, selectedSpecialty1, selectedSpecialty2, selectedSpecialty3);

            dataSaved = true;
        }

        private void CreateAndOpenWordDocument(string fio, int russkii, int literatura, int rodnoiYazik, int rodnoiLiteratura, int inostranniiYazik,
     int istoria, int obchestvo, int geografia, int algebra, int geometria, int informatika, int fizika, int biologia, int himia,
     int izobrazitelnoeIskusstvo, int muzyka, int tekhnologia, int fizicheskayaKultura, int obz, float averageScore)
        {

            try
            {
                Word.Application wordApp = new Word.Application();
                Word.Document doc = wordApp.Documents.Add();

                // Добавление данных в документ
                doc.Content.Text = $"ФИО студента: {fio}\n";
                doc.Content.Text += $"Русский язык: {russkii}\n";
                doc.Content.Text += $"Литература: {literatura}\n";
                doc.Content.Text += $"Родной язык: {rodnoiYazik}\n";
                doc.Content.Text += $"Родная литература: {rodnoiLiteratura}\n";
                doc.Content.Text += $"Иностранный язык: {inostranniiYazik}\n";
                doc.Content.Text += $"История России. Всеобщая история: {istoria}\n";
                doc.Content.Text += $"Обществознание: {obchestvo}\n";
                doc.Content.Text += $"География: {geografia}\n";
                doc.Content.Text += $"Алгебра: {algebra}\n";
                doc.Content.Text += $"Геометрия: {geometria}\n";
                doc.Content.Text += $"Информатика и ИКТ: {informatika}\n";
                doc.Content.Text += $"Физика: {fizika}\n";
                doc.Content.Text += $"Биология: {biologia}\n";
                doc.Content.Text += $"Химия: {himia}\n";
                doc.Content.Text += $"Изобразительное искусство: {izobrazitelnoeIskusstvo}\n";
                doc.Content.Text += $"Музыка: {muzyka}\n";
                doc.Content.Text += $"Технология: {tekhnologia}\n";
                doc.Content.Text += $"Физическая культура: {fizicheskayaKultura}\n";
                doc.Content.Text += $"Основы безопасной жизнедеятельности: {obz}\n";
                doc.Content.Text += $"Средний балл: {averageScore}\n";

                // Сохранение документа в файл
                string fileName = $"{fio}.docx";
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
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

        private void SaveToDatabase(string fio, string selectedSpecialty1, string selectedSpecialty2, string selectedSpecialty3)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL-запрос для вставки данных в таблицу student
                    string query = "INSERT INTO student (FIO, osnov_gruppa, dop_gruppa1, dop_gruppa2) " +
                                   "VALUES (@FIO, @OsnovGruppa, @DopGruppa1, @DopGruppa2)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FIO", fio);
                    command.Parameters.AddWithValue("@OsnovGruppa", selectedSpecialty1);
                    command.Parameters.AddWithValue("@DopGruppa1", selectedSpecialty2);
                    command.Parameters.AddWithValue("@DopGruppa2", selectedSpecialty3);

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

        private int GetSpecialtyIdByName(string specialtyName, SqlConnection connection)
        {
            // SQL-запрос для получения ID специальности по ее названию
            string query = "SELECT id_special FROM special WHERE name_special = @SpecialtyName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SpecialtyName", specialtyName);
            object result = command.ExecuteScalar();

            // Проверка, был ли найден ID специальности
            if (result != null && int.TryParse(result.ToString(), out int specialtyId))
            {
                return specialtyId;
            }

            // Если специальность с таким названием не найдена, можно вернуть значение по умолчанию или бросить исключение
            // Например, можно вернуть -1, если специальность не найдена:
            return -1;
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
                    Close();
                
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {



            sred_ball calculateAverageForm = new sred_ball();
            DialogResult result = calculateAverageForm.ShowDialog(); // Здесь объявляем и инициализируем result



            // Получение среднего балла из CalculateAverageForm
            float averageScore = sred_ball.GetAverageScore();

            // Отображение среднего балла на главной форме в текстовом поле txtAverageScore
            txtAverageScore.Text = averageScore.ToString();


            // Если результат диалога - OK, сохраняем средний балл в базе данных
            if (result == DialogResult.OK)
            {
                float averageScoreValue = sred_ball.GetAverageScore(); // Получаем средний балл из формы расчета
                calculateAverageForm.SaveToDatabase(averageScoreValue); // Сохраняем средний балл в базе данных


                int russkii = calculateAverageForm.russkii;
                int literatura = calculateAverageForm.literatura;
                int rodnoiYazik = calculateAverageForm.rodnoiYazik;
                int rodnoiLiteratura = calculateAverageForm.rodnoiLiteratura;
                int inostranniiYazik = calculateAverageForm.inostranniiYazik;
                int istoria = calculateAverageForm.istoria;
                int obchestvo = calculateAverageForm.obchestvo;
                int geografia = calculateAverageForm.geografia;
                int algebra = calculateAverageForm.algebra;
                int geometria = calculateAverageForm.geometria;
                int informatika = calculateAverageForm.informatika;
                int fizika = calculateAverageForm.fizika;
                int biologia = calculateAverageForm.biologia;
                int himia = calculateAverageForm.himia;
                int izobrazitelnoeIskusstvo = calculateAverageForm.izobrazitelnoeIskusstvo;
                int muzyka = calculateAverageForm.muzyka;
                int tekhnologia = calculateAverageForm.tekhnologia;
                int fizicheskayaKultura = calculateAverageForm.fizicheskayaKultura;
                int obz = calculateAverageForm.obz;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {   
            this.Hide();
            special special = new special(); // Передача текущей формы в конструктор glav_forms
            special.Show();
        }
    }



    
}
