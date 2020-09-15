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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace leave_management.Controllers
{
    public class WorkTimeScheduleController : Controller
    {
        private readonly IWorkTimeScheduleRepository _workTimeScheduleRepository;
        private readonly IWorkTimeScheduleEmployeesRepository _workTimeScheduleEmployeesRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public WorkTimeScheduleController(IWorkTimeScheduleRepository workTimeScheduleRepository, 
            IWorkTimeScheduleEmployeesRepository workTimeScheduleEmployeesRepository, 
            IEmployeeRepository employeeRepository, 
            IMapper mapper)
        {
            _workTimeScheduleRepository = workTimeScheduleRepository;
            _workTimeScheduleEmployeesRepository = workTimeScheduleEmployeesRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        // GET: WorkTimeSchedule
        public async Task<ActionResult> Index()
        {
            var workTimeSchedule = await _workTimeScheduleRepository.FindAll();
            var model = _mapper.Map<List<WorkTimeSchedule>, List<WorkTimeScheduleVM>>(workTimeSchedule.ToList());
            return View(model);
        }

        // GET: WorkTimeSchedule/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View();
        }

        // GET: WorkTimeSchedule/Create
        public async Task<ActionResult> Create()
        {
            var model = new CreateWorkTimeScheduleVM();
            var employees = await _employeeRepository.FindAll();
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                Value = q.Id.ToString()
            });

            model.Employees = employeesItems;
            return View(model);
        }

        // POST: WorkTimeSchedule/Create
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

        // GET: WorkTimeSchedule/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View();
        }

        // POST: WorkTimeSchedule/Edit/5
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

        // GET: WorkTimeSchedule/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View();
        }

        // POST: WorkTimeSchedule/Delete/5
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