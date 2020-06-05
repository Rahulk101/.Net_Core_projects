using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParksWeb.Models;
using ParksWeb.Models.ViewModel;
using ParksWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace ParksWeb.Controllers
{
    [Authorize]
    public class TrailsController : Controller
    {

        private readonly INationalParkRepository _npRepo;
        private readonly ITrailRepository _trailRepo;

        public TrailsController(INationalParkRepository npRepo, ITrailRepository trailRepo)
        {
            _npRepo = npRepo;
            _trailRepo = trailRepo;
        }

        public IActionResult Index()
        {
            return View(new Trail() { });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<NationalPark> npList = await _npRepo.GetAllAsync(StaticDetails.NationalParkAPIPath, HttpContext.Session.GetString("JWToken"));
            //IEnumerable<NationalPark> npList = await _npRepo.GetAllAsync(StaticDetails.NationalParkAPIPath);

            TrailsVM objVM = new TrailsVM()
            {
                NationalParkList = npList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Trail = new Trail()
            };

            if (id == null)
            {
                //this will be true for Insert/Create
                return View(objVM);
            }

            //Flow will come here for update
            objVM.Trail = await _trailRepo.GetAsync(StaticDetails.TrailAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
            //objVM.Trail = await _trailRepo.GetAsync(StaticDetails.TrailAPIPath, id.GetValueOrDefault());
            if (objVM.Trail == null)
            {
                return NotFound();
            }
            return View(objVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TrailsVM obj)
        {
            if (ModelState.IsValid)
            {

                if (obj.Trail.Id == 0)
                {
                    await _trailRepo.CreateAsync(StaticDetails.TrailAPIPath, obj.Trail, HttpContext.Session.GetString("JWToken"));
                    //await _trailRepo.CreateAsync(StaticDetails.TrailAPIPath, obj.Trail);
                }
                else
                {
                    await _trailRepo.UpdateAsync(StaticDetails.TrailAPIPath + obj.Trail.Id, obj.Trail, HttpContext.Session.GetString("JWToken"));
                    //await _trailRepo.UpdateAsync(StaticDetails.TrailAPIPath + obj.Trail.Id, obj.Trail);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                IEnumerable<NationalPark> npList = await _npRepo.GetAllAsync(StaticDetails.NationalParkAPIPath, HttpContext.Session.GetString("JWToken"));
                //IEnumerable<NationalPark> npList = await _npRepo.GetAllAsync(StaticDetails.NationalParkAPIPath);

                TrailsVM objVM = new TrailsVM()
                {
                    NationalParkList = npList.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                    Trail = obj.Trail
                };
                return View(objVM);
            }
        }

        public async Task<IActionResult> GetAllTrail()
        {
            return Json(new { data = await _trailRepo.GetAllAsync(StaticDetails.TrailAPIPath, HttpContext.Session.GetString("JWToken")) });
            //return Json(new { data = await _trailRepo.GetAllAsync(StaticDetails.TrailAPIPath) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _trailRepo.DeleteAsync(StaticDetails.TrailAPIPath, id, HttpContext.Session.GetString("JWToken"));
            //var status = await _trailRepo.DeleteAsync(StaticDetails.TrailAPIPath, id);
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            return Json(new { success = false, message = "Delete Not Successful" });
        }
    }
}