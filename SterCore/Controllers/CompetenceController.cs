﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace leave_management.Controllers
{
    [Authorize]
    public class CompetenceController : Controller
    {
        private readonly ICompetenceRepository _competenceRepository;
        private readonly ICompetenceTypeRepository _competenceTypeRepository;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;
        public CompetenceController(ICompetenceRepository competenceRepository, ICompetenceTypeRepository competenceTypeRepository, IEmployeeRepository employeeRepo, UserManager<Employee> userManager, IMapper mapper)
        {
            _competenceRepository = competenceRepository;
            _competenceTypeRepository = competenceTypeRepository;
            _employeeRepo = employeeRepo;
            _userManager = userManager;
            _mapper = mapper;
        }
        // GET: Competence
        public async Task<ActionResult> Index()
        {
            var competences = await _competenceRepository.FindAll();
            var competenceTypes = await _competenceTypeRepository.FindAll();
            var mappedModel = _mapper.Map<List<Competence>, List<CompetenceVM>>(competences.ToList());
            var model = new List<CompetenceVM>();
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            foreach (var item in mappedModel)
            {
                model.Add(new CompetenceVM
                {
                   Id = item.Id,
                   CompetenceTypeId = item.CompetenceTypeId,
                   DateValidUntil = item.DateValidUntil,
                   OrganizationToken = item.OrganizationToken,
                   EmployeeFullName = employees.FirstOrDefault(q => q.Id == item.EmployeeId).Firstname.ToString() + " " + employees.FirstOrDefault(q => q.Id == item.EmployeeId).Lastname.ToString(),
                   CompetenceName = competenceTypes.FirstOrDefault(q=>q.Id==item.CompetenceTypeId).name
                   
                });
            }
            return View(model);
        }

        // GET: Competence/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _competenceRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var competence = await _competenceRepository.FindById(id);
            var competenceTypes = await _competenceTypeRepository.FindAll();
            var model = _mapper.Map<CompetenceVM>(competence);
            var employee = await _employeeRepo.FindById(model.EmployeeId);
            model.EmployeeFullName = String.Format("{0} {1}", employee.Lastname, employee.Firstname);
            model.CompetenceName = competenceTypes.FirstOrDefault(q => q.Id == competence.CompetenceTypeId).name;
            return View(model);
        }
        [Authorize(Roles = "Administrator, Employer, Agent")]
        // GET: Competence/Create
        public async Task<ActionResult> Create()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = q.Firstname + " " + q.Lastname,
                Value = q.Id.ToString()
            });
            var competenceTypes = await _competenceTypeRepository.FindAll();
            var competenceTypesItems = competenceTypes.Select(q => new SelectListItem
            {
                Text = q.name,
                Value = q.Id.ToString()
            });

            var model = new CreateCompetenceVM
            {
                Employees = employeesItems,
                CompetenceTypes = competenceTypesItems
            };
            return View(model);
        }

        // POST: Competence/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCompetenceVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var competence = _mapper.Map<Competence>(model);
                var isSuccess = await _competenceRepository.Create(competence);
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

        // GET: Competence/Edit/5
        public async Task<ActionResult> Edit(int id, string organizationToken, string employeeId)
        {
            var success = await _competenceRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var competence = await _competenceRepository.FindById(id);
            var model = _mapper.Map<CreateCompetenceVM>(competence);
            model.EmployeeId = competence.EmployeeId;
            model.CompetenceTypeId = competence.CompetenceTypeId;
            model.OrganizationToken = organizationToken;
            return View(model);
        }

        // POST: Competence/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CreateCompetenceVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var competence = _mapper.Map<Competence>(model);
                var isSuccess = await _competenceRepository.Update(competence);
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

        // GET: Competence/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var competence = await _competenceRepository.FindById(id);
            if (competence == null)
            {
                return NotFound();
            }
            var isSuccess = await _competenceRepository.Delete(competence);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Competence/Delete/5
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