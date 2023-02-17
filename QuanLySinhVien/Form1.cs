using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;
        public Form1()
        {
            InitializeComponent();
            InitData();
            InitLopSH();
            InitCbSort();
            gvTable.DataSource = CSDL.Instance.GetAllSV();
            Instance = this;
        }

        #region Methods
        private void InitData()
        {
            string[] tmp = File.ReadAllLines("DataSV.txt");
            foreach (string s in tmp)
            {
                SinhVien sv = new SinhVien();
                string[] property = s.Split(',');
                sv.MSSV = property[0];
                sv.Name = property[1];
                sv.LopSH = property[2];
                sv.Gender = Convert.ToBoolean(property[3]);
                sv.NS = Convert.ToDateTime(property[4]);    
                sv.DTB = Convert.ToDouble(property[5]);    
                sv.Anh = Convert.ToBoolean(property[6]);
                sv.Hocba = Convert.ToBoolean(property[7]);
                sv.CCCD = Convert.ToBoolean(property[8]);
                CSDL.Instance.Add_CSDL(sv);
            }


        }
        private void InitCbSort()
        {
            cbSort.Items.Add("MSSV");
            cbSort.Items.Add("Name");
            cbSort.Items.Add("DTB");
            cbSort.SelectedIndex = 0;
        }
        private void InitLopSH()
        {
            cbLopSH.Items.Add("All");
            var lopsh = QLSV.Instance.GetLSH().Distinct();
            foreach (string i in lopsh)
            {
                cbLopSH.Items.Add(i); 
            }
            cbLopSH.SelectedIndex = 0;
            
        }
        #endregion

        #region Events
        private void cbLopSH_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbLopSH.SelectedIndex == 0)
            {
                gvTable.DataSource = CSDL.Instance.GetAllSV();
            }
            else
            {
                gvTable.DataSource = QLSV.Instance.GetSV(cbLopSH.Text, "", "");
            }
            txtSearch.Text = "";
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Contains(','))
            {
                string[] nameAndID = txtSearch.Text.Split(',');
                nameAndID[1] = nameAndID[1].Trim();
                gvTable.DataSource = QLSV.Instance.GetSV(cbLopSH.Text, nameAndID[0], nameAndID[1]);
            }
            else
            {
                gvTable.DataSource = QLSV.Instance.GetSV(cbLopSH.Text, txtSearch.Text, "");
            }

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (gvTable.SelectedRows.Count > 0)
            {
                List<string> list = new List<string>();
                foreach (DataGridViewRow row in gvTable.SelectedRows)
                {
                    list.Add(row.Cells[0].Value.ToString());
                }
                QLSV.Instance.DelSV(list);
                btnSearch_Click(sender, e);
            }
            btnSearch_Click(sender, e);
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            string ab = cbSort.SelectedItem.ToString();
            if (ab == "MSSV")
            {
                QLSV.Instance.D = SinhVien.CompareMSSV;
            }
            else if (ab == "Name")
            {
                QLSV.Instance.D = SinhVien.CompareName;
            }
            else
            {
                QLSV.Instance.D = SinhVien.CompareDTB;
            }
            List<SinhVien> li = new List<SinhVien>();
            foreach (DataGridViewRow row in gvTable.Rows)
            {
                bool gender;
                DateTime ns;
                double dtb;
                bool anh;
                bool hocba;
                bool cccd;

                bool.TryParse(row.Cells["Gender"].Value.ToString(), out gender);
                DateTime.TryParse(row.Cells["NS"].Value.ToString(), out ns);
                double.TryParse(row.Cells["DTB"].Value.ToString(), out dtb);
                bool.TryParse(row.Cells["Anh"].Value.ToString(), out anh);
                bool.TryParse(row.Cells["Hocba"].Value.ToString(), out hocba);
                bool.TryParse(row.Cells["CCCD"].Value.ToString(), out cccd);

                li.Add(new SinhVien()
                {
                    MSSV = row.Cells["MSSV"].Value.ToString(),
                    Name = row.Cells["Name"].Value.ToString(),
                    LopSH = row.Cells["LopSH"].Value.ToString(),
                    Gender = gender,
                    NS = ns,
                    DTB = dtb,
                    Anh = anh,
                    Hocba = hocba,
                    CCCD = cccd
                });
            }
            gvTable.DataSource = QLSV.Instance.Sort(li);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (gvTable.SelectedRows.Count == 1)
            {
                string mssv = gvTable.SelectedRows[0].Cells[0].Value.ToString();
                Form2 f2 = new Form2(mssv);
                f2.ShowDialog();

            }

        }
        #endregion

    }
}
