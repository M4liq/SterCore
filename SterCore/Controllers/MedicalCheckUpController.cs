﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leave_management.Controllers
{
    public class MedicalCheckUpController : Controller
    {
        // GET: MedicalCheckUp
        public ActionResult Index()
        {
            return View();
        }

        // GET: MedicalCheckUp/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MedicalCheckUp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicalCheckUp/Create
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

        // GET: MedicalCheckUp/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MedicalCheckUp/Edit/5
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

        // GET: MedicalCheckUp/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MedicalCheckUp/Delete/5
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