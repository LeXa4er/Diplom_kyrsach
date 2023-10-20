using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Приемная_комиссия_By_LeXa
{
	public class Ozenki
	{
        public int russkii;
        public int literatura;
        public int rodnoiYazik;
        public int rodnoiLiteratura;
        public int inostranniiYazik;
        public int istoria;
        public int obchestvo;
        public int geografia;
        public int algebra;
        public int geometria;
        public int informatika;
        public int fizika;
        public int biologia;
        public int himia;
        public int izobrazitelnoeIskusstvo;
        public int muzyka;
        public int tekhnologia;
        public int fizicheskayaKultura;
        public int obz;
        public float totalScore;

        public float CalculateAverageScore()
        {
            // Рассчет среднего балла
            totalScore = russkii + literatura + rodnoiYazik + rodnoiLiteratura + inostranniiYazik +
                         istoria + obchestvo + geografia + algebra + geometria + informatika +
                         fizika + biologia + himia + izobrazitelnoeIskusstvo + muzyka + tekhnologia +
                         fizicheskayaKultura + obz;
            totalScore /= 19.0f;

            // Округление до сотых
            totalScore = (float)Math.Round(totalScore, 2);

            return totalScore;
        }
    }
}
