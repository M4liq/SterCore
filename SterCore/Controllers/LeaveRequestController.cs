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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace leave_management.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {   
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveRequestController(
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository,
            ILeaveAllocationRepository leaveAllocationRepository,
            IMapper mapper,
            UserManager<Employee> userManager
            )
        {
           _leaveRequestRepository = leaveRequestRepository;
           _leaveTypeRepository = leaveTypeRepository;
           _leaveAllocationRepository = leaveAllocationRepository;
           _mapper = mapper;
           _userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        // GET: LeaveRequest
        public async Task<ActionResult> Index()
        {
            var leaveRequests = await _leaveRequestRepository.FindAll();
            var leaveRequestsModel = _mapper.Map<List<LeaveRequestVM>>(leaveRequests);
            var TotalCancelledRequests = leaveRequestsModel.Where(q => q.Cancelled == true).Count();

            var AdminModel = new AdministratorLeaveRequestVM
            {   
                TotalRequests = leaveRequestsModel.Count - TotalCancelledRequests, //doing Count direct against list (has got property in default)
                ApprovedRequests = leaveRequestsModel.Where(q => q.Approved == true && q.Cancelled != true).Count(),
                PendingRequests = leaveRequestsModel.Where(q => q.Approved == null && q.Cancelled != true).Count(),
                RejectedRequests = leaveRequestsModel.Where(q => q.Approved == false && q.Cancelled != true).Count(),
                LeaveRequests = leaveRequestsModel
            };

            return View(AdminModel);
        }

        [Authorize(Roles = "Administrator")]
        // GET: LeaveRequest/Details/5
        public async Task<ActionResult> Details(int id)                             
        {
            var leaveRequest = await _leaveRequestRepository.FindById(id);
            var model = _mapper.Map<LeaveRequestVM>(leaveRequest);
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> ApproveRequest(int id)
        {
            try
            {
                var leaveRequest = await _leaveRequestRepository.FindById(id);
                var admin = await _userManager.GetUserAsync(User);
                var employeeId = leaveRequest.RequestingEmployeeId;
                var leaveTypeId = leaveRequest.LeaveTypeId;
                var allocation = await _leaveAllocationRepository.GetLeaveAllocationsByEmployeeAndType(employeeId, leaveTypeId);
                int daysRequested = (int)(leaveRequest.EndDate.Date - leaveRequest.StartDate.Date).TotalDays;

                allocation.NumberOfDays -= daysRequested;

                leaveRequest.Approved = true;
                leaveRequest.ApprovedById = admin.Id;
                leaveRequest.DateActioned = DateTime.Now;

                var isSuccess = await _leaveRequestRepository.Update(leaveRequest);
                isSuccess = await _leaveAllocationRepository.Update(allocation);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Coś poszło nie tak. Skontaktuj się z administratorem systemu.");
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction(nameof(Index));
            }

            catch 
            {
                return RedirectToAction(nameof(Index));
            }
           
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> RejectRequest(int id)
        {
            try
            {
                var leaveRequest = await _leaveRequestRepository.FindById(id);
                var admin = await _userManager.GetUserAsync(User);

                leaveRequest.Approved = false;
                leaveRequest.ApprovedById = admin.Id;
                leaveRequest.DateActioned = DateTime.Now;

                var isSuccess = await _leaveRequestRepository.Update(leaveRequest);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Coś poszło nie tak. Skontaktuj się z administratorem systemu.");
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction(nameof(Index));
            }

            catch 
            {
                return RedirectToAction("Index", "Home");
            }

        }

        // GET: LeaveRequest/Create
        public async Task<ActionResult> Create()
        {
            var leaveTypes = await _leaveTypeRepository.FindAll();
            var leaveTypeItems = leaveTypes.Select(q => new SelectListItem { 
                Text = q.Name,
                Value = q.Id.ToString()
            }); //fetch all data from DB and parse each object to SelectListItem type to display it as a list

            var model = new CreateLeaveRequestVM
            {
                LeaveTypes = leaveTypeItems,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2)
            };

            return View(model);
        }

        // POST: LeaveRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveRequestVM model)
        {
            var leaveTypes = await _leaveTypeRepository.FindAll();
            var leaveTypeItems = leaveTypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            model.LeaveTypes = leaveTypeItems; //repopulate leave types in case of error

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if(DateTime.Compare(model.StartDate, model.EndDate) > 1)
                {
                    ModelState.AddModelError("", "Data zakonczenia nie może być datą przed datą rozpoczęcia");
                    return View(model);
                }

                var employee = await _userManager.GetUserAsync(User); //getting signinuser
                var allocation = await _leaveAllocationRepository.GetLeaveAllocationsByEmployeeAndType(employee.Id,model.LeaveTypeId);
                int daysRequested = (int)(model.EndDate.Date - model.StartDate.Date).TotalDays;

                if (allocation == null)
                {
                    ModelState.AddModelError("", "Urlop nie został przydzielony");
                    return View(model);
                }

                 if (daysRequested > allocation.NumberOfDays)
                  {
                        ModelState.AddModelError("", "Niewystarczająca liczba dni wolnych do wykorzystania");
                        return View(model);
                  }
                
               
                var leaveRequestModel = new LeaveRequestVM
                {
                    RequestingEmployeeId = employee.Id,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Approved = null,
                    DateRequested = DateTime.Now,
                    DateActioned = DateTime.Now,
                    LeaveTypeId = model.LeaveTypeId,
                    Comment = model.Comment
                };

                var leaveRequest = _mapper.Map<LeaveRequests>(leaveRequestModel);
                var isSuccess = await _leaveRequestRepository.Create(leaveRequest);

                if(!isSuccess)
                {
                    ModelState.AddModelError("", "Coś poszło nie tak. Skontaktuj się z administratorem systemu.");
                    return View(model);
                }

                return RedirectToAction("Index","Home");
            }
            catch 
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }
        }

        public async Task<ActionResult> MyLeaveRequests()
        {
            var employee = await _userManager.GetUserAsync(User);

            var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsByEmployee(employee.Id);
            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsByEmployee(employee.Id);

            var leaveAllocationsModel = _mapper.Map<List<LeaveAllocationVM>>(leaveAllocations);
            var leaveRequestsModel = _mapper.Map<List<LeaveRequestVM>>(leaveRequests);

            var model = new EmployeeLeaveRequestsVM
            {
                LeaveRequests = leaveRequestsModel,
                LeaveAllocations = leaveAllocationsModel
            };

            return View(model);
        }

        // GET: LeaveRequest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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


        public async Task<ActionResult> CancelRequest(int id)
        {
            try
            {
                var success = await _leaveRequestRepository.Exists(id);
                if (!success)
                {
                    return NotFound();
                }

                var leaveRequest = await _leaveRequestRepository.FindById(id);

                if (leaveRequest.StartDate < DateTime.Now)
                {
                    ModelState.AddModelError("", "Niepoprawna operacja anulowania wniosku.");
                    return RedirectToAction(nameof(MyLeaveRequests));
                }
                    leaveRequest.Cancelled = true;

                    success = await _leaveRequestRepository.Update(leaveRequest);
                    if (!success)
                    {
                        ModelState.AddModelError("", "Niepoprawna operacja anulowania wniosku.");
                        return RedirectToAction(nameof(MyLeaveRequests));
                    }

                    return RedirectToAction(nameof(MyLeaveRequests));

            }

            catch 
            {
                ModelState.AddModelError("", "Something went wrong.");
                return RedirectToAction(nameof(MyLeaveRequests));
            }
        }

        // GET: LeaveRequest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequest/Delete/5
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