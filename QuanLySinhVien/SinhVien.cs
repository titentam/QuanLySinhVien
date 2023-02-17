using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{
    public class SinhVien
    {

        public string MSSV { get; set; }
        public string Name { get; set; }
        public string LopSH { get; set; }
        public bool Gender { get; set; }
        public DateTime NS { set; get; }
        public double DTB { set; get; }
        public bool Anh { set; get; }
        public bool Hocba { set; get; }
        public bool CCCD { set; get; }

        public static bool CompareMSSV(object o1, object o2)
        {
            SinhVien sv1 = o1 as SinhVien;
            SinhVien sv2 = o2 as SinhVien;
            return sv1.MSSV.CompareTo(sv2.MSSV)>0;
        }
        public static bool CompareName(object o1, object o2)
        {
            SinhVien sv1 = o1 as SinhVien;
            SinhVien sv2 = o2 as SinhVien;
            return sv1.Name.CompareTo(sv2.Name) > 0;
        }
        public static bool CompareDTB(object o1, object o2)
        {
            SinhVien sv1 = o1 as SinhVien;
            SinhVien sv2 = o2 as SinhVien;
            return sv1.DTB>sv2.DTB;
        }
    }
}
