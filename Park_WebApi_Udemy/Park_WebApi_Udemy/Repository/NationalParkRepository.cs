﻿using Park_WebApi_Udemy.Data;
using Park_WebApi_Udemy.Models;
using Park_WebApi_Udemy.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Park_WebApi_Udemy.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext db;

        public NationalParkRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            db.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            db.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            return db.NationalParks.FirstOrDefault(a => a.Id == nationalParkId);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return db.NationalParks.OrderBy(m => m.Name).ToList();
        }

        public bool NationalParkExists(string name)
        {
            bool value = db.NationalParks.Any(n => n.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool NationalParkExists(int id)
        {
            return db.NationalParks.Any(i => i.Id == id);
        }

        public bool Save()
        {
            return db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            db.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
