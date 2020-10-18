using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using leave_management.Services.LeaveHelper.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace leave_management.Controllers
{
    public class SchedulerEvent
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Guid { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ShiftStartTime { get; set; }
        public string ShiftEndTime { get; set; }
        public bool IsAllDay { get; set; }
        public string EmployeeId { get; set; }
        public string Description { get; set; }
        public int PauseTimeLength { get; set; }
        public string WorkTimeLength { get; set; }
    }
    [EnableCors("SchedulerPolicy")]
    public class WorkTimeScheduleController : Controller
    {
        private readonly IWorkTimeScheduleRepository _workTimeScheduleRepository;
        private readonly IWorkTimeScheduleEmployeesRepository _workTimeScheduleEmployeesRepository;
        private readonly IWorkingTimeSystemRepository _workingTimeSystemRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILeaveHelper _leaveHelper;
        private readonly IMapper _mapper;
        public WorkTimeScheduleController(IWorkTimeScheduleRepository workTimeScheduleRepository, 
            IWorkTimeScheduleEmployeesRepository workTimeScheduleEmployeesRepository,
            IWorkingTimeSystemRepository workingTimeSystemRepository,
            IEmployeeRepository employeeRepository,
            ILeaveHelper leaveHelper,
            IMapper mapper)
        {
            _workTimeScheduleRepository = workTimeScheduleRepository;
            _workTimeScheduleEmployeesRepository = workTimeScheduleEmployeesRepository;
            _workingTimeSystemRepository = workingTimeSystemRepository;
            _employeeRepository = employeeRepository;
            _leaveHelper = leaveHelper;
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
            var workTimeSystems = await _workingTimeSystemRepository.FindAll();
            var workTimeSystemsItems = workTimeSystems.Select(q => new SelectListItem
            {
                Text = q.Name.ToString(),
                Value = q.Id.ToString()
            });

            model.Employees = employeesItems;
            model.WorkingTimeSystems = workTimeSystemsItems;
            return View(model);
        }

        // POST: WorkTimeSchedule/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateWorkTimeScheduleVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (model.DateFrom.Date > model.DateTo.Date)
                {
                    ModelState.AddModelError("", "Podane daty są nieprawidłowe");
                    return View(model);
                }
                model.DateCreated = DateTime.Now;
                var workTimeSchedule = _mapper.Map<WorkTimeSchedule>(model);
                var isSuccess = await _workTimeScheduleRepository.Create(workTimeSchedule);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View(model);
                }
                foreach (var item in model.EmployeeIds)
                {
                    var workTimeScheduleEmployee = new WorkTimeScheduleEmployee();
                    workTimeScheduleEmployee.ScheduleId = workTimeSchedule.Id;
                    workTimeScheduleEmployee.EmployeeId = item;
                    var isSuccessWorkTimeScheduleEmployees = await _workTimeScheduleEmployeesRepository.Create(workTimeScheduleEmployee);
                    if (!isSuccessWorkTimeScheduleEmployees)
                    {
                        ModelState.AddModelError("", "Something went wrong");
                        return View(model);
                    }
                }

                return RedirectToAction("Step2", new { id = workTimeSchedule.Id });
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong");
                return View();
            }
        }

        // GET: WorkTimeSchedule/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var success = await _workTimeScheduleRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var workTimeSchedule = await _workTimeScheduleRepository.FindById(id);
            var model = _mapper.Map<CreateWorkTimeScheduleVM>(workTimeSchedule);
            var employees = await _employeeRepository.FindAll();
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                Value = q.Id.ToString()
            });
            var workTimeSystems = await _workingTimeSystemRepository.FindAll();
            var workTimeSystemsItems = workTimeSystems.Select(q => new SelectListItem
            {
                Text = q.Name.ToString(),
                Value = q.Id.ToString()
            });

            model.Employees = employeesItems;
            model.WorkingTimeSystems = workTimeSystemsItems;

            return View(model);
        }

        // POST: WorkTimeSchedule/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CreateWorkTimeScheduleVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var workTimeSchedule = _mapper.Map<WorkTimeSchedule>(model);
                var isSuccess = await _workTimeScheduleRepository.Update(workTimeSchedule);
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

        // GET: WorkTimeSchedule/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var workTimeSchedule = await _workTimeScheduleRepository.FindById(id);
            if (workTimeSchedule == null)
            {
                return NotFound();
            }
            var isSuccess = await _workTimeScheduleRepository.Delete(workTimeSchedule);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
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

        public async Task<ActionResult> Approve(int id)
        {
            var workTimeSchedule = await _workTimeScheduleRepository.FindById(id);
            if (workTimeSchedule == null)
            {
                return NotFound();
            }
            var isSuccess = false;
            if (workTimeSchedule.IsApproved)
            {
                workTimeSchedule.IsApproved = false;
                isSuccess = await _workTimeScheduleRepository.Update(workTimeSchedule);
            }
            else
            {
                workTimeSchedule.IsApproved = true;
                isSuccess = await _workTimeScheduleRepository.Update(workTimeSchedule);
            }
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: WorkTimeSchedule/Step2/5
        public async Task<ActionResult> Step2(int id)
        {
            var success = await _workTimeScheduleRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var workTimeSchedule = await _workTimeScheduleRepository.FindById(id);
            var workTimeScheduleEmployees = await _workTimeScheduleEmployeesRepository.FindAll();
            var EmployeeIds = new List<string>();
            var model = new Step2WorkTimeScheduleVM();
            foreach (var item in workTimeScheduleEmployees)
            {   
                if(item.ScheduleId == id)
                {
                    EmployeeIds.Add(item.EmployeeId); 
                }
            }
            var ListOfNames = new List<string>();
            foreach (var item in EmployeeIds)
            {
                var Employee = await _employeeRepository.FindById(item);
                ListOfNames.Add(String.Format("{0} {1}", Employee.Lastname, Employee.Firstname));
            }
            var employees = await _employeeRepository.FindAll();
            
            List<SelectListItem> employeeItems = new List<SelectListItem>();
            for (int i = 0; i<EmployeeIds.Count;i++)
            {
                employeeItems.Add(new SelectListItem
                {
                    Text = ListOfNames[i],
                    Value = EmployeeIds[i]
                });
            }
            //var employeesItems = employees.Select(q => new SelectListItem
            //{
            //    Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
            //    Value = q.Id.ToString()
            //});

            List<DateTime> UnavailableDates = new List<DateTime>();
            UnavailableDates = _leaveHelper.ListUnavailableDates(workTimeSchedule.DateFrom, workTimeSchedule.DateTo);

            model.EmployeeFullNames = ListOfNames;
            model.EmployeeIds = EmployeeIds;
            model.ScheduleId = id;
            model.DateFrom = workTimeSchedule.DateFrom;
            model.DateTo= workTimeSchedule.DateTo;
            model.Employees = employeeItems;
            model.UnavailableDates = UnavailableDates;
            return View(model);
        }

        // POST: WorkTimeSchedule/Step2/5
        [HttpPost]
        
      
        public async Task<ActionResult> Step2([FromBody]List<SchedulerEvent> schedulerEvents)
        {
            try
            {
                var ScheduleEventData = new WorkTimeScheduleEvent();
                //var model = new W
                //var workTimeSchedule = _mapper.Map<WorkTimeSchedule>(model);
                //foreach (var item in schedulerEvents)
                //{
                //    var data 
                //    await _workTimeScheduleRepository.Create(item);

                //}
                return  RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}