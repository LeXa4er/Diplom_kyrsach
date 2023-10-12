using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Приемная_комиссия_By_LeXa.glav_forms;

namespace Приемная_комиссия_By_LeXa
{
    public partial class sred_ball : Form
    {
        private double averageScore;
        private glav_forms mainForm;

        public sred_ball(glav_forms form)
        {
            InitializeComponent();
            mainForm = form;

        }
        private double GetScoreFromTextBox(TextBox textBox)
        {
            double score;
            if (double.TryParse(textBox.Text, out score))
            {
                return score;
            }
            return 0; // Если введено некорректное значение, считаем как 0
        }

        public double CalculateAverageScore()
        {
            // Считаем средний балл
            List<double> validScores = new List<double>();

            if (IsValidScore(RussianTextBox.Text)) validScores.Add(GetScoreFromTextBox(RussianTextBox));
            if (IsValidScore(LiteratureTextBox.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            if (IsValidScore(NativeLiteratureTextBox.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            if (IsValidScore(NativeLiteratureText.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            if (IsValidScore(ForeignLanguageTextBox.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            if (IsValidScore(HistoryTextBox.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            if (IsValidScore(SocialStudiesTextBox.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            if (IsValidScore(GeographyTextBox.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            if (IsValidScore(AlgebraTextBox.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            if (IsValidScore(GeometryTextBox.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            if (IsValidScore(ComputerScienceTextBox.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            if (IsValidScore(PhysicsTextBox.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            if (IsValidScore(ChemistryTextBox.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            if (IsValidScore(VisualArtsTextBox.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            if (IsValidScore(MusicTextBox.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            if (IsValidScore(MusicTextBox.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            if (IsValidScore(PhysicalEducationTextBox.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            if (IsValidScore(LifeSafetyTextBox.Text)) validScores.Add(GetScoreFromTextBox(LiteratureTextBox));
            // Повторите этот шаблон для всех TextBox, добавляя оценки в validScores только если они корректны

            if (validScores.Count > 0)
            {
                double totalScore = validScores.Sum();
                double averageScore = totalScore / validScores.Count;
                return averageScore;
            }
            else
            {
                MessageBox.Show("Введите хотя бы одну корректную оценку от 2 до 5.");
                return 0;
            }

        }
        private bool IsValidScore(string input)
        {
            if (double.TryParse(input, out double score))
            {
                if (score >= 2 && score <= 5)
                {
                    return true;
                }
            }
            return false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            averageScore = CalculateAverageScore();

            // Передача среднего балла в главную форму
            mainForm.SetAverageScore(averageScore);

            // Закрытие текущей формы
            DialogResult = DialogResult.OK;
            Close();
        }
        public double GetAverageScore()
        {
            return averageScore;
        }

        private void ComputerScienceTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}