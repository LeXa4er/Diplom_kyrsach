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
using static Приемная_комиссия_By_LeXa.glav_forms;

namespace Приемная_комиссия_By_LeXa
{
    public partial class glav_forms : Form
    {
        private sred_ball sredBallForm;
        private autorithation authorizationForm;

        public glav_forms(autorithation form)
        {

            InitializeComponent();
            authorizationForm = form;
        }
        private double receivedAverageScore;
        public void SetAverageScore(double averageScore)
        {
            receivedAverageScore = averageScore;
            // Присвоить receivedAverageScore вашиему текстовому полю в этой форме
            AverageScoreTextBox.Text = receivedAverageScore.ToString();
        }


        //rjvrtikdfghjkl;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверка наличия экземпляра sred_ball, если он уже создан
                if (sredBallForm == null || sredBallForm.IsDisposed)
                {
                    sredBallForm = new sred_ball(this); // Передача текущего экземпляра glav_forms в конструктор sred_ball
                }

                // Отображаем форму sred_ball в режиме диалогового окна
                DialogResult result = sredBallForm.ShowDialog();

                // Проверяем результат диалога
                if (result == DialogResult.OK)
                {
                    // Если пользователь нажал кнопку "Рассчитать", получаем средний балл и округляем его до 2 десятичных знаков
                    double averageScore = sredBallForm.CalculateAverageScore();
                    averageScore = Math.Round(averageScore, 2); // Округляем до 2 десятичных знаков
                    AverageScoreTextBox.Text = averageScore.ToString(CultureInfo.GetCultureInfo("ru-RU"));
                }
                else
                {
                    // Если пользователь закрыл форму без рассчета, выполняем соответствующие действия
                    MessageBox.Show("Вы закрыли форму без расчета среднего балла.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Создаем новый Word-документ и добавляем информацию
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();

                // Получаем данные из формы glav_forms
                string fullName = FullNameTextBox.Text;
                double averageScore = Convert.ToDouble(AverageScoreTextBox.Text);

                // Добавляем информацию в документ
                doc.Content.Text = $"ФИО студента: {fullName}\nСредний балл: {averageScore}";

                // Сохраняем документ и открываем его
                string filePath = @"СреднийБалл.docx";
                doc.SaveAs2(filePath);
                //doc.Close();
                wordApp.Quit();

                MessageBox.Show("Документ успешно создан.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при создании документа: {ex.Message}");
            }

        }

        private void tableLayoutPanel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void glav_forms_Load(object sender, EventArgs e)
        {

        }
    }
}
