using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
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
        public async Task<ActionResult> Index()
        {
            var billingBusinessTravels = await _repo.FindAll();
            var model = _mapper.Map<List<BillingBusinessTravel>, List<BillingBusinessTravelVM>>(billingBusinessTravels.ToList());
            return View(model);
        }

        // GET: BillingBusinessTravel/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _repo.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var BillingBusinessTravel = await _repo.FindById(id);
            var model = _mapper.Map<BillingBusinessTravelVM>(BillingBusinessTravel);
            return View(model);
        }

        // GET: BillingBusinessTravel/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: BillingBusinessTravel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BillingBusinessTravelVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var billingBusinessTravel = _mapper.Map<BillingBusinessTravel>(model);
                var isSuccess = await _repo.Create(billingBusinessTravel);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong");
                return View();
            }
        }

        // GET: BillingBusinessTravel/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var success = await _repo.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var BillingBusinessTravel = await _repo.FindById(id);
            var model = _mapper.Map<BillingBusinessTravelVM>(BillingBusinessTravel);
            return View(model);
        }

        // POST: BillingBusinessTravel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BillingBusinessTravelVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var BillingBusinessTravel = _mapper.Map<BillingBusinessTravel>(model);
                var isSuccess = await _repo.Update(BillingBusinessTravel);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong");
                return View();
            }
        }

        // GET: BillingBusinessTravel/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var businessTravel = await _repo.FindById(id);
            if (businessTravel == null)
            {
                return NotFound();
            }
            var isSuccess = await _repo.Delete(businessTravel);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: BillingBusinessTravel/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
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