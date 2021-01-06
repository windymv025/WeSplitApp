using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSplitApp.Model;

namespace WeSplitApp.ViewModels
{
    public class ThanhVienKhoanThu
    {
        public string TenThanhVien { get; set; }
        public decimal SoTienThu { get; set; }
        public string ChucVu { get; set; }
        public ThanhVienKhoanThu(string ten, string chucvu, decimal soTien)
        {
            TenThanhVien = ten;
            ChucVu = chucvu;
            SoTienThu = soTien;
        }
    }

    public class AddTripViewModel
    {
        public ChuyenDi ChuyenDi { get; set; }
        public BindingList<KhoanChiTieu> KhoanChiTieus { get; set; }
        public List<ThamGiaChuyenDi> ThamGiaChuyenDis { get; set; }
        public BindingList<ThanhVienKhoanThu> ThanhVienKhoanThus { get; set; }
        public AddTripViewModel()
        {
            ChuyenDi = new ChuyenDi();
            KhoanChiTieus = new BindingList<KhoanChiTieu>();
            ThamGiaChuyenDis = new List<ThamGiaChuyenDi>();
            ThanhVienKhoanThus = new BindingList<ThanhVienKhoanThu>();
        }
        public void InsertTrip()
        {
            addThanhVien();
            using(DBWeSplitEntities db = new DBWeSplitEntities())
            {
                foreach (var tg in ThamGiaChuyenDis)
                {
                    tg.ChuyenDi = ChuyenDi;
                    db.ThamGiaChuyenDis.Add(tg);
                }
                db.SaveChanges();
            }
            addKhoanThu();
            addKhoanChiTieu();
        }
        public void addThanhVien()
        {
            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {
                var list = db.ThanhViens;
                foreach (var tv in ThanhVienKhoanThus)
                {
                    
                    bool isThanhVien = false;
                    foreach (var item in list)
                    {
                        if (tv.TenThanhVien == item.TenThanhVien)
                        {
                            isThanhVien = true;
                            ThamGiaChuyenDis.Add(new ThamGiaChuyenDi()
                            {
                                IDThanhVien = item.ID,
                                VaiTro = tv.ChucVu,
                            });
                        }
                    }
                    if (!isThanhVien)
                    {
                        ThanhVien thanhVien = new ThanhVien()
                        {
                            TenThanhVien = tv.TenThanhVien
                        };
                        ThamGiaChuyenDis.Add(new ThamGiaChuyenDi()
                        {
                            ThanhVien = thanhVien,
                            VaiTro = tv.ChucVu,
                        });
                    }
                    
                }
            }
        }
        public void addKhoanThu()
        {
            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {
                foreach(var thu in ThanhVienKhoanThus)
                {
                    var thanhVien = from tv in db.ThanhViens
                                    where tv.TenThanhVien == thu.TenThanhVien
                                    select tv;
                    db.KhoanThus.Add(new KhoanThu()
                    {
                        SoTien = thu.SoTienThu,
                        IDChuyenDi = this.ChuyenDi.IDChuyenDi,
                        IDNguoiDongTien = thanhVien.First().ID
                    }) ;
                }
                db.SaveChanges();
            }
        }
        public void addKhoanChiTieu()
        {
            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {

                foreach (var chi in KhoanChiTieus)
                {
                    chi.IDChuyenDi = this.ChuyenDi.IDChuyenDi;
                    db.KhoanChiTieux.Add(chi);
                }
                db.SaveChanges();
            }
        }
    }
}
