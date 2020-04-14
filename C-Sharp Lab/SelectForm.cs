using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Sharp_Lab
{
    public partial class SelectForm : Form
    {
        public byte Age { get; private set; }
        public double Price { get; private set; }

        public SelectForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            Age = (byte)UpDownAge.Value;
            Price = tbPrice.Text != "" ? double.Parse(tbPrice.Text) : 0;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void tbPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < (int)'0' || e.KeyChar > (int)'9') && e.KeyChar != 8 && e.KeyChar != 44)
                e.Handled = true;
        }
    }
}
