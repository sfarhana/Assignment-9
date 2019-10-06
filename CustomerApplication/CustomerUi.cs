using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomerApplication.BLL;
using CustomerApplication.Model;

namespace CustomerApplication
{
    public partial class CustomerUi : Form
    {
        CustomerManager _CM = new CustomerManager();
        int CustId = 0;

        public CustomerUi()
        {
            InitializeComponent();
        }
        public void ComboLoadByTable()
        {
            DataTable datatable = new DataTable();

            datatable = _CM.ComboLoad();

            DataRow row = datatable.NewRow();
            //row[0] = -1;
            row[1] = "--Select--";
            datatable.Rows.InsertAt(row, 0);

            cmbDistrict.DataSource = datatable;
        }
        private void CustomerUi_Load(object sender, EventArgs e)
        {
            //ComboLoadByTable();
            try
            {
                DistrictModel d = new DistrictModel();
                d.DId = -1;
                d.District = "--Select--";

                List<DistrictModel> Dlist = _CM.ComboLoadByList();
                Dlist.Insert(0, d);

                cmbDistrict.DataSource = Dlist;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Combo Data Load Error When Form Load "+ex.Message.ToString());
            }
            
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                bool isChecked=FieldCheck();
                if (isChecked == true)
                {
                    SaveInfo();
                }
                else
                    return;
            }

            else
            {
                bool isChecked = FieldCheck();
                if (isChecked == true)
                {
                    UpdateInfo();
                }
                else
                    return;

            }

        }

        public bool FieldCheck()
        {
            try
            {
                if (string.IsNullOrEmpty(txtCode.Text))
                {
                    lblMsg.Text = "Code field can not be empty";
                    return false;
                }

                if (txtCode.Text.Length != 4)
                {
                    lblMsg.Text = "Code length must be 4 char";
                    return false;
                }

                bool isCode = _CM.IsCodeUnique(txtCode.Text, CustId);

                if (isCode == true)
                {
                    lblMsg.Text = "Code must be unique";
                    return false;
                }

                if (string.IsNullOrEmpty(txtName.Text))
                {
                    lblMsg.Text = "Name field can not be empty";
                    return false;
                }

                if (string.IsNullOrEmpty(txtContact.Text))
                {
                    lblMsg.Text = "Contact field can not be empty";
                    return false;
                }

                if (txtContact.Text.Length != 11)
                {
                    lblMsg.Text = "Contact length must be 11 char";
                    return false;
                }

                bool isContact = _CM.IsContactUnique(txtContact.Text, CustId);

                if (isContact == true)
                {
                    lblMsg.Text = "Contact must be unique";
                    return false;

                }

                int DId = Convert.ToInt16(cmbDistrict.SelectedValue.ToString());//entry time cmb value extract
                if (DId == -1)
                {
                    lblMsg.Text = "Please Select a District";
                    return false;
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show("Field Checking Error " + ex.Message.ToString());
            }

            return true;
        }




        public void SaveInfo()
        {
            try
            {
                Customer C = new Customer();
                C.Code = txtCode.Text;
                C.Name = txtName.Text;
                C.Address = txtAddress.Text;
                C.Contact = txtContact.Text;
                C.DId = Convert.ToInt16(cmbDistrict.SelectedValue.ToString());//entry time cmb value extract

                bool isSave = _CM.Insert(C);
                if (isSave == true)
                {
                    lblMsg.Text = "Data saved";
                    ClearControls();//to get ready for update
                    dataGridView.DataSource = _CM.ShowData();
                }
                else
                    lblMsg.Text = "Data save failed";
            }

            catch (Exception ex)
            {
                MessageBox.Show("Save Then Show Error " + ex.Message.ToString());
            }
            
        }

        public void UpdateInfo()
        {
            try
            {
                Customer c = new Customer();
                c.CustId = CustId;
                c.Code = txtCode.Text;
                c.Name = txtName.Text;
                c.Address = txtAddress.Text;
                c.Contact = txtContact.Text;
                c.DId = int.Parse(cmbDistrict.SelectedValue.ToString());

                bool isUpdate = _CM.Update(c);

                if (isUpdate == true)
                {
                    lblMsg.Text = "Update Successful";
                    dataGridView.DataSource = _CM.ShowData();
                    btnSave.Text = "Save";
                }
                else
                {
                    lblMsg.Text = "Update Failed";
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show("Update Then Show Error " + ex.Message.ToString());
            }
            
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index;
                index = e.RowIndex;
                DataGridViewRow row = dataGridView.Rows[index];

                txtCode.Text = row.Cells[3].Value.ToString();
                txtName.Text = row.Cells[4].Value.ToString();
                txtAddress.Text = row.Cells[5].Value.ToString();
                txtContact.Text = row.Cells[6].Value.ToString();
                cmbDistrict.SelectedValue = int.Parse(row.Cells[2].Value.ToString());

                CustId = int.Parse(row.Cells[1].Value.ToString());//save globally to update customer table

                btnSave.Text = "Update";
            }

            catch (Exception ex)
            {
                MessageBox.Show("Cell Double Click Then Load On Text Error " + ex.Message.ToString());
            }
            
        }

        public void ClearControls()
        { 
            txtCode.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtContact.Text = "";
            cmbDistrict.SelectedValue = -1;
        }

        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            
           this.dataGridView.Rows[e.RowIndex].Cells["Sl"].Value = (e.RowIndex + 1).ToString();
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView.DataSource = _CM.Search(txtSearch.Text);
            }

            catch (Exception ex)
            {
               MessageBox.Show("Error When Search A Particular Data " + ex.Message.ToString());
            }
            
        }
    }
}
