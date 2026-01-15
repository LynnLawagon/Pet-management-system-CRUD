using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Pet_Information_Table;

namespace Pet_Management_System
{
    public partial class frmPet : Form
    {
        Pet p = new Pet();
        String sql = "";
        public frmPet()
        {
            InitializeComponent();
        }

        private void frmPet_Load(object sender, EventArgs e)
        {
            p.dbConnection("db_pet_test");
            p.displayRecords("SELECT * FROM tbl_pet", DGPet);

            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGender.Items.Add("Male");
            cboGender.Items.Add("Female");


            cboType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboType.Items.Add("Dog");
            cboType.Items.Add("Cat");

            Clear();
        }

        private void Clear()
        {
            txtID.Clear();
            txtName.Clear();
            cboGender.SelectedIndex = -1;
            cboType.SelectedIndex = -1;

            txtID.Enabled = true;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            int id = p.displayRecords("SELECT * FROM tbl_pet ORDER by pet_id desc ").Rows[0].Field<int>(0) + 1;
            txtID.Text = id.ToString();
        }

        public void change()
        {
            txtID.Enabled = false;
            btnAdd.Enabled = false;
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            sql = "INSERT INTO `db_pet_test`.`tbl_pet` (`pet_id`, `pet_name`, `pet_gender`, `pet_type`) VALUES (" + txtID.Text + ",' " + txtName.Text + "','" +
                                                        cboGender.SelectedItem + "','" + cboType.SelectedItem + "');";
            p.SQLManager(sql);
            p.displayRecords("SELECT * FROM tbl_pet", DGPet);
            Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void DGPet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtID.Text = DGPet[0, i].Value.ToString();
            txtName.Text = DGPet[1, i].Value.ToString();
            cboGender.Text = DGPet[2, i].Value.ToString();
            cboType.Text = DGPet[3, i].Value.ToString();

            change();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sql = "UPDATE `db_pet_test`.`tbl_pet` SET " + "pet_name = '" + txtName.Text + "'," + "pet_gender = '" + cboGender.SelectedItem + "'," + "pet_type = '" + cboType.SelectedItem
                                                                + "' " + "WHERE pet_id = " + txtID.Text + ";";

            p.SQLManager(sql);
            p.displayRecords("SELECT * FROM tbl_pet", DGPet);
            Clear();
        }

        private void frmPet_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sql = "DELETE FROM `db_pet_test`.`tbl_pet` WHERE  `pet_id`= " + txtID.Text + ";";

            p.SQLManager(sql);
            p.displayRecords("SELECT * FROM tbl_pet", DGPet);
            Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            sql = $"SELECT * FROM tbl_pet WHERE pet_name LIKE '%{txtSearch.Text}%'";
            p.displayRecords(sql, DGPet);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
