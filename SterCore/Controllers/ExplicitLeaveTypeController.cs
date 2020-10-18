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
using Microsoft.AspNetCore.Mvc;

namespace leave_management.Controllers
{
    [Roles(RoleEnum.Administrator, RoleEnum.Employer, RoleEnum.Agent, RoleEnum.Manager)]
    public class ExplicitLeaveTypeController : Controller
    {
        private readonly IExplicitLeaveTypeRepository _repo;
        private readonly IMapper _mapper;

        public ExplicitLeaveTypeController(IExplicitLeaveTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: LeaveTypes
        public async Task<ActionResult> Index()
        {
            var leavetypes = await _repo.FindAll();
            var model = _mapper.Map<List<ExplicitLeaveTypes>, List<ExplicitLeaveTypesVM>>(leavetypes.ToList());
            return View(model);
        }

        // GET: LeaveTypes/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var exists = await _repo.Exists(id);
            if (!exists)
            {
                return NotFound();
            }
            var leavetype = await _repo.FindById(id);
            var model = _mapper.Map<ExplicitLeaveTypesVM>(leavetype);
            return View(model);
        }

        // GET: LeaveTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ExplicitLeaveTypesVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var leaveType = _mapper.Map<ExplicitLeaveTypes>(model);
                leaveType.DateCreated = DateTime.Now;

                var isSuccess = await _repo.Create(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something Went Wrong...");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something Went Wrong...");
                return View(model);
            }
        }

        // GET: LeaveTypes/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var success = await _repo.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var leavetype = await _repo.FindById(id);
            var model = _mapper.Map<ExplicitLeaveTypesVM>(leavetype);
            return View(model);
        }

        // POST: LeaveTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ExplicitLeaveTypesVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var leaveType = _mapper.Map<ExplicitLeaveTypes>(model);
                var isSuccess = await _repo.Update(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Coś poszło nie tak skontaktuj się z administratorem...");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Coś poszło nie tak skontaktuj się z administratorem...");
                return View(model);
            }
        }

        // GET: LeaveTypes/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var leavetype = await _repo.FindById(id);
            if (leavetype == null)
            {
                return NotFound();
            }
            var isSuccess = await _repo.Delete(leavetype);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ExplicitLeaveTypesVM model)
        {
            try
            {
                var leaveType = await _repo.FindById(id);
                if (leaveType == null)
                {
                    return NotFound();
                }
                var isSuccess = await _repo.Delete(leaveType);
                if (!isSuccess)
                {
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}