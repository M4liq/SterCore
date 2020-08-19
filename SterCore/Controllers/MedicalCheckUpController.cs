﻿using System;
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

namespace leave_management.Controllers
{
    public class MedicalCheckUpController : Controller
    {
        private readonly IMedicalCheckUpRepository _medicalCheckUpRepository;
        private readonly ITypeOfMedicalCheckUpRepository _typeOfMedicalCheckUpRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;


        public MedicalCheckUpController(IMedicalCheckUpRepository medicalCheckUpRepository,
            ITypeOfMedicalCheckUpRepository typeOfMedicalCheckUpRepo,
            IEmployeeRepository employeeRepo,
            IMapper mapper
            )
        {
            _medicalCheckUpRepository = medicalCheckUpRepository;
            _typeOfMedicalCheckUpRepo = typeOfMedicalCheckUpRepo;
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }
        // GET: MedicalCheckUp
        public async Task<ActionResult> Index()
        {
            var medicalCheckUps = await _medicalCheckUpRepository.FindAll();
            var model = _mapper.Map<List<MedicalCheckUp>, List<MedicalCheckUpVM>>(medicalCheckUps.ToList());
            return View(model);
        }

        // GET: MedicalCheckUp/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View();
        }

        // GET: MedicalCheckUp/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: MedicalCheckUp/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
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
        public async Task<ActionResult> Edit(int id)
        {
            return View();
        }

        // POST: MedicalCheckUp/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
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
        public async Task<ActionResult> Delete(int id)
        {
            return View();
        }

        // POST: MedicalCheckUp/Delete/5
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