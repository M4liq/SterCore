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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace leave_management.Controllers
{
    public class BusinessTravelController : Controller
    {
        private readonly IBusinessTravelRepository _repo;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ITransportVehicleRepository _transportVehicleRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public BusinessTravelController(IBusinessTravelRepository repo, UserManager<Employee> userManager, ICountryRepository countryRepository, IEmployeeRepository employeeRepository, ITransportVehicleRepository transportVehicleRepository, IMapper mapper)
        {
            _repo = repo;
            _employeeRepository = employeeRepository;
            _countryRepository = countryRepository;
            _transportVehicleRepository = transportVehicleRepository;
            _userManager = userManager;
            _mapper = mapper;
        }
        // GET: PWS
        public async Task<ActionResult> Index()
        {
            var businnesTravels = await _repo.FindAll();
            var countries = _countryRepository.FindAll().Result;
            var employees = _employeeRepository.FindAll().Result;
            var model = _mapper.Map<List<BusinessTravel>, List<BusinessTravelVM>>(businnesTravels.ToList());
            foreach (var item in model)
            {
                item.EmployeeFullName = String.Format("{0} {1}", employees.FirstOrDefault(q => q.Id == item.EmployeeId).Firstname, employees.FirstOrDefault(q => q.Id == item.EmployeeId).Lastname);
                item.DestinationCountry = countries.FirstOrDefault(q => q.Id == item.CountryId).Name;
            }
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
            var countries = _countryRepository.FindAll().Result;
            var employees = _employeeRepository.FindAll().Result;
            var transportVehicles = _transportVehicleRepository.FindAll().Result;
            var model = _mapper.Map<BusinessTravelVM>(businessTravel);
            model.EmployeeFullName = String.Format("{0} {1}", employees.FirstOrDefault(q => q.Id == model.EmployeeId).Firstname, employees.FirstOrDefault(q => q.Id == model.EmployeeId).Lastname);
            model.DestinationCountry = countries.FirstOrDefault(q => q.Id == model.CountryId).Name;
            model.TransportVehicle = transportVehicles.FirstOrDefault(q => q.Id == model.TransportVehicleId).Name;
            return View(model);
        }

        // GET: PWS/Create
        public async Task<ActionResult> Create()
        {
            var employees = _employeeRepository.FindAll().Result;
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                Value = q.Id.ToString()
            });

            var countries = _countryRepository.FindAll().Result;
            var countriesItems = countries.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            var transportVehicles = _transportVehicleRepository.FindAll().Result;
            var transportVehiclesItems = transportVehicles.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Value.ToString()
            });

            var model = new CreateBusinessTravelVM
            {
                Employees = employeesItems,
                TransportVehicles = transportVehiclesItems,
                DestinationCountries = countriesItems
            };


            return View(model);
        }

        // POST: PWS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBusinessTravelVM model)
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
                model.DateCreated = DateTime.Now;
                
                var lastApplicationId = _repo.getLatestApplicationId().Result;
                var applicationId = String.Format("WS{0}", lastApplicationId + 1);
                model.ApplicationId = applicationId;
                var businessTravel = _mapper.Map<BusinessTravel>(model);
                var isSuccess = await _repo.Create(businessTravel);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
                return View(model);
            }
        }

        // GET: PWS/Edit/5
        public async Task<ActionResult> Edit(int id, string employeeId, int countryId, int transportVehicleId, string organizationToken)
        {
            var success = await _repo.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var businessTravel = await _repo.FindById(id);
            var model = _mapper.Map<CreateBusinessTravelVM>(businessTravel);
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