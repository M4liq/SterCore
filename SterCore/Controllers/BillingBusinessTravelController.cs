using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace leave_management.Controllers
{
    public class BillingBusinessTravelController : Controller
    {
        private readonly IBillingBusinessTravelRepository _billingBusinessTravelRepo;
        private readonly IBusinessTravelRepository _businessTravelRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;


        public BillingBusinessTravelController(IBillingBusinessTravelRepository repo, 
            IEmployeeRepository employeeRepo,
            IBusinessTravelRepository businessTravelRepo, 
            UserManager<Employee> userManager, 
            IMapper mapper
            )
        { 
            _billingBusinessTravelRepo = repo;
            _businessTravelRepo = businessTravelRepo;
            _employeeRepo = employeeRepo;
            _userManager = userManager;
            _mapper = mapper;
        }
        // GET: BillingBusinessTravel
        public async Task<ActionResult> Index()
        {
            var billingBusinessTravels = await _billingBusinessTravelRepo.FindAll();
            var model = _mapper.Map<List<BillingBusinessTravel>, List<BillingBusinessTravelVM>>(billingBusinessTravels.ToList());
            return View(model);
        }

        // GET: BillingBusinessTravel/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _billingBusinessTravelRepo.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var BillingBusinessTravel = await _billingBusinessTravelRepo.FindById(id);
            var model = _mapper.Map<BillingBusinessTravelVM>(BillingBusinessTravel);
            return View(model);
        }

        // GET: BillingBusinessTravel/Create
        public async Task<ActionResult> Create()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");

            //var employees = _employeeRepo.FindAll().Result;
            
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = q.Firstname + " " + q.Lastname,
                Value = q.Id.ToString()
            });
            var businessTravels = _businessTravelRepo.FindAll().Result;
            var businessTravelsItems = businessTravels.Select(q => new SelectListItem
            {
                Text = q.ApplicationId.ToString(),
                Value = q.Id.ToString()
            });
            var model = new CreateBillingBusinessTravelVM
            {
                Employees = employeesItems,
                BusinessTravels = businessTravelsItems
            };

            return View(model);
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
                var isSuccess = await _billingBusinessTravelRepo.Create(billingBusinessTravel);
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
            var success = await _billingBusinessTravelRepo.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var BillingBusinessTravel = await _billingBusinessTravelRepo.FindById(id);
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
                var isSuccess = await _billingBusinessTravelRepo.Update(BillingBusinessTravel);
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
            var businessTravel = await _billingBusinessTravelRepo.FindById(id);
            if (businessTravel == null)
            {
                return NotFound();
            }
            var isSuccess = await _billingBusinessTravelRepo.Delete(businessTravel);
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