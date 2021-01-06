using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WeSplitApp.Model;

namespace WeSplitApp.ViewModels
{
    class ChuyenDiDAO
    {
        public static List<ChuyenDi> GetAll()
        {
            List<ChuyenDi> list = new List<ChuyenDi>();

            using(DBWeSplitEntities db = new DBWeSplitEntities())
            {
                list = db.ChuyenDis.ToList();
            }

            return list;
        }
        public static List<ChuyenDi> GetChuyenDiFinish()
        {
            List<ChuyenDi> list = new List<ChuyenDi>();

            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {
                var listChuyenDi = db.ChuyenDis.ToList();
                var resulf = from f in listChuyenDi
                             where f.NgayKetThuc.CompareTo(DateTime.Now) <= 0
                             select f;
                list = resulf.ToList();
                foreach(var item in list)
                {
                    item.HinhAnh = Path.GetFullPath(item.HinhAnh);
                }
            }

            return list;
        }
        public static List<ChuyenDi> GetChuyenDiFinishByName(string name)
        {
            List<ChuyenDi> list = new List<ChuyenDi>();

            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {
                string sql = $"select * from ChuyenDi where freetext(TenChuyenDi, N'%{name}%')";
                var listChuyenDi = db.ChuyenDis.SqlQuery(sql);
                var resulf = from f in listChuyenDi
                             where f.NgayKetThuc.CompareTo(DateTime.Now) <= 0
                             select f;
                list = resulf.ToList();
                foreach (var item in list)
                {
                    item.HinhAnh = Path.GetFullPath(item.HinhAnh);
                }
            }

            return list;
        }
        public static List<ChuyenDi> GetChuyenDiComing()
        {
            List<ChuyenDi> list = new List<ChuyenDi>();

            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {
                var listChuyenDi = db.ChuyenDis.ToList();
                var resulf = from f in listChuyenDi
                             where f.NgayKetThuc.CompareTo(DateTime.Now) == 1
                             select f;
                list = resulf.ToList();
                foreach (var item in list)
                {
                    item.HinhAnh = Path.GetFullPath(item.HinhAnh);
                }
            }

            return list;
        }
        public static List<ChuyenDi> GetChuyenDiComingByName(string name)
        {
            List<ChuyenDi> list = new List<ChuyenDi>();

            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {
                string sql = $"select * from ChuyenDi where freetext(TenChuyenDi, N'%{name}%')";
                var listChuyenDi = db.ChuyenDis.SqlQuery(sql);
                var resulf = from f in listChuyenDi
                             where f.NgayKetThuc.CompareTo(DateTime.Now) == 1
                             select f;
                list = resulf.ToList();
                foreach (var item in list)
                {
                    item.HinhAnh = Path.GetFullPath(item.HinhAnh);
                }
            }

            return list;
        }
        public static List<ChuyenDi> GetChuyenDiComingByMemberName(string name)
        {
            List<ChuyenDi> list = new List<ChuyenDi>();

            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {
                string sql = $"select ChuyenDi.IDChuyenDi, ChuyenDi.TenChuyenDi, ChuyenDi.NgayBatDau, ChuyenDi.NgayKetThuc, ChuyenDi.GioiThieu, ChuyenDi.LoTrinh, ChuyenDi.HinhAnh from ChuyenDi, ThamGiaChuyenDi, ThanhVien where ChuyenDi.IDChuyenDi = ThamGiaChuyenDi.IDChuyenDi and ThamGiaChuyenDi.IDThanhVien = ThanhVien.ID and freetext(ThanhVien.TenThanhVien, N'%{name}%')";
                var listChuyenDi = db.ChuyenDis.SqlQuery(sql);
                var resulf = from f in listChuyenDi
                             where f.NgayKetThuc.CompareTo(DateTime.Now) == 1
                             select f;
                list = resulf.ToList();
                foreach (var item in list)
                {
                    item.HinhAnh = Path.GetFullPath(item.HinhAnh);
                }
            }

            return list;
        }
        public static List<ChuyenDi> GetChuyenDiFinishedByMemberName(string name)
        {
            List<ChuyenDi> list = new List<ChuyenDi>();

            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {
                string sql = $"select ChuyenDi.IDChuyenDi, ChuyenDi.TenChuyenDi, ChuyenDi.NgayBatDau, ChuyenDi.NgayKetThuc, ChuyenDi.GioiThieu, ChuyenDi.LoTrinh, ChuyenDi.HinhAnh from ChuyenDi, ThamGiaChuyenDi, ThanhVien where ChuyenDi.IDChuyenDi = ThamGiaChuyenDi.IDChuyenDi and ThamGiaChuyenDi.IDThanhVien = ThanhVien.ID and freetext(ThanhVien.TenThanhVien, N'%{name}%')";
                var listChuyenDi = db.ChuyenDis.SqlQuery(sql);
                var resulf = from f in listChuyenDi
                             where f.NgayKetThuc.CompareTo(DateTime.Now) <= 0
                             select f;
                list = resulf.ToList();
                foreach (var item in list)
                {
                    item.HinhAnh = Path.GetFullPath(item.HinhAnh);
                }
            }

            return list;
        }
    }
    public class PagingInfo
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int NumberOfTripInPerPage { get; set; }

        public PagingInfo(int numberOftripInPerPage, int total)
        {
            if (total > 0)
            {
                CurrentPage = 1;
                NumberOfTripInPerPage = numberOftripInPerPage;
                TotalPage = total / numberOftripInPerPage +
                    ((total % numberOftripInPerPage) == 0 ? 0 : 1);
            }
            else
            {
                CurrentPage = 0;
                TotalPage = 0;
            }
            
        }
    }
    public class HomeViewModel
    {
        public List<ChuyenDi> ChuyenDis { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public List<ChuyenDi> loadPage(int pageNumber, int numItemInPage)
        {
            List<ChuyenDi> resulf = new List<ChuyenDi>();
            PagingInfo.NumberOfTripInPerPage = numItemInPage;

            if (PagingInfo.TotalPage > 0)
            {
                if (pageNumber > PagingInfo.TotalPage)
                    pageNumber = 1;
                if (pageNumber < 1)
                    pageNumber = PagingInfo.TotalPage;

                PagingInfo.CurrentPage = pageNumber;

                resulf = ChuyenDis.Skip((pageNumber - 1) * PagingInfo.NumberOfTripInPerPage).Take(PagingInfo.NumberOfTripInPerPage).ToList();
            }else
            {
                PagingInfo.CurrentPage = 0;
            }
            return resulf;
        }
    }
}
