using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    public class QLSV
    {
        
        private static QLSV _Instance;
        public static QLSV Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new QLSV();
                }
                return _Instance;

            }
            private set { }
        }

        public Mydel D { get => d; set => d = value; }

        public delegate bool Mydel(object o1, object o2);
        private Mydel d;
        public List<string> GetLSH()
        {
            List<string> li = new List<string>();
            foreach (SinhVien i in CSDL.Instance.GetAllSV())
            {
                li.Add(i.LopSH);
            }
            return li;
        }
        public List<SinhVien> GetSV(string lopsh, string name,string mssv)
        {
            List<SinhVien> li = new List<SinhVien>();
            if (CSDL.Instance.CheckDatatable())
            {
                var allSv = CSDL.Instance.GetAllSV();
                foreach (SinhVien sv in allSv)
                {
                    if (sv.Name.Contains(name) && sv.MSSV.Contains(mssv))
                    {
                        if (lopsh != "All"&&sv.LopSH!= lopsh)
                        {
                            continue;
                        }
                        li.Add(sv);
                    }
                }
            }
            return li;
        }
        public void DelSV(List<string> li)
        {
            foreach (string i in li)
            {
                CSDL.Instance.Del_CSDL(i);
            }

        }
        public List<SinhVien> Sort(List<SinhVien> li)
        {
            for(int i=0;i<li.Count;i++)
            {
                for(int j=i+1;j<li.Count;j++)
                {
                    if (D(li[i] as object, li[j] as object))
                    {
                        (li[i], li[j]) = (li[j], li[i]);
                    }
                }
            }
            return li;
        }
    }
}
