using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Helpers.Attributes;
using leave_management.Helpers.Enums;
using leave_management.Models;
using leave_management.Services.LeaveHelper.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace leave_management.Controllers
{
    [Roles(RoleEnum.Administrator,RoleEnum.Agent,RoleEnum.Employer,RoleEnum.Manager)]
    public class LeaveAllocationController : Controller
    {
        private readonly ICommonLeaveTypeRepository _commonLeaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILeaveHelper _leaveHelper;
        private readonly IExplicitLeaveTypeRepository _explicitLeaveTypeRepository;

        public LeaveAllocationController(
            ICommonLeaveTypeRepository leaveTypeRepository,
            ILeaveAllocationRepository leaveAllocationRepository,
            IMapper mapper, 
            IEmployeeRepository employeeRepository, 
            ILeaveHelper leaveHelper, 
            IExplicitLeaveTypeRepository explicitLeaveTypeRepository)
        {
            _commonLeaveTypeRepository = leaveTypeRepository;
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _leaveHelper = leaveHelper;
            _explicitLeaveTypeRepository = explicitLeaveTypeRepository;
        }

        // GET: LeaveAllocation
        public async Task<ActionResult> Index()
        {
            var leavetypes = await _commonLeaveTypeRepository.FindAll();

            var mappedLeaveTypes = _mapper.Map<List<CommonLeaveTypes>, List<LeaveTypeVM>>(leavetypes.ToList());
            var model = new CreateLeaveAllocationVM
            {
                LeaveTypes = mappedLeaveTypes,
                NumberUpdated = 0
            };
            return View(model);
        }


        public async Task<ActionResult> SetCommonLeave(int id)
        {
            var leaveType = await _commonLeaveTypeRepository.FindById(id);
            var employees = await _employeeRepository.FindAll();
            foreach (var emp in employees)
            {
                var success = await _leaveAllocationRepository.CheckAllocation(id, emp.Id);
                if (success)
                    continue;
                var allocation = new CommonLeaveAllocationVM
                {
                    DateCreated = DateTime.Now,
                    EmployeeId = emp.Id,
                    CommonLeaveTypeId  = id,
                    NumberOfDays = _leaveHelper.DivideByCycle(leaveType),
                    Period = DateTime.Now.Year
                };
                var leaveAllocation = _mapper.Map<LeaveAllocations>(allocation);
                await _leaveAllocationRepository.Create(leaveAllocation);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> SetExplicitLeave(int id)
        {
            var leaveType = await _explicitLeaveTypeRepository.FindById(id);
            var employees = await _employeeRepository.FindAll();
            foreach (var emp in employees)
            {
                var success = await _leaveAllocationRepository.CheckAllocation(id, emp.Id);
                if (success)
                    continue;
                var allocation = new ExplicitLeaveAllocationVM()
                {
                    DateCreated = DateTime.Now,
                    EmployeeId = emp.Id,
                    ExplicitLeaveTypeId = id,
                    NumberOfDays = _leaveHelper.DivideByCycle(leaveType),
                    Period = DateTime.Now.Year
                };
                var leaveAllocation = _mapper.Map<LeaveAllocations>(allocation);
                await _leaveAllocationRepository.Create(leaveAllocation);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: LeaveAllocation/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var employee = _mapper.Map<EmployeeVM>(await _employeeRepository.FindById(id));
            var allocations = _mapper.Map<List<CommonLeaveAllocationVM>>(await _leaveAllocationRepository.GetLeaveAllocationsByEmployee(id));
            var model = new ViewAllocationsVM
            {
                Employee = employee,
                LeaveAllocations = allocations
            };
            return View(model);
        }

        // GET: LeaveAllocation/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var leaveallocation = await _leaveAllocationRepository.FindById(id);
            var model = _mapper.Map<EditLeaveAllocationVM>(leaveallocation);
            return View(model);
        }

        // POST: LeaveAllocation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditLeaveAllocationVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var record = await _leaveAllocationRepository.FindById(model.Id);
                record.NumberOfDays = model.NumberOfDays;
                var isSuccess = await _leaveAllocationRepository.Update(record);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Error while saving");
                    return View(model);
                }
                return RedirectToAction(nameof(Details), new {id = model.EmployeeId });
            }
            catch 
            {
                return View(model);
            }
        }
    }
}