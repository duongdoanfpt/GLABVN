using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PS11905_BAODUONG_ASMFULL_NET104.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PS11905_BAODUONG_ASMFULL_NET104.Extensions;
using PS11905_BAODUONG_ASMFULL_NET104.ViewModels;

namespace PS11905_BAODUONG_ASMFULL_NET104.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly webglab_dbsContext _context;

        public HomeController(webglab_dbsContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var webglab_dbsContext = _context.Sanphams.Include(s => s.NhomSpNavigation);
            //Nike
            List<Sanpham> Nike = webglab_dbsContext.Where(x => x.NhomSp.Equals(1001)).ToList();
            //Adidas
            List<Sanpham> Adidas = webglab_dbsContext.Where(x => x.NhomSp.Equals(1002)).ToList();

            List<Sanpham> Quanao = webglab_dbsContext.Where(x => x.NhomSp.Equals(1003)).ToList();

            List<Sanpham> Phukien = webglab_dbsContext.Where(x => x.NhomSp.Equals(1004)).ToList();

            var session = SessionsHelper.GetObjFromJson<List<CartItem>>(HttpContext.Session, "cart");

            if (session != null)
            {
                List<ProductViewModel> sanphams = new List<ProductViewModel>();
                foreach (var item in session)
                {
                    var findItem = webglab_dbsContext.Single(x => x.MaSp.Equals(item.ProductID));
                    sanphams.Add(new ProductViewModel(item.ProductID, item.Quantity, findItem.TenSp, findItem.DonGia, findItem.HinhAnh));
                }
                ViewData["SessionCart"] = sanphams;
            }
            else ViewData["SessionCart"] = null;

            ViewData["Nike"] = Nike;
            ViewData["Adidas"] = Adidas;
            ViewData["Quanao"] = Quanao;
            ViewData["Phukien"] = Phukien;

            return View(await webglab_dbsContext.ToListAsync());
        }
    }
}
