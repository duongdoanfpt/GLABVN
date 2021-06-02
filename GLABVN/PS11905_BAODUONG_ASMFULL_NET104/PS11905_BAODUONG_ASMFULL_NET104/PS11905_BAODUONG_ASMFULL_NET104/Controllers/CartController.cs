using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PS11905_BAODUONG_ASMFULL_NET104.Extensions;
using PS11905_BAODUONG_ASMFULL_NET104.Models;
using PS11905_BAODUONG_ASMFULL_NET104.ViewModels;

namespace PS11905_BAODUONG_ASMFULL_NET104.Controllers
{
    public class CartController : Controller
    {
        private readonly webglab_dbsContext _context;
        private static List<ProductViewModel> products;

        public CartController(webglab_dbsContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var getAll = _context.Sanphams.ToList();
            var cartItem = SessionsHelper.GetObjFromJson<List<CartItem>>(HttpContext.Session, "cart");
            products = new List<ProductViewModel>();
            if (cartItem != null)
            {
                foreach (var item in cartItem)
                {
                    var getProducts = getAll.Single(x => x.MaSp.Equals(item.ProductID));
                    products.Add(new ProductViewModel(item.ProductID, item.Quantity, getProducts.TenSp, getProducts.DonGia, getProducts.HinhAnh));
                }
            }
            return View(products);
        }

        public IActionResult Remove(int id)
        {
            List<CartItem> cartItems = SessionsHelper.GetObjFromJson<List<CartItem>>(HttpContext.Session, "cart");

            var findItem = cartItems.SingleOrDefault(x => x.ProductID.Equals(id));
            if (findItem != null)
            {
                cartItems.Remove(findItem);
                SessionsHelper.SetObjAsJson(HttpContext.Session, "cart", cartItems);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Checkout()
        {
            List<CartItem> cartItems = SessionsHelper.GetObjFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cartItems != null)
            {
                List<ChiTietHD> chiTietHDs = new List<ChiTietHD>();
                foreach (var item in products)
                {
                    chiTietHDs.Add(new ChiTietHD()
                    {
                        Count = item.Quantity,
                        MaSP = item.ProductID,
                        Price = Convert.ToDouble(item.Price),
                        Total = Convert.ToDouble(item.Total)
                    });
                }

                HoaDon hd = new HoaDon() { TotalPrice = chiTietHDs.Sum(x => x.Total) };
                _context.HoaDons.Add(hd);
                _context.SaveChanges();
                var idHD = _context.HoaDons.ToList().Last().ID;
                foreach (var item in chiTietHDs)
                {
                    item.IDHoaDon = idHD;
                    _context.ChiTietHDs.Add(item);
                }
                _context.SaveChanges();


                SessionsHelper.SetObjAsJson(HttpContext.Session, "cart", null);
                return View();
            }
            return View();
        }

        public IActionResult AddCart(int id)
        {
            List<CartItem> cartItem = SessionsHelper.GetObjFromJson<List<CartItem>>(HttpContext.Session, "cart");
            //Empty cart
            if (cartItem == null)
            {
                cartItem = new List<CartItem>();
                //Addsession
                cartItem.Add(new CartItem() { ProductID = id, Quantity = 1 });
                SessionsHelper.SetObjAsJson(HttpContext.Session, "cart", cartItem);
            }
            else
            {
                bool isAdded = false;
                //Get cart
                for (int i = 0; i < cartItem.Count(); i++)
                {
                    if (cartItem[i].ProductID.Equals(id))
                    {
                        cartItem[i].Quantity++;
                        isAdded = true;
                    }
                }

                if (!isAdded)
                {
                    cartItem.Add(new CartItem() { ProductID = id, Quantity = 1 });
                }

                SessionsHelper.SetObjAsJson(HttpContext.Session, "cart", cartItem);
            }

            return RedirectToAction("Index", "Home");
        }
    }

    public class CartItem
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
