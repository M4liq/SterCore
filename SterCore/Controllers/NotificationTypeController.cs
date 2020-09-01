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
    public class NotificationTypeController : Controller
    {
        private readonly INotificationTypeRepository _notificationTypeRepository;
        private readonly IMapper _mapper;
        public NotificationTypeController(INotificationTypeRepository notificationTypeRepository, IMapper mapper)
        {
            _notificationTypeRepository = notificationTypeRepository;
            _mapper = mapper;
        }

        // GET: notificationType
        public async Task<ActionResult> Index()
        {
            var notificationTypes = await _notificationTypeRepository.FindAll();
            var model = _mapper.Map<List<NotificationType>, List<NotificationTypeVM>>(notificationTypes.ToList());
            return View(model);
        }

        // GET: notificationType/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _notificationTypeRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var notificationTypes = await _notificationTypeRepository.FindById(id);
            var model = _mapper.Map<NotificationTypeVM>(notificationTypes);
            return View(model);
        }

        // GET: notificationType/Create
        public async Task<ActionResult> Create()
        {
            var model = new NotificationTypeVM();
            return View(model);
        }

        // POST: notificationType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NotificationTypeVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var notificationType = _mapper.Map<NotificationType>(model);
                var isSuccess = await _notificationTypeRepository.Create(notificationType);
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

        // GET: notificationType/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var success = await _notificationTypeRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var notificationType = await _notificationTypeRepository.FindById(id);
            var model = _mapper.Map<NotificationTypeVM>(notificationType);
            
            return View(model);
        }

        // POST: notificationType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(NotificationTypeVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var notificationType = _mapper.Map<NotificationType>(model);
                var isSuccess = await _notificationTypeRepository.Update(notificationType);
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

        // GET: notificationType/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var notificationType = await _notificationTypeRepository.FindById(id);
            if (notificationType == null)
            {
                return NotFound();
            }
            var isSuccess = await _notificationTypeRepository.Delete(notificationType);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: notificationType/Delete/5
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