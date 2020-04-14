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
    public partial class FormToy : Form
    {        
        public Toy toy { get; private set; }

        public FormToy(Toy toy = null)
        {
            InitializeComponent();
            this.toy = toy;
            InitFields();
        }

        private void InitFields()
        {
            comboBox.DataSource = new BindingSource(ToyTypesConverter.ToyTypesDict, null);
            comboBox.DisplayMember = "Value";
            comboBox.ValueMember = "Key";
            

            if (toy != null)
            {
                comboBox.SelectedIndex = (int)toy.Type - 1;
                UpDownAge.Value = toy.Age;
                tbPrice.Text = toy.Price.ToString();
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            int selectedType = comboBox.SelectedIndex + 1;
            ToyTypes type = ToyTypesConverter.ToyTypesDict[selectedType];
            byte age = (byte)UpDownAge.Value;
            double price = tbPrice.Text != "" ? double.Parse(tbPrice.Text) : 0;

            toy = new Toy(type, age, price);

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
