﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using leave_management.Repository;
using leave_management.Services.Components.ORI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leave_management.Controllers
{
    [Authorize(Roles = "Administrator, Agent")]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationRepository _organizationRepostory;
        private readonly IDepartmentRepository _departmentRepostory;
        private readonly IOrganizationResourceManager _organizationManager;
        public IAuthorizedDepartmentRepository _authorizedDepartmentRepository { get; }
        private readonly IMapper _mapper;

        public OrganizationController(IMapper mapper, 
            IOrganizationRepository organizationRepostory,
            IDepartmentRepository departmentRepostory,
            IOrganizationResourceManager organizationManager, 
            IAuthorizedDepartmentRepository authorizedDepartmentRepository)
        {
            _organizationRepostory = organizationRepostory;
            _mapper = mapper;
            _departmentRepostory = departmentRepostory;
            _organizationManager = organizationManager;
            _authorizedDepartmentRepository = authorizedDepartmentRepository;
        }

        // GET: Organization
        public async Task<ActionResult> Index()
        {   
            var organizations = await _organizationRepostory.FindAll();
            var model = _mapper.Map<List<Organization>, List<Models.OrganizationVM>>(organizations.ToList());

            return View(model);
        }

        // GET: Organization/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _organizationRepostory.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var organization = await _organizationRepostory.FindById(id);
            var model = _mapper.Map<OrganizationVM>(organization);
            return View(model);
        }

        // GET: Organization/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Organization/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrganizationVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                model.Disabled = false;
                model.InitialOrganization = true;

                var record = _mapper.Map<Organization>(model);
                var isSuccess = await _organizationRepostory.Create(record);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Coś poszło nie tak. Skontaktuj się z Administratorem...");
                    return View(model);
                }

                var departmentToken = _organizationManager.GenerateToken();

                var authorizedDepartment = new AuthorizedDepartment()
                {
                    AuthorizedDepartmentToken = departmentToken
                };

                isSuccess = await _authorizedDepartmentRepository.Create(authorizedDepartment);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Coś poszło nie tak. Skontaktuj się z Administratorem...");
                    return View(model);
                }

                var department = new Department
                {
                    Name = "Administracja",
                    Code = "ADM",
                    DateCreated = DateTime.Now,
                    OrganizationToken = record.OrganizationToken,
                    InitialDepartment = true,
                    OrganizationId = record.Id,
                    DepartmentToken = departmentToken

                };

                var successDep =
                    await _departmentRepostory.Create(department, departmentToken);

                if (!successDep)
                {
                    ModelState.AddModelError("", "Coś poszło nie tak. Skontaktuj się z Administratorem...");
                    return View(model);
                }



                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "coś poszło nie tak. skontaktuj się z administratorem...");
                return View(model);
            }
        }

// GET: Organization/Edit/5
public async Task<ActionResult> Edit(int id)
        {
            var organization = await _organizationRepostory.FindById(id);
            var model = _mapper.Map<OrganizationVM>(organization);
            return View(model);
        }

        // POST: Organization/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OrganizationVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var record = _mapper.Map<Organization>(model);
                var isSuccess = await _organizationRepostory.Update(record);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Błąd podczas zapisu, skontaktuj się z administratorem");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Błąd podczas zapisu, skontaktuj się z administratorem");
                return View(model);
            }
        }

        public async Task<ActionResult> Disable(int id)
        {
            try
            {
                var success = await _organizationRepostory.Exists(id);
                if (!success)
                {
                    return NotFound();
                }

                var organization = await _organizationRepostory.FindById(id);

                organization.Disabled = true;

                if (organization.InialOrganization)
                {
                    ModelState.AddModelError("", "Nie można wyłączyć pierwotnej organizacji.");
                    return RedirectToAction(nameof(Index));
                }


                success = await  _organizationRepostory.Update(organization);

                if (!success)
                {
                    ModelState.AddModelError("", "Niepoprawna operacja wyłączenia organizacji.");
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Index));

            }

            catch 
            {
                ModelState.AddModelError("", "Something went wrong.");
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<ActionResult> Enable(int id)
        {
            try
            {
                var success = await _organizationRepostory.Exists(id);
                if (!success)
                {
                    return NotFound();
                }

                var organization = await _organizationRepostory.FindById(id);


                organization.Disabled = false;

                success = await _organizationRepostory.Update(organization);
                if (!success)
                {
                    ModelState.AddModelError("", "Niepoprawna operacja włączenia organizacji.");
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Index));

            }

            catch 
            {
                ModelState.AddModelError("", "Something went wrong.");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}