using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuanLySinhVien
{
    public class CSDL
    {
        private DataTable dt;
        private static CSDL _Instance;
        public static CSDL Instance
        {
            get
            {
                if(_Instance==null)
                {
                    _Instance = new CSDL();
                    _Instance.InitDataTable();
                }
                return _Instance;
            }
            private set { }
        }
        private void InitDataTable()
        {

            dt = new DataTable();

            string[] columnNames = { "MSSV", "Name", "LopSH","Gender", "NS","DTB","Anh","Hocba","CCCD" };
            Type[] columnTypes = { typeof(string), typeof(string), typeof(string),typeof(bool), 
                typeof(DateTime),typeof(double),typeof(bool),typeof(bool),typeof(bool) };
            for(int i =0;i<columnNames.Length;i++)
            {
                DataColumn column = new DataColumn(columnNames[i], columnTypes[i]);
                dt.Columns.Add(column);
            }
            foreach (DataColumn item in dt.Columns)
            {
                item.ReadOnly = true;
            }
        }
        private CSDL()
        {

        }
        public void Add_CSDL(SinhVien sinhVien)
        {
            DataRow row= dt.NewRow();
            row["MSSV"] = sinhVien.MSSV;
            row["Name"] = sinhVien.Name;
            row["LopSH"] = sinhVien.LopSH;
            row["Gender"] = sinhVien.Gender;
            row["NS"] = sinhVien.NS;
            row["DTB"] = sinhVien.DTB;
            row["Anh"] = sinhVien.Anh;
            row["Hocba"] = sinhVien.Hocba;
            row["CCCD"] = sinhVien.CCCD;
            dt.Rows.Add(row);
        }
        public void Del_CSDL(string MSSV)
        {
            if (CheckDatatable())
            {
                List<DataRow> rowsToRemove = new List<DataRow>();
                foreach (DataRow row in dt.Rows)
                {
                    string mssvTmp = row["MSSV"].ToString();
                    if (mssvTmp == MSSV)
                    {
                        rowsToRemove.Add(row);
                    }
                }
                foreach (DataRow row in rowsToRemove)
                {
                    dt.Rows.Remove(row);
                }
            }

        }
        public void Update_CSDL(SinhVien s)
        {

        }
        public List<SinhVien> GetAllSV()
        {
            List<SinhVien> li = new List<SinhVien>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    bool gender;
                    DateTime ns;
                    double dtb;
                    bool anh;
                    bool hocba;
                    bool cccd;

                    bool.TryParse(row["Gender"].ToString(), out gender);
                    DateTime.TryParse(row["NS"].ToString(), out ns);
                    double.TryParse(row["DTB"].ToString(), out dtb);
                    bool.TryParse(row["Anh"].ToString(), out anh);
                    bool.TryParse(row["Hocba"].ToString(), out hocba);
                    bool.TryParse(row["CCCD"].ToString(), out cccd);

                    li.Add(new SinhVien()
                    {
                        MSSV = row["MSSV"].ToString(),
                        Name = row["Name"].ToString(),
                        LopSH = row["LopSH"].ToString(),
                        Gender = gender,
                        NS = ns,
                        DTB = dtb,
                        Anh = anh,
                        Hocba = hocba,
                        CCCD = cccd
                    });
                }
            }
            return li;
        }
        public bool CheckDatatable()
        {
            return dt != null && dt.Rows.Count > 0;
        }


    }
}
