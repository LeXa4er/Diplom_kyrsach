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
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Xml.Linq;
using Table = Microsoft.Office.Interop.Word.Table;

namespace Приемная_комиссия_By_LeXa
{
    public partial class glav_forms : Form
    {
        private bool dataSaved = false;
        private string connectionString = "Data Source=DESKTOP-V7FB61F\\SQLEXPRESS;Initial Catalog=RKRIPT;Integrated Security=True";
        private Dictionary<string, int> subjectsScores;

        public glav_forms()
        {
            InitializeComponent();
            subjectsScores = new Dictionary<string, int>();
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
                int totalScore = 0;
                int subjectCount = 0;

                foreach (var score in subjectsScores.Values)
                {
                    totalScore += score;
                    subjectCount++;
                }

                if (subjectCount > 0)
                {
                    float averageScore = (float)totalScore / subjectCount;
                    txtAverageScore.Text = averageScore.ToString();
                }
                else
                {
                    MessageBox.Show("Не введены оценки предметов.");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string fio = txtFIO.Text;
            string selectedSpecialty1 = comboBoxSpecialty1.SelectedItem.ToString();
            string selectedSpecialty2 = comboBoxSpecialty2.SelectedItem.ToString();
            string selectedSpecialty3 = comboBoxSpecialty3.SelectedItem.ToString();

            // Проверка, что были введены оценки
            sred_ball sredBallForm = new sred_ball();
            DialogResult result = sredBallForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Получение среднего балла и оценок из формы sred_ball
                float averageScore = sredBallForm.GetAverageScore();
                Dictionary<string, int> subjectsScores = sredBallForm.GetSubjectsScores();

                // Сохранение в базу данных
                SaveToDatabase(fio, selectedSpecialty1, selectedSpecialty2, selectedSpecialty3, averageScore);

                if (subjectsScores.Count > 0)
                {
                    // Создание и открытие Word-документа
                    string filePath = $"{fio}_AverageScore_{averageScore}.docx";
                    CreateAndOpenWordDocument(fio, selectedSpecialty1, selectedSpecialty2, selectedSpecialty3, subjectsScores, averageScore, filePath);
                }
                else
                {
                    MessageBox.Show("Не введены оценки предметов.");
                }
            }
            else
            {
                MessageBox.Show("Не введены оценки предметов.");
            }

            dataSaved = true;
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
        private void CreateAndOpenWordDocument(string fio, string selectedSpecialty1, string selectedSpecialty2, string selectedSpecialty3, Dictionary<string, int> subjectsScores, float averageScore, string filePath)
        {
            try
            {
                Word.Application wordApp = new Word.Application();
                Word.Document doc = wordApp.Documents.Add();

                // Добавление ФИО и специальностей в документ
                doc.Content.Text = $"ФИО студента: {fio}\n";
                doc.Content.Text += $"Специальности: {selectedSpecialty1}, {selectedSpecialty2}, {selectedSpecialty3}\n\n";

                // Добавление таблицы с оценками
                int rowCount = 1;
                Word.Table table = doc.Tables.Add(doc.Paragraphs[doc.Paragraphs.Count].Range, subjectsScores.Count, 2);

                foreach (var entry in subjectsScores)
                {
                    AddRowToTable(table, rowCount++, entry.Key, entry.Value.ToString());
                }

                // Добавление среднего балла в документ
                doc.Content.Text += $"\nСредний балл: {averageScore}\n";

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

        private void AddRowToTable(Word.Table table, int row, string subject, string grade)
        {
            table.Cell(row, 1).Range.Text = subject;
            table.Cell(row, 2).Range.Text = grade;
        }
    }
}
