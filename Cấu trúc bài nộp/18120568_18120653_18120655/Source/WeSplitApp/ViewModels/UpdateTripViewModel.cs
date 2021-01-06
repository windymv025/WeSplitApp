using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeSplitApp.Model;
using WeSplitApp.ViewModels;

namespace WeSplitApp.ViewModels
{
    public class SaveImage
    {
        public static string tempUriImage; //Ảnh được load
        public List<string> listFiles = new List<string>(); //List files ảnh sẽ được lưu
        public List<string> listImageName = new List<string>(); //List name ảnh đã được lưu theo thứ tự 1->n

        public void Save_Image()
        {
            if (listFiles != null)
            {
                
                var currentFolder = AppDomain.CurrentDomain.BaseDirectory.ToString();
                var imagesFolder = $"{currentFolder}Images";

                //Lấy từng file ảnh copy vào Images của project
                foreach (var file in listFiles)
                {
                    var info = new FileInfo(file);
                    var newName = $"{Guid.NewGuid()}{info.Extension}";
                    var newImageAvatarPath = String.Format(imagesFolder + "\\" + newName);
                    File.Copy(file, newImageAvatarPath, true);

                    listImageName.Add($"Images/{newName}");
                }
            }
        }

        public void Free_Memory()
        {
            listFiles.Clear();
            tempUriImage = null;
            listImageName.Clear();
        }
    }
    public class UpdateTripViewModel
    {
        public SaveImage SaveImage { get; set; }
        public ChuyenDi ChuyenDi { get; set; }
        public BindingList<ThanhVienKhoanThu> ThanhVienKhoanThus { get; set; }
        public BindingList<KhoanChiTieu> KhoanChiTieus { get; set; }
        public BindingList<HinhAnhChuyenDi> HinhAnhChuyenDis { get; set; }
        public List<ThamGiaChuyenDi> ThamGiaChuyenDis { get; set; }

        public UpdateTripViewModel(ChuyenDi chuyenDi)
        {
            SaveImage = new SaveImage();
            ThanhVienKhoanThus = new BindingList<ThanhVienKhoanThu>();
            KhoanChiTieus = new BindingList<KhoanChiTieu>();
            HinhAnhChuyenDis = new BindingList<HinhAnhChuyenDi>();
            ThamGiaChuyenDis = new List<ThamGiaChuyenDi>();
            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {
                ChuyenDi = (from cd in db.ChuyenDis
                            where cd.IDChuyenDi == chuyenDi.IDChuyenDi
                            select cd).First();
                ChuyenDi.HinhAnh = Path.GetFullPath(ChuyenDi.HinhAnh);
                foreach (var chiTieu in ChuyenDi.KhoanChiTieux)
                {
                    KhoanChiTieus.Add(chiTieu);
                }
                foreach (var hinh in ChuyenDi.HinhAnhChuyenDis)
                {
                    HinhAnhChuyenDis.Add(hinh);
                }
                foreach (var hinh in HinhAnhChuyenDis)
                {
                    hinh.HinhAnh = Path.GetFullPath(hinh.HinhAnh);
                }
                ThamGiaChuyenDis = ChuyenDi.ThamGiaChuyenDis.ToList();
                var listThamGia = ChuyenDi.ThamGiaChuyenDis;
                var listKhoanThu = ChuyenDi.KhoanThus;

                var thanhVienKhoanThu = from tg in listThamGia
                                        join kt in listKhoanThu
                                        on tg.IDThanhVien equals kt.IDNguoiDongTien
                                        select new ThanhVienKhoanThu(tg.ThanhVien.TenThanhVien, tg.VaiTro, kt.SoTien);

                foreach (var tv in thanhVienKhoanThu)
                {
                    ThanhVienKhoanThus.Add(tv);
                }
            }
        }

