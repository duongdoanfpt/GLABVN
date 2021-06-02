using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PS11905_BAODUONG_ASMFULL_NET104.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace PS11905_BAODUONG_ASMFULL_NET104.Controllers
{
    public class ThemSPController : Controller
    {
        private readonly webglab_dbsContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        //inject
        public ThemSPController(webglab_dbsContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: ThemSP
        public async Task<IActionResult> Index()
        {
            var webglab_dbsContext = _context.Sanphams.Include(s => s.NhomSpNavigation);
            return View(await webglab_dbsContext.ToListAsync());
        }

        // GET: ThemSP/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanphams
                .Include(s => s.NhomSpNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }

        // GET: ThemSP/Create
        public IActionResult Create()
        {
            ViewData["NhomSp"] = new SelectList(_context.NhomSps, "MaNhom", "TenNhom");
            return View();
            //if(ModelState.IsValid){
            //    string rootpath = 
            //}
        }

        // POST: ThemSP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                if (sanpham.ProfileImage.Length > 0)
                {
                    UploadImage(sanpham.ProfileImage);
                }

                Sanpham sp = new Sanpham()
                {
                    TenSp = sanpham.TenSp,
                    MoTaSp = sanpham.MoTaSp,
                    DonGia = sanpham.DonGia,
                    NhomSp = sanpham.NhomSp,
                    NhomSpNavigation = sanpham.NhomSpNavigation,
                    HinhAnh = sanpham.ProfileImage.FileName,
                };
                _context.Add(sp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NhomSp"] = new SelectList(_context.NhomSps, "MaNhom", "TenNhom", sanpham.NhomSp);
            return View(sanpham);
        }

        private void UploadImage(IFormFile file)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "images", file.FileName);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }

        private void RemoveImage(string path)
        {
            //string path = Path.Combine(_hostingEnvironment.WebRootPath, "images", file.FileName);
            var getFile = new FileInfo(path);
            getFile.Delete();
        }

        // GET: ThemSP/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanphams.FindAsync(id);
            if (sanpham == null)
            {
                return NotFound();
            }
            ViewData["NhomSp"] = new SelectList(_context.NhomSps, "MaNhom", "TenNhom", sanpham.NhomSp);
            return View(sanpham);
        }

        // POST: ThemSP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sanpham sanpham)
        {
            if (id != sanpham.MaSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                sanpham.HinhAnh = sanpham.ProfileImage.FileName;
                var findSP = _context.Sanphams.SingleOrDefault(x => x.MaSp.Equals(id));
                string prevImage = Path.Combine(_hostingEnvironment.WebRootPath, "images", findSP.HinhAnh);
                try
                {
                    RemoveImage(prevImage);
                    UploadImage(sanpham.ProfileImage);
                    findSP.MaSp = id;
                    findSP.TenSp = sanpham.TenSp;
                    findSP.DonGia = sanpham.DonGia;
                    findSP.HinhAnh = sanpham.ProfileImage.FileName;
                    findSP.MoTaSp = sanpham.MoTaSp;
                    findSP.NhomSp = sanpham.NhomSp;
                    findSP.NhomSpNavigation = sanpham.NhomSpNavigation;

                    //_context.Update(sp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanphamExists(sanpham.MaSp))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["NhomSp"] = new SelectList(_context.NhomSps, "MaNhom", "TenNhom", sanpham.NhomSp);
            return View(sanpham);
        }

        // GET: ThemSP/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanphams
                .Include(s => s.NhomSpNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }

        // POST: ThemSP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanpham = await _context.Sanphams.FindAsync(id);
            _context.Sanphams.Remove(sanpham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanphamExists(int id)
        {
            return _context.Sanphams.Any(e => e.MaSp == id);
        }
    }
}
