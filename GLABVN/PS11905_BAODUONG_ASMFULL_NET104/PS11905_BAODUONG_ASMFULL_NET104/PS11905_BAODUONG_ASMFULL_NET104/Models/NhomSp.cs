using System;
using System.Collections.Generic;

#nullable disable

namespace PS11905_BAODUONG_ASMFULL_NET104.Models
{
    public partial class NhomSp
    {
        public NhomSp()
        {
            Sanphams = new HashSet<Sanpham>();
        }

        public int MaNhom { get; set; }
        public string TenNhom { get; set; }

        public virtual ICollection<Sanpham> Sanphams { get; set; }
    }
}
