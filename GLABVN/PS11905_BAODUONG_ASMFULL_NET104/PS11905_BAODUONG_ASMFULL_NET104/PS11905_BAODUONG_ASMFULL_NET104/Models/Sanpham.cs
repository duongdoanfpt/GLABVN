using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace PS11905_BAODUONG_ASMFULL_NET104.Models
{
    public partial class Sanpham
    {
        public int MaSp { get; set; }
        public string TenSp { get; set; }
        public decimal DonGia { get; set; }
        public string MoTaSp { get; set; }
        public string HinhAnh { get; set; }

        [DisplayName("Tải hình lên")]
        [NotMapped]
        public IFormFile ProfileImage { get; set; }
        public int NhomSp { get; set; }

        public virtual NhomSp NhomSpNavigation { get; set; }
    }
}
