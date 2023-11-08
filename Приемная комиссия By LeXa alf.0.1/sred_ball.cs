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
	public partial class sred_ball : Form
	{
		//public static int russkii { get; set; }
		//public static int literatura { get; set; }
		//public static int rodnoiYazik { get; set; }
		//public static int rodnoiLiteratura { get; set; }
		//public static int inostranniiYazik { get; set; }
		//public static int istoria { get; set; }
		//public static int obchestvo { get; set; }
		//public static int geografia { get; set; }
		//public static int algebra { get; set; }
		//public static int geometria { get; set; }
		//public static int informatika { get; set; }
		//public static int fizika { get; set; }
		//public static int biologia { get; set; }
		//public static int himia { get;  set; }
		//public static int izobrazitelnoeIskusstvo { get;  set; }
		//public static int muzyka { get;  set; }
		//public static int tekhnologia { get;  set; }
		//public static int fizicheskayaKultura { get;  set; }
		//public static int obz { get;  set; }
		//private Dictionary<string, int> subjectsScores;
		//public float GetAverageScore()
		//{
		//    return CalculateAverageScore(); 
		//}
		//public Dictionary<string, int> GetSubjectsScores()
		//{
		//    Dictionary<string, int> subjectsScores = new Dictionary<string, int>
		//{
		//    { "russkii", russkii },
		//    { "literatura", literatura },

		//};
		//    return subjectsScores;
		//}

		private string connectionString = "Data Source=HOME-PC;Initial Catalog=RKRIPT;Integrated Security=True";
		public sred_ball()
		{
			InitializeComponent();
			//subjectsScores = new Dictionary<string, int>();

		}

		Ozenki ozenki = new Ozenki();

		public static float _averageScore;
		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				// Получение значений из текстовых полей и сохранение их в статические переменные
				ozenki.russkii = Convert.ToInt32(txtRusskii.Text);
				ozenki.literatura = Convert.ToInt32(txtLiteratura.Text);
				ozenki.rodnoiYazik = Convert.ToInt32(txtRodnoiYazik.Text);
				ozenki.rodnoiLiteratura = Convert.ToInt32(txtRodnoiLiteratura.Text);
				ozenki.inostranniiYazik = Convert.ToInt32(txtInostranniiYazik.Text);
				ozenki.istoria = Convert.ToInt32(txtIstoria.Text);
				ozenki.obchestvo = Convert.ToInt32(txtObchestvo.Text);
				ozenki.geografia = Convert.ToInt32(txtGeografia.Text);
				ozenki.algebra = Convert.ToInt32(txtAlgebra.Text);
				ozenki.geometria = Convert.ToInt32(txtGeometria.Text);
				ozenki.informatika = Convert.ToInt32(txtInformatika.Text);
				ozenki.fizika = Convert.ToInt32(txtFizika.Text);
				ozenki.biologia = Convert.ToInt32(txtBiologia.Text);
				ozenki.himia = Convert.ToInt32(txtHimia.Text);
				ozenki.izobrazitelnoeIskusstvo = Convert.ToInt32(txtIzobrazitelnoeIskusstvo.Text);
				ozenki.muzyka = Convert.ToInt32(txtMuzyka.Text);
				ozenki.tekhnologia = Convert.ToInt32(txtTekhnologia.Text);
				ozenki.fizicheskayaKultura = Convert.ToInt32(txtFizicheskayaKultura.Text);
				ozenki.obz = Convert.ToInt32(txtOBZ.Text);
				_averageScore = ozenki.CalculateAverageScore(); // Рассчет среднего балла и сохранение его
				DialogResult = DialogResult.OK; // Устанавливаем результат диалога на OK
				glav_forms glav_Forms = (glav_forms)Application.OpenForms[Application.OpenForms.Count - 2];
				glav_Forms.ozenki = ozenki;
				this.Close(); // Закрытие формы CalculateAverageForm после сохранения в базу данных
			}
			catch (Exception ex)
			{
				MessageBox.Show("Произошла ошибка при сохранении среднего балла в базе данных: " + ex.Message);
			}
		}

		//private float CalculateAverageScore()
		//{
		//    // Рассчет среднего балла
		//    float totalScore = russkii + literatura + rodnoiYazik + rodnoiLiteratura + inostranniiYazik +
		//                      istoria + obchestvo + geografia + algebra + geometria + informatika +
		//                      fizika + biologia + himia + izobrazitelnoeIskusstvo + muzyka + tekhnologia +
		//                      fizicheskayaKultura + obz;
		//    return totalScore / 19.0f;
		//}
		//public static float GetAverageScore()
		//{
		//    return _averageScore; // Метод для получения значения среднего балла из других форм
		//}
		private void ComputerScienceTextBox_TextChanged(object sender, EventArgs e)
		{

		}
		public void SaveToDatabase(float averageScore)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string query = "INSERT INTO sred_ball (russkii, literat, rodnoi_yazik, rodnoi_literat, inostranniiYazik, " +
								   "histori, obchestvo, geograf, algebra, geometria, informatika, fizika, biologia, izo, " +
								   "myzika, texcologia, fizra, OBZ, sred_ball) VALUES (@russkii, @literat, @rodnoiYazik, " +
								   "@rodnoiLiterat, @inostranniiYazik, @istoria, @obchestvo, @geografia, @algebra, " +
								   "@geometria, @informatika, @fizika, @biologia, @izo, @myzika, @texcologia, @fizra, @OBZ, @sredBall)";
					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@russkii", Convert.ToInt32(txtRusskii.Text));
					command.Parameters.AddWithValue("@literat", Convert.ToInt32(txtLiteratura.Text));
					command.Parameters.AddWithValue("@rodnoiYazik", Convert.ToInt32(txtRodnoiYazik.Text));
					command.Parameters.AddWithValue("@rodnoiLiterat", Convert.ToInt32(txtRodnoiLiteratura.Text));
					command.Parameters.AddWithValue("@inostranniiYazik", Convert.ToInt32(txtInostranniiYazik.Text));
					command.Parameters.AddWithValue("@istoria", Convert.ToInt32(txtIstoria.Text));
					command.Parameters.AddWithValue("@obchestvo", Convert.ToInt32(txtObchestvo.Text));
					command.Parameters.AddWithValue("@geografia", Convert.ToInt32(txtGeografia.Text));
					command.Parameters.AddWithValue("@algebra", Convert.ToInt32(txtAlgebra.Text));
					command.Parameters.AddWithValue("@geometria", Convert.ToInt32(txtGeometria.Text));
					command.Parameters.AddWithValue("@informatika", Convert.ToInt32(txtInformatika.Text));
					command.Parameters.AddWithValue("@fizika", Convert.ToInt32(txtFizika.Text));
					command.Parameters.AddWithValue("@biologia", Convert.ToInt32(txtBiologia.Text));
					command.Parameters.AddWithValue("@izo", Convert.ToInt32(txtIzobrazitelnoeIskusstvo.Text));
					command.Parameters.AddWithValue("@myzika", Convert.ToInt32(txtMuzyka.Text));
					command.Parameters.AddWithValue("@texcologia", Convert.ToInt32(txtTekhnologia.Text));
					command.Parameters.AddWithValue("@fizra", Convert.ToInt32(txtFizicheskayaKultura.Text));
					command.Parameters.AddWithValue("@OBZ", Convert.ToInt32(txtOBZ.Text));
					command.Parameters.AddWithValue("@sredBall", averageScore);

					int rowsAffected = command.ExecuteNonQuery();
					if (rowsAffected > 0)
					{
						MessageBox.Show("Средний балл успешно сохранен в базе данных.");
					}
					else
					{
						MessageBox.Show("Не удалось сохранить средний балл в базе данных.");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Произошла ошибка при сохранении среднего балла в базе данных: " + ex.Message);
			}
		}

        private void txtRusskii_KeyPress(object sender, KeyPressEventArgs e)
		{
            if (int.TryParse(txtRusskii.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtRusskii.Focus();
                txtRusskii.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtLiteratura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.TryParse(txtLiteratura.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtLiteratura.Focus();
                txtLiteratura.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtRodnoiYazik_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.TryParse(txtRodnoiYazik.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtRodnoiYazik.Focus();
                txtRodnoiYazik.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtRodnoiLiteratura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.TryParse(txtRodnoiLiteratura.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtRodnoiLiteratura.Focus();
                txtRodnoiLiteratura.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtInostranniiYazik_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (int.TryParse(txtInostranniiYazik.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtInostranniiYazik.Focus();
                txtInostranniiYazik.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtIstoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.TryParse(txtIstoria.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtIstoria.Focus();
                txtIstoria.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtObchestvo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.TryParse(txtObchestvo.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtObchestvo.Focus();
                txtObchestvo.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtGeografia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.TryParse(txtGeografia.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtGeografia.Focus();
                txtGeografia.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtAlgebra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.TryParse(txtAlgebra.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtAlgebra.Focus();
                txtAlgebra.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtGeometria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.TryParse(txtGeometria.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtGeometria.Focus();
                txtGeometria.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtInformatika_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.TryParse(txtInformatika.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtInformatika.Focus();
                txtInformatika.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtFizika_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.TryParse(txtFizika.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtFizika.Focus();
                txtFizika.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

      

        private void txtBiologia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.TryParse(txtBiologia.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtBiologia.Focus();
                txtBiologia.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtHimia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.TryParse(txtHimia.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtHimia.Focus();
                txtHimia.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtIzobrazitelnoeIskusstvo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.TryParse(txtIzobrazitelnoeIskusstvo.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtIzobrazitelnoeIskusstvo.Focus();
                txtIzobrazitelnoeIskusstvo.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtMuzyka_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.TryParse(txtMuzyka.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtMuzyka.Focus();
                txtMuzyka.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtTekhnologia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.TryParse(txtTekhnologia.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtTekhnologia.Focus();
                txtTekhnologia.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtFizicheskayaKultura_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (int.TryParse(txtFizicheskayaKultura.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtFizicheskayaKultura.Focus();
                txtFizicheskayaKultura.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }

        private void txtOBZ_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (int.TryParse(txtOBZ.Text, out int value) && (value < 2 || value > 5))
            {
                MessageBox.Show("Оценка должна быть от 2 до 5");
                txtOBZ.Focus();
                txtOBZ.Text = ""; // Очищаем поле ввода в случае недопустимого значения
            }
        }
    }
}