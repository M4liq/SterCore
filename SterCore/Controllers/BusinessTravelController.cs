﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leave_management.Controllers
{
    public class BusinessTravelController : Controller
    {
        private readonly IBusinessTravelRepository _repo;
        private readonly IMapper _mapper;

        public BusinessTravelController(IBusinessTravelRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        // GET: PWS
        public async Task<ActionResult> Index()
        {
            var businnesTravels = await _repo.FindAll();
            var model = _mapper.Map<List<BusinessTravel>, List<BusinessTravelVM>>(businnesTravels.ToList());
            return View(model);
        }

        // GET: PWS/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PWS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PWS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PWS/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PWS/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PWS/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PWS/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}