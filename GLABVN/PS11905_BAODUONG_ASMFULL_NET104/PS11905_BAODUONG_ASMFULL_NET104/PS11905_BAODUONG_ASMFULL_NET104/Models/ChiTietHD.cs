using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PS11905_BAODUONG_ASMFULL_NET104.Models
{
    [Table("ChiTietHD")]
    public class ChiTietHD
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int ID { get; set; }

        public int Count { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }

        [ForeignKey("MaSP")]
        public int MaSP { get; set; }

        [ForeignKey("IDHoaDon")]
        public int IDHoaDon { get; set; }

    }
}
