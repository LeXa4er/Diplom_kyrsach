using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Приемная_комиссия_By_LeXa
{
    public partial class special : Form
    {
        public special()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            glav_forms glavForm = new glav_form(this); // Передача текущей формы в конструктор glav_forms
            glavForm.Show();
        }
    }
}
