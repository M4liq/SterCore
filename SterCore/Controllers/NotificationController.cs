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
    public class NotificationController : Controller
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly INotificationTypeRepository _notificationTypeRepository;
        private readonly IMapper _mapper;
        public NotificationController(INotificationRepository notificationRepository, IEmployeeRepository employeeRepository,  INotificationTypeRepository notificationTypeRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _employeeRepository = employeeRepository;
            _notificationTypeRepository = notificationTypeRepository;
            _mapper = mapper;
        }

        // GET: Notification
        public async Task<ActionResult> Index()
        {
            var employees = await _employeeRepository.FindAll();
            var notificationTypes = await _notificationTypeRepository.FindAll();
            var Notifications = await _notificationRepository.FindAll();
            var model = _mapper.Map<List<Notification>, List<NotificationVM>>(Notifications.ToList());
            foreach (var item in model)
            {
                var employee = employees.FirstOrDefault(q => q.Id == item.EmployeeId);
                var notificationType = notificationTypes.FirstOrDefault(q => q.Id == item.NotificationTypeId);
                var employeeFulName = String.Format("{0} {1}", employee.Lastname, employee.Firstname);
                item.EmployeeFullName = employeeFulName;
                item.NotificationTypeName = notificationType.Name;
            }
            return View(model);
        }

        // GET: Notification/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _notificationRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var employees = await _employeeRepository.FindAll();
            var notificationTypes = await _notificationTypeRepository.FindAll();
            var Notifications = await _notificationRepository.FindById(id);
            var model = _mapper.Map<NotificationVM>(Notifications);
            var employee = employees.FirstOrDefault(q => q.Id == model.EmployeeId);
            var notificationType = notificationTypes.FirstOrDefault(q => q.Id == model.NotificationTypeId);
            var employeeFulName = String.Format("{0} {1}", employee.Lastname, employee.Firstname);

            model.NotificationTypeName = notificationType.Name;
            model.EmployeeFullName = employeeFulName;
            return View(model);
        }

        // GET: Notification/Create
        public async Task<ActionResult> Create()
        {
            var model = new CreateNotificationVM();
            var employees = await _employeeRepository.FindAll();
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                Value = q.Id.ToString()
            });
            var notificationTypes = await _notificationTypeRepository.FindAll();
            var notificationTypesItems = notificationTypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            model.Employees = employeesItems;
            model.NotificationTypes = notificationTypesItems;
            return View(model);
        }

        // POST: Notification/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateNotificationVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (model.DateOfNotification.Date > model.DateValidUntil.Date)
                {
                    var employees = await _employeeRepository.FindAll();
                    var employeesItems = employees.Select(q => new SelectListItem
                    {
                        Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                        Value = q.Id.ToString()
                    });
                    var notificationTypes = await _notificationTypeRepository.FindAll();
                    var notificationTypesItems = notificationTypes.Select(q => new SelectListItem
                    {
                        Text = q.Name,
                        Value = q.Id.ToString()
                    });

                    model.Employees = employeesItems;
                    model.NotificationTypes = notificationTypesItems;

                    ModelState.AddModelError("", "Podane daty są nieprawidłowe");
                    return View(model);
                }
                var Notification = _mapper.Map<Notification>(model);
                var isSuccess = await _notificationRepository.Create(Notification);
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

        // GET: Notification/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var success = await _notificationRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var Notification = await _notificationRepository.FindById(id);
            var model = _mapper.Map<CreateNotificationVM>(Notification);


            

            var employees = await _employeeRepository.FindAll();
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                Value = q.Id.ToString()
            });
            var notificationTypes = await _notificationTypeRepository.FindAll();
            var notificationTypesItems = notificationTypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            model.Employees = employeesItems;
            model.NotificationTypes = notificationTypesItems;

            return View(model);
        }

        // POST: Notification/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CreateNotificationVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (model.DateOfNotification.Date > model.DateValidUntil.Date)
                {
                    var employees = await _employeeRepository.FindAll();
                    var employeesItems = employees.Select(q => new SelectListItem
                    {
                        Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                        Value = q.Id.ToString()
                    });
                    var notificationTypes = await _notificationTypeRepository.FindAll();
                    var notificationTypesItems = notificationTypes.Select(q => new SelectListItem
                    {
                        Text = q.Name,
                        Value = q.Id.ToString()
                    });

                    model.Employees = employeesItems;
                    model.NotificationTypes = notificationTypesItems;

                    ModelState.AddModelError("", "Podane daty są nieprawidłowe");
                    return View(model);
                }
                var Notification = _mapper.Map<Notification>(model);
                var isSuccess = await _notificationRepository.Update(Notification);
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

        // GET: Notification/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var Notification = await _notificationRepository.FindById(id);
            if (Notification == null)
            {
                return NotFound();
            }
            var isSuccess = await _notificationRepository.Delete(Notification);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Notification/Delete/5
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