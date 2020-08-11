using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leave_management.Controllers
{
    public class BillingBusinessTravelController : Controller
    {
        private readonly IBillingBusinessTravelRepository _repo;
        private readonly IMapper _mapper;

        public BillingBusinessTravelController(IBillingBusinessTravelRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        // GET: BillingBusinessTravel
        public ActionResult Index()
        {
            return View();
        }

        // GET: BillingBusinessTravel/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BillingBusinessTravel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BillingBusinessTravel/Create
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

        // GET: BillingBusinessTravel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BillingBusinessTravel/Edit/5
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

        // GET: BillingBusinessTravel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BillingBusinessTravel/Delete/5
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