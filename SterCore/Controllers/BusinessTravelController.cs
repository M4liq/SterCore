using System;
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
        public async Task<ActionResult> Details(int id)
        {
            var success = await _repo.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var businessTravel = await _repo.FindById(id);
            var model = _mapper.Map<BusinessTravelVM>(businessTravel);
            return View(model);
        }

        // GET: PWS/Create
        public ActionResult Create()
        {
            //to do wyciagniecie employee i wrzucic model do view
            return View();
        }

        // POST: PWS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BusinessTravelVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
                if(model.DateFrom.Date > model.DateTo.Date)
                {
                    ModelState.AddModelError("", "Podane daty są nieprawidłowe");
                    return View(model);
                }

                var businessTravel = _mapper.Map<BusinessTravel>(model);
                var isSuccess = await _repo.Create(businessTravel);
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

        // GET: PWS/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var success = await _repo.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var businessTravel = await _repo.FindById(id);
            var model = _mapper.Map<BusinessTravelVM>(businessTravel);
            return View(model);
        }

        // POST: PWS/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BusinessTravelVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var businessTravel = _mapper.Map<BusinessTravel>(model);
                var isSuccess = await _repo.Update(businessTravel);
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

        // GET: PWS/Delete/5
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