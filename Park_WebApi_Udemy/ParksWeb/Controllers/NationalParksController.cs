﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParksWeb.Models;
using ParksWeb.Repository.IRepository;

namespace ParksWeb.Controllers
{
    [Authorize]
    public class NationalParksController : Controller
    {

        private readonly INationalParkRepository _npRepo;

        public NationalParksController(INationalParkRepository npRepo)
        {
            _npRepo = npRepo;
        }

        public IActionResult Index()
        {
            return View(new NationalPark() { });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            NationalPark obj = new NationalPark();

            if (id == null)
            {
                //this will be true for Insert/Create
                return View(obj);
            }

            //Flow will come here for update
            obj = await _npRepo.GetAsync(StaticDetails.NationalParkAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
            //obj = await _npRepo.GetAsync(StaticDetails.NationalParkAPIPath, id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(NationalPark obj)
        {
            if (ModelState.IsValid)
            {
                //for image file
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)//true then, image is uploaded
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    obj.Picture = p1;
                }
                else//if the image is not uploaded then retrieve the object from database and fetch the image
                {
                    var objFromDb = await _npRepo.GetAsync(StaticDetails.NationalParkAPIPath, obj.Id, HttpContext.Session.GetString("JWToken"));
                    //var objFromDb = await _npRepo.GetAsync(StaticDetails.NationalParkAPIPath, obj.Id);
                    obj.Picture = objFromDb.Picture;
                }
                if (obj.Id == 0)
                {
                    await _npRepo.CreateAsync(StaticDetails.NationalParkAPIPath, obj, HttpContext.Session.GetString("JWToken"));
                    //await _npRepo.CreateAsync(StaticDetails.NationalParkAPIPath, obj);
                }
                else//here we are passing id as in Park_WebApi_Udemy it compares the id with the object id
                {
                    await _npRepo.UpdateAsync(StaticDetails.NationalParkAPIPath + obj.Id, obj, HttpContext.Session.GetString("JWToken"));
                    //await _npRepo.UpdateAsync(StaticDetails.NationalParkAPIPath + obj.Id, obj); 
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }

        public async Task<IActionResult> GetAllNationalPark()
        {
            return Json(new { data = await _npRepo.GetAllAsync(StaticDetails.NationalParkAPIPath, HttpContext.Session.GetString("JWToken")) });
            //return Json(new { data = await _npRepo.GetAllAsync(StaticDetails.NationalParkAPIPath) });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        //This method will be called from the nationalPark.js onclick delete.
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _npRepo.DeleteAsync(StaticDetails.NationalParkAPIPath, id, HttpContext.Session.GetString("JWToken"));
            //var status = await _npRepo.DeleteAsync(StaticDetails.NationalParkAPIPath, id);
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            return Json(new { success = false, message = "Delete Not Successful" });
        }
    }
}