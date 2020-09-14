using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using leave_management.Services.Components.ORI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace leave_management.Controllers
{
    [Authorize(Roles = "Administrator, Agent, Employer")]
    public class DepartmentController : Controller
    {   

        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IOrganizationResourceManager _organizationManager;

        public DepartmentController(
            IDepartmentRepository departmentRepository,
            IMapper mapper, 
            IOrganizationResourceManager organizationManager
            )
        {
            _departmentRepository = departmentRepository;
            _mapper= mapper;
            _organizationManager= organizationManager;
        }
        // GET: Department
        public async Task<ActionResult> Index()
        {
            var departments = await _departmentRepository.FindAll();
            var model = _mapper.Map<List<Department>, List<DepartmentVM>>(departments.ToList());

            return View(model);
        }

        // GET: Department/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _departmentRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }

            var document = await _departmentRepository.FindById(id);
            var model = _mapper.Map<DepartmentVM>(document);
            return View(model);
        }

        // GET: Department/Create
        public async Task<ActionResult> Create()
        {
            var model = new DepartmentVM();
            return View(model);
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DepartmentVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                model.DateCreated = DateTime.Now;
                var organization = await _organizationManager.GetCurrentOrganization();
                model.OrganizationId = organization.Id;
                var department = _mapper.Map<Department>(model);
                if (!await _departmentRepository.Create(department))
                    throw new Exception("Błąd przy zapisywaniu danych. Skontaktuj się z administratorem.");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(" ", "Błąd przy zapisywaniu danych. Skontaktuj się z administratorem.");
                return View(model);
            }
        }

        // GET: Department/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var department = await _departmentRepository.FindById(id);
            if (department == null)
                return NotFound();

            var model = _mapper.Map<DepartmentVM>(department);
            return View(model);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DepartmentVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

            var organization = await _organizationManager.GetCurrentOrganization();
            model.OrganizationId = organization.Id;

            var department = _mapper.Map<Department>(model);
                var success = await _departmentRepository.Update(department);

                if (!success)
                    throw new Exception("Błąd zapisu do bazy danych. Skontaktuj się z Administratorem");

                return RedirectToAction(nameof(Index));
        }
            catch(Exception e)
            {
                ModelState.AddModelError(" ", "Błąd zapisu do bazy danych. Skontaktuj się z Administratorem");
                return View(model);
    }
}

        // GET: Department/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var department = await _departmentRepository.FindById(id);
            if (department == null)
            {
                return NotFound();
            }
             
            if(department.InitialDepartment)
                return RedirectToAction(nameof(Index));

            var isSuccess = await _departmentRepository.Delete(department);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Department/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}