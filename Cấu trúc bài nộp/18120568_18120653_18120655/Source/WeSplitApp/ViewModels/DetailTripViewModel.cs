using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSplitApp.Model;

namespace WeSplitApp.ViewModels
{
    public class DetailTripViewModel
    {
        public ChuyenDi ChuyenDi { get; set; }
        public List<ThanhVienKhoanThu> ThanhVienKhoanThus { get; set; }
        public List<KhoanChiTieu> KhoanChiTieus { get; set; }
        public List<HinhAnhChuyenDi> HinhAnhChuyenDis { get; set; }

        public DetailTripViewModel(ChuyenDi chuyenDi)
        {
            ThanhVienKhoanThus = new List<ThanhVienKhoanThu>();

            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {
                ChuyenDi = (from cd in db.ChuyenDis
                           where cd.IDChuyenDi == chuyenDi.IDChuyenDi
                           select cd).First();

                ChuyenDi.HinhAnh = Path.GetFullPath(ChuyenDi.HinhAnh);

                KhoanChiTieus = ChuyenDi.KhoanChiTieux.ToList();
                HinhAnhChuyenDis = ChuyenDi.HinhAnhChuyenDis.ToList();
                foreach(var hinh in HinhAnhChuyenDis)
                {
                    hinh.HinhAnh = Path.GetFullPath(hinh.HinhAnh);
                }

                var listThamGia = ChuyenDi.ThamGiaChuyenDis;
                var listKhoanThu = ChuyenDi.KhoanThus;

                var thanhVienKhoanThu = from tg in listThamGia
                                        join kt in listKhoanThu
                                        on tg.IDThanhVien equals kt.IDNguoiDongTien
                                        select new ThanhVienKhoanThu(tg.ThanhVien.TenThanhVien, tg.VaiTro, kt.SoTien);

                ThanhVienKhoanThus = thanhVienKhoanThu.ToList();
            }
        }
    }
}
