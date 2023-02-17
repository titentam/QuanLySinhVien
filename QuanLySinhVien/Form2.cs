using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class Form2 : Form
    {

        string _MSSV;
        SinhVien moi = new SinhVien();
        public Form2()
        {
            InitializeComponent();
            InitCbLopSh();
            InitDataAdd();
        }
        public Form2(string mssv)
        {
            InitializeComponent();
            _MSSV = mssv;
            InitCbLopSh();
            InitDataEdit();
            
            
        }
  
        #region Inits
        private void InitCbLopSh()
        {
            foreach (string lop in QLSV.Instance.GetLSH().Distinct())
            {
                cbLopSh.Items.Add(lop);
            }
            cbLopSh.SelectedIndex = 0;
        }
        private void InitDataEdit()
        {
            foreach (SinhVien sv in CSDL.Instance.GetAllSV())
            {
                if (sv.MSSV == _MSSV)
                {
                    txtMSSV.Text = sv.MSSV;
                    txtName.Text = sv.Name;
                    txtDTB.Text = sv.DTB.ToString();
                    dtNs.Text = sv.NS.ToString();
                    if (sv.Gender)
                    {
                        rbtnMale.Checked = true;
                    }
                    else
                    {
                        rbtnFemale.Checked = true;
                    }
                    if (sv.Anh) checkAnh.Checked = true;
                    if (sv.Hocba) checkHocBa.Checked = true;
                    if (sv.CCCD) checkCCCD.Checked = true;
                    cbLopSh.SelectedIndex = cbLopSh.Items.IndexOf(sv.LopSH);
                    moi = sv;

                }
            }
            txtMSSV.Enabled = false;
            btnOk.Click += btnOk_ClickEdit;

        }
        private void InitDataAdd()
        {

            txtMSSV.Enabled = true;
            btnOk.Click += btnOk_ClickAdd;
            rbtnMale.Checked = true;

        }
        #endregion


        #region Events
        private void TextChange(object sender, EventArgs e)
        {
            TextBox tmp = sender as TextBox;
            if (tmp.Tag.ToString() == "Name")
            {
                moi.Name = tmp.Text;
            }
            else if (tmp.Tag.ToString() == "DTB")
            {
                if (Double.TryParse(tmp.Text, out double dtb))
                {
                    moi.DTB = dtb;
                }
                else
                {
                    moi.DTB = 0;
                }
            }
            else if (tmp.Tag.ToString() == "MSSV")
            {
                moi.MSSV = tmp.Text;
            }

        }
        private void rbtnMale_CheckedChanged(object sender, EventArgs e)
        {
            moi.Gender = rbtnMale.Checked;
        }

        private void CheckChanged(object sender, EventArgs e)
        {
            CheckBox tmp = sender as CheckBox;
            if (tmp.Tag.ToString() == "Anh") moi.Anh = tmp.Checked;
            if (tmp.Tag.ToString() == "HocBa") moi.Hocba = tmp.Checked;
            if (tmp.Tag.ToString() == "CCCD") moi.CCCD = tmp.Checked;
        }
        private void dtNs_ValueChanged(object sender, EventArgs e)
        {
            moi.NS = dtNs.Value;
        }
        private void btnOk_ClickEdit(object sender, EventArgs e)
        {
            List<string> li = new List<string>();
            foreach (SinhVien sv in CSDL.Instance.GetAllSV())
            {
                if (sv.MSSV == _MSSV)
                {

                    li.Add(sv.MSSV);
                    break;

                }
            }
            QLSV.Instance.DelSV(li);
            CSDL.Instance.Add_CSDL(moi);
            this.Dispose();
            Form1.Instance.btnSearch_Click(sender, e);

        }
        private void cbLopSh_SelectedIndexChanged(object sender, EventArgs e)
        {
            moi.LopSH = cbLopSh.SelectedItem.ToString();
        }
        private void btnOk_ClickAdd(object sender, EventArgs e)
        {

            bool valid = true;
            bool check = true;
            moi.MSSV= txtMSSV.Text;
            moi.Name= txtName.Text;
            moi.NS = dtNs.Value;
            moi.NS =Convert.ToDateTime( moi.NS.ToShortDateString());

            foreach (SinhVien sv in CSDL.Instance.GetAllSV())
            {
                if (sv.MSSV == moi.MSSV)
                {

                    MessageBox.Show("Sinh vien nay da ton tai!");
                    valid= false;
                    check = false;
                    break;

                }
            }
            if(valid )
            {
                if (moi.MSSV != "" && moi.Name != "" && moi.LopSH != "")
                {
                    CSDL.Instance.Add_CSDL(moi);
                }
                else
                {
                    MessageBox.Show("Thieu thong tin");
                    check = false;
                }
            }
            if(check)
            {
                this.Dispose();
                Form1.Instance.btnSearch_Click(sender,e);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion

       
    }
}
