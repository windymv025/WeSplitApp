//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeSplitApp.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class KhoanThu
    {
        public int IDChuyenDi { get; set; }
        public int IDNguoiDongTien { get; set; }
        public decimal SoTien { get; set; }
    
        public virtual ChuyenDi ChuyenDi { get; set; }
        public virtual ThanhVien ThanhVien { get; set; }
    }
}