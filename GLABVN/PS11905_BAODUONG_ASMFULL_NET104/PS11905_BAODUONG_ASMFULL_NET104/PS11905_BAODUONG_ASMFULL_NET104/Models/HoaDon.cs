using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PS11905_BAODUONG_ASMFULL_NET104.Models
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int ID { get; set; }

        public double TotalPrice { get; set; }


        //[ForeignKey("IDChiTiet")]
        //public int IDChiTiet { get; set; }

        //public List<ChiTietHD> ChiTietHDs { get; set; }
    }
}
