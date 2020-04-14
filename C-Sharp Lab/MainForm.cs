using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace C_Sharp_Lab
{
    public partial class MainForm : Form
    {
        static string initPath = Application.StartupPath + "\\Files";

        FileInfo currentFile;
        List<Toy> toys;
        List<Toy> unselectedToys;

        bool ListIsChanged;
        bool ListIsExist;
        
        public MainForm()
        {
            InitializeComponent();
            ToyTypesConverter.InitDict();
            dataGridView.AutoGenerateColumns = true;
            dataGridView.MultiSelect = false;
            saveFileDialog.InitialDirectory = initPath;
            openFileDialog.InitialDirectory = initPath;
        }
        
        //buttons in menu

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ListIsExist)
            {
                if (!TryDeleteList())
                    return;
            }
            InitWorkWithList();
            GenerateTestList();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFile == null && !GetSaveDialog())
                return;
            SaveList();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetSaveDialog())
                return;
            SaveList();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            if (ListIsExist && !TryDeleteList())
                return;
            
            InitWorkWithList(openFileDialog.FileName);

            IToyListService loadList = ServiceFactory.getService(currentFile.Extension);
            toys = loadList.Load(currentFile.FullName);
            RefreshGrid();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toys.Clear();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TryDeleteList();
        }

        //buttons on form

        private void btnAdd_Click(object sender, EventArgs e)
        {           
            FormToy formToy = new FormToy();
            if (formToy.ShowDialog() == DialogResult.OK)
            {
                toys.Add(formToy.toy);
                RefreshGrid();
                ListIsChanged = true;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            Toy toy = GetSelectedToy();
            if (toy == null)
                return;

            FormToy formToy = new FormToy(toy);
            if (formToy.ShowDialog() == DialogResult.OK)
            {
                Toy fToy = formToy.toy;
                toy.Change(fToy.Type, fToy.Age, fToy.Price);
                RefreshGrid();
                ListIsChanged = true;
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Toy toy = GetSelectedToy();
            if (toy == null)
                return;

            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Deleting",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                  MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
                return;

            toys.Remove(toy);
            RefreshGrid();
            ListIsChanged = true;
        }

        //main task

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectForm selectForm = new SelectForm();
            if (selectForm.ShowDialog() == DialogResult.OK)
            {
                ToysListHelper toysHelper = new ToysListHelper(this.toys);
                byte minAge = selectForm.Age;
                double maxPrice = selectForm.Price;

                List<Toy> selectedToys = toysHelper.selectMinAgeMaxPrice(minAge, maxPrice);
                unselectedToys = toysHelper.ListDif(selectedToys);
                this.toys = selectedToys;
                RefreshGrid();

                selectToolStripMenuItem.Click -= selectToolStripMenuItem_Click;
                selectToolStripMenuItem.Click += returnAllToyToGrid;
                selectToolStripMenuItem.Text = "Return";

            }
        }

        private void returnAllToyToGrid(object sender, EventArgs e)
        {
            toys.AddRange(unselectedToys);
            RefreshGrid();
            selectToolStripMenuItem.Text = "Select";
            selectToolStripMenuItem.Click += selectToolStripMenuItem_Click;
            selectToolStripMenuItem.Click -= returnAllToyToGrid;
        }

        //auxiliary

        private void RefreshGrid()
        {
            dataGridView.DataSource = null;
            dataGridView.DataSource = toys;
        }

        private bool GetSaveDialog()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return false;
            SetNewFile(saveFileDialog.FileName);
            return true;
        }

        private void SaveList()
        {
            IToyListService saveList = ServiceFactory.getService(currentFile.Extension);
            saveList.Save(toys, currentFile.FullName);
            ListIsChanged = false;
        }

        private Toy GetSelectedToy()
        {
            return (Toy)dataGridView.SelectedRows[0].DataBoundItem;
        }

        private void InitWorkWithList(string fileName = null)
        {
            InitFields();
            if (fileName != null)
                SetNewFile(fileName);
            ButtonsSwitch(true);
        }

        private bool TryDeleteList()
        {
            if (ListIsChanged && !ApproveDeletingModified())
                return false;
            
            ClearFields();
            ButtonsSwitch(false);
            return true;
        }

        private bool ApproveDeletingModified()
        {
            DialogResult result = MessageBox.Show("The file has been modified. Save changes?", "Deleting",
                                                  MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                                                  MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Cancel)
                return false;

            if (result == DialogResult.Yes)
                SaveList();

            return true;
        }

        private void SetNewFile(string fileName)
        {
            currentFile = new FileInfo(fileName);
            saveFileDialog.InitialDirectory = currentFile.DirectoryName;
            openFileDialog.InitialDirectory = currentFile.DirectoryName;
        }

        private void InitFields()
        {
            toys = new List<Toy>();
            ListIsChanged = false;
            ListIsExist = true;
        }

        private void ClearFields()
        {
            toys = null;
            ListIsExist = false;
            currentFile = null;
        }

        private void ButtonsSwitch(bool flag)
        {
            btnAdd.Enabled = flag;
            btnChange.Enabled = flag;
            btnDelete.Enabled = flag;
            selectToolStripMenuItem.Enabled = flag;
            saveToolStripMenuItem.Enabled = flag;
            saveAsToolStripMenuItem.Enabled = flag;
            clearToolStripMenuItem.Enabled = flag;
            closeToolStripMenuItem.Enabled = flag;
        }
        
        private void GenerateTestList()
        {
            Toy toy1 = new Toy(ToyTypes.Doll, 6, 3000);
            toys.Add(toy1);
            Toy toy2 = new Toy(ToyTypes.Ball, 3, 1000);
            toys.Add(toy2);
            Toy toy3 = new Toy(ToyTypes.Cubes, 2, 8000);
            toys.Add(toy3);
            Toy toy4 = new Toy(ToyTypes.Car, 3, 500);
            toys.Add(toy4);
            RefreshGrid();
        }
    }
}