        public void updateTrip()
        {
            SaveImage.Save_Image();
            UpdateHinhAnhChuyenDi();
            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {
                var chuyenDi = db.ChuyenDis.FirstOrDefault(cd => cd.IDChuyenDi == ChuyenDi.IDChuyenDi);
                chuyenDi.HinhAnh = ChuyenDi.HinhAnh;
                chuyenDi.GioiThieu = ChuyenDi.GioiThieu;
                chuyenDi.NgayBatDau = ChuyenDi.NgayBatDau;
                chuyenDi.NgayKetThuc = ChuyenDi.NgayKetThuc;
                chuyenDi.TenChuyenDi = ChuyenDi.TenChuyenDi;
                chuyenDi.LoTrinh = ChuyenDi.LoTrinh;
                db.SaveChanges();
            }
        }
        public void addKhoanChiTieu(KhoanChiTieu chiTieu)
        {
            KhoanChiTieus.Add(chiTieu);
            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {
                db.KhoanChiTieux.Add(chiTieu);
                db.SaveChanges();
            }
        }
        public void addThanhVienKhoanThu(ThanhVienKhoanThu thanhVienKhoanThu)
        {
            ThanhVienKhoanThus.Add(thanhVienKhoanThu);
            addThanhVien(thanhVienKhoanThu);
            addKhoanThu(thanhVienKhoanThu);
        }
        private void addThanhVien(ThanhVienKhoanThu thanhVienKhoanThu)
        {
            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {
                var list = db.ThanhViens;
                ThamGiaChuyenDi thamGiaChuyenDi = null;
                bool isThanhVien = false;
                foreach (var item in list)
                {
                    if (thanhVienKhoanThu.TenThanhVien == item.TenThanhVien)
                    {
                        isThanhVien = true;
                        thamGiaChuyenDi = new ThamGiaChuyenDi()
                        {
                            IDThanhVien = item.ID,
                            VaiTro = thanhVienKhoanThu.ChucVu,
                            IDChuyenDi = ChuyenDi.IDChuyenDi
                        };
                        ThamGiaChuyenDis.Add(thamGiaChuyenDi);
                        break;
                    }
                }
                if (!isThanhVien)
                {
                    ThanhVien thanhVien = new ThanhVien()
                    {
                        TenThanhVien = thanhVienKhoanThu.TenThanhVien
                    };
                    thamGiaChuyenDi = new ThamGiaChuyenDi()
                    {
                        ThanhVien = thanhVien,
                        VaiTro = thanhVienKhoanThu.ChucVu,
                        IDChuyenDi = ChuyenDi.IDChuyenDi
                    };
                    ThamGiaChuyenDis.Add(thamGiaChuyenDi);
                }

                db.ThamGiaChuyenDis.Add(thamGiaChuyenDi);
                db.SaveChanges();
            }
        }
        private void addKhoanThu(ThanhVienKhoanThu thanhVienKhoanThu)
        {
            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {

                var thanhVien = from tv in db.ThanhViens
                                where tv.TenThanhVien == thanhVienKhoanThu.TenThanhVien
                                select tv;

                db.KhoanThus.Add(new KhoanThu()
                {
                    SoTien = thanhVienKhoanThu.SoTienThu,
                    IDChuyenDi = this.ChuyenDi.IDChuyenDi,
                    IDNguoiDongTien = thanhVien.First().ID
                });

                db.SaveChanges();
            }
        }

        public void UpdateHinhAnhChuyenDi()
        {
            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {
                var list = ChuyenDi.HinhAnhChuyenDis;

                foreach(var item in list)
                {
                    bool isDelete = true;
                    foreach(var hinh in HinhAnhChuyenDis)
                    {
                        if(item.IDHinhAnh == hinh.IDHinhAnh)
                        {
                            isDelete = false;
                            break;
                        }
                    }
                    if(isDelete)
                    {
                        db.HinhAnhChuyenDis.Remove(db.HinhAnhChuyenDis.FirstOrDefault(h => h.IDHinhAnh == item.IDHinhAnh));
                    }
                }

                if (SaveImage.listImageName != null)
                {
                    foreach (var hinh in SaveImage.listImageName)
                    {
                        db.HinhAnhChuyenDis.Add(
                            new HinhAnhChuyenDi()
                            {
                                HinhAnh = hinh,
                                IDChuyenDi = ChuyenDi.IDChuyenDi
                            });
                    }
                }
                db.SaveChanges();
            }
        }

        public void xoaHinhAnhChuyenDi(int index)
        {
            var hinhAnh = HinhAnhChuyenDis[index];
            HinhAnhChuyenDis.RemoveAt(index);
            //using(DBWeSplitEntities db = new DBWeSplitEntities())
            //{
            //    db.HinhAnhChuyenDis.Remove(
            //        db.HinhAnhChuyenDis.FirstOrDefault(
            //            h => h.IDHinhAnh == hinhAnh.IDHinhAnh
            //            ));

            //    db.SaveChanges();
            //}
        }

        public void xoaThanhVienKhoanThu(int index)
        {
            var thanhVienKhoanThu = ThanhVienKhoanThus[index];
            ThanhVienKhoanThus.RemoveAt(index);
            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {
                var thanhVien = from item in db.ThanhViens
                                where item.TenThanhVien == thanhVienKhoanThu.TenThanhVien
                                select item;
                ThanhVien tv = thanhVien.FirstOrDefault();

                db.KhoanThus.Remove(db.KhoanThus.FirstOrDefault(kt => kt.IDChuyenDi == ChuyenDi.IDChuyenDi && kt.IDNguoiDongTien == tv.ID));

                db.SaveChanges();
            }
        }

        public void xoaKhoanChi(int index)
        {
            var khoanChi = KhoanChiTieus[index];
            using(DBWeSplitEntities db = new DBWeSplitEntities())
            {
                db.KhoanChiTieux.Remove(db.KhoanChiTieux.FirstOrDefault(ct => ct.IDKhoanChi == khoanChi.IDKhoanChi));
                db.SaveChanges();
                KhoanChiTieus.RemoveAt(index);
            }
        }
    }
}
