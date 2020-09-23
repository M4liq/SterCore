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
    public class ApplicationController : Controller
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public ApplicationController(IApplicationRepository applicationRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        // GET: Application
        public async Task<ActionResult> Index()
        {
            var employees = await _employeeRepository.FindAll();

            var Applications = await _applicationRepository.FindAll();
            var model = _mapper.Map<List<Application>, List<ApplicationVM>>(Applications.ToList());
            foreach (var item in model)
            {
                var employee = employees.FirstOrDefault(q => q.Id == item.EmployeeId);
                var employeeFulName = String.Format("{0} {1}", employee.Lastname, employee.Firstname);
                item.EmployeeFullName = employeeFulName;
            }
            return View(model);
        }

        // GET: Application/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _applicationRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var employees = await _employeeRepository.FindAll();
            var Applications = await _applicationRepository.FindById(id);
            var model = _mapper.Map<ApplicationVM>(Applications);
            var employee = employees.FirstOrDefault(q => q.Id == model.EmployeeId);
            var employeeFulName = String.Format("{0} {1}", employee.Lastname, employee.Firstname);

            model.EmployeeFullName = employeeFulName;
            return View(model);
        }
        // GET: Application/Create
        public async Task<ActionResult> Create()
        {
            var model = new CreateApplicationVM();
            var employees = await _employeeRepository.FindAll();
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                Value = q.Id.ToString()
            });

            model.Employees = employeesItems;
            return View(model);
        }

        // POST: Application/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateApplicationVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var Application = _mapper.Map<Application>(model);
                var isSuccess = await _applicationRepository.Create(Application);
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

        // GET: Application/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var success = await _applicationRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var Application = await _applicationRepository.FindById(id);
            var model = _mapper.Map<CreateApplicationVM>(Application);

            var employees = await _employeeRepository.FindAll();
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                Value = q.Id.ToString()
            });

            model.Employees = employeesItems;

            return View(model);
        }

        // POST: Application/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CreateApplicationVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                
                var Application = _mapper.Map<Application>(model);
                var isSuccess = await _applicationRepository.Update(Application);
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

        // GET: Application/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var Application = await _applicationRepository.FindById(id);
            if (Application == null)
            {
                return NotFound();
            }
            var isSuccess = await _applicationRepository.Delete(Application);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Application/Delete/5
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