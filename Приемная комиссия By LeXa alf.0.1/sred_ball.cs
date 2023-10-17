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


        private string connectionString = "Data Source=DESKTOP-V7FB61F\\SQLEXPRESS;Initial Catalog=RKRIPT;Integrated Security=True";
        public sred_ball()
        {
            InitializeComponent();
      

        }


        private static float _averageScore;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Получение значений из текстовых полей
                int russkii = Convert.ToInt32(txtRusskii.Text);
                int literatura = Convert.ToInt32(txtLiteratura.Text);
                int rodnoiYazik = Convert.ToInt32(txtRodnoiYazik.Text);
                int rodnoiLiteratura = Convert.ToInt32(txtRodnoiLiteratura.Text);
                int inostranniiYazik = Convert.ToInt32(txtInostranniiYazik.Text);
                int istoria = Convert.ToInt32(txtIstoria.Text);
                int obchestvo = Convert.ToInt32(txtObchestvo.Text);
                int geografia = Convert.ToInt32(txtGeografia.Text);
                int algebra = Convert.ToInt32(txtAlgebra.Text);
                int geometria = Convert.ToInt32(txtGeometria.Text);
                int informatika = Convert.ToInt32(txtInformatika.Text);
                int fizika = Convert.ToInt32(txtFizika.Text);
                int biologia = Convert.ToInt32(txtBiologia.Text);
                int himia = Convert.ToInt32(txtHimia.Text);
                int izobrazitelnoeIskusstvo = Convert.ToInt32(txtIzobrazitelnoeIskusstvo.Text);
                int muzyka = Convert.ToInt32(txtMuzyka.Text);
                int tekhnologia = Convert.ToInt32(txtTekhnologia.Text);
                int fizicheskayaKultura = Convert.ToInt32(txtFizicheskayaKultura.Text);
                int obz = Convert.ToInt32(txtOBZ.Text);

                // Рассчет среднего балла
                float averageScore = (russkii + literatura + rodnoiYazik + rodnoiLiteratura + inostranniiYazik +
                                      istoria + obchestvo + geografia + algebra + geometria + informatika +
                                      fizika + biologia + himia + izobrazitelnoeIskusstvo + muzyka + tekhnologia +
                                      fizicheskayaKultura + obz) / 19.0f;

                _averageScore = averageScore; // Сохранение среднего балла в статической переменной
                DialogResult = DialogResult.OK; // Устанавливаем результат диалога на OK
                this.Close(); // Закрытие формы CalculateAverageForm после сохранения в базу данных
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при сохранении среднего балла в базе данных: " + ex.Message);
            }
        }
        public static float GetAverageScore()
        {
            return _averageScore; // Метод для получения значения среднего балла из других форм
        }
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
    }
}