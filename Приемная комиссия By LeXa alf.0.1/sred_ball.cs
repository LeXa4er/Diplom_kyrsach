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
        public static int russkii { get; set; }
        public static int literatura { get; set; }
        public static int rodnoiYazik { get; set; }
        public static int rodnoiLiteratura { get; set; }
        public static int inostranniiYazik { get; set; }
        public static int istoria { get; set; }
        public static int obchestvo { get; set; }
        public static int geografia { get; set; }
        public static int algebra { get; set; }
        public static int geometria { get; set; }
        public static int informatika { get; set; }
        public static int fizika { get; set; }
        public static int biologia { get; set; }
        public static int himia { get;  set; }
        public static int izobrazitelnoeIskusstvo { get;  set; }
        public static int muzyka { get;  set; }
        public static int tekhnologia { get;  set; }
        public static int fizicheskayaKultura { get;  set; }
        public static int obz { get;  set; }
        private Dictionary<string, int> subjectsScores;
        public float GetAverageScore()
        {
            return CalculateAverageScore(); // Ваш метод для расчета среднего балла
        }
        public Dictionary<string, int> GetSubjectsScores()
        {
            Dictionary<string, int> subjectsScores = new Dictionary<string, int>
        {
            { "russkii", russkii },
            { "literatura", literatura },
            // Добавьте другие предметы и их оценки сюда
        };
            return subjectsScores;
        }

        private string connectionString = "Data Source=DESKTOP-V7FB61F\\SQLEXPRESS;Initial Catalog=RKRIPT;Integrated Security=True";
        public sred_ball()
        {
            InitializeComponent();
            subjectsScores = new Dictionary<string, int>();

        }


        public static float _averageScore;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Получение значений из текстовых полей и сохранение их в статические переменные
                russkii = Convert.ToInt32(txtRusskii.Text);
                literatura = Convert.ToInt32(txtLiteratura.Text);
                rodnoiYazik = Convert.ToInt32(txtRodnoiYazik.Text);
                rodnoiLiteratura = Convert.ToInt32(txtRodnoiLiteratura.Text);
                inostranniiYazik = Convert.ToInt32(txtInostranniiYazik.Text);
                istoria = Convert.ToInt32(txtIstoria.Text);
                obchestvo = Convert.ToInt32(txtObchestvo.Text);
                geografia = Convert.ToInt32(txtGeografia.Text);
                algebra = Convert.ToInt32(txtAlgebra.Text);
                geometria = Convert.ToInt32(txtGeometria.Text);
                informatika = Convert.ToInt32(txtInformatika.Text);
                fizika = Convert.ToInt32(txtFizika.Text);
                biologia = Convert.ToInt32(txtBiologia.Text);
                himia = Convert.ToInt32(txtHimia.Text);
                izobrazitelnoeIskusstvo = Convert.ToInt32(txtIzobrazitelnoeIskusstvo.Text);
                muzyka = Convert.ToInt32(txtMuzyka.Text);
                tekhnologia = Convert.ToInt32(txtTekhnologia.Text);
                fizicheskayaKultura = Convert.ToInt32(txtFizicheskayaKultura.Text);
                obz = Convert.ToInt32(txtOBZ.Text);
                _averageScore = CalculateAverageScore(); // Рассчет среднего балла и сохранение его
                DialogResult = DialogResult.OK; // Устанавливаем результат диалога на OK
                this.Close(); // Закрытие формы CalculateAverageForm после сохранения в базу данных
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при сохранении среднего балла в базе данных: " + ex.Message);
            }
        }

        private float CalculateAverageScore()
        {
            // Рассчет среднего балла
            float totalScore = russkii + literatura + rodnoiYazik + rodnoiLiteratura + inostranniiYazik +
                              istoria + obchestvo + geografia + algebra + geometria + informatika +
                              fizika + biologia + himia + izobrazitelnoeIskusstvo + muzyka + tekhnologia +
                              fizicheskayaKultura + obz;
            return totalScore / 19.0f;
        }
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

                    int rowsAffected  = command.ExecuteNonQuery();
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