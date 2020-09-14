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
    public class CalendarController : Controller
    {
        private readonly ITrainingCourseRepository _trainingCourseRepository;
        private readonly IMedicalCheckUpRepository _medicalCheckUpRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;
        public CalendarController(ITrainingCourseRepository trainingCourseRepository, 
            IMedicalCheckUpRepository medicalCheckUpRepository,
            INotificationRepository notificationRepository, 
            IContractRepository contractRepository, 
            IEmployeeRepository employeeRepo, 
            IMapper mapper)
        {
            _trainingCourseRepository = trainingCourseRepository;
            _medicalCheckUpRepository = medicalCheckUpRepository;
            _notificationRepository = notificationRepository;
            _contractRepository = contractRepository;
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }
        // GET: Calendar
        public async Task<ActionResult> Index()
        {
            var trainingCourses = await _trainingCourseRepository.FindAll();
            var medicalCheckUps = await _medicalCheckUpRepository.FindAll();
            var notifications = await _notificationRepository.FindAll();
            var contracts = await _contractRepository.FindAll();
            var employees = await _employeeRepo.FindAll();

            var model = new List<CalendarVM>();
            
            foreach (var item in trainingCourses)
            {
                var employeeFirstName = employees.FirstOrDefault(q => q.Id == item.EmployeeId).Firstname;
                var employeeLastName = employees.FirstOrDefault(q => q.Id == item.EmployeeId).Lastname;
                model.Add(new CalendarVM
                {
                    TrainingCourseEmployeeFullName = String.Format("{0} {1} -->",employeeLastName,employeeFirstName),
                    TrainingCourseStartDate = item.DateOfTrainingCourse,
                    //TrainingCourseEndDate = item.DateValidUntil
                });
                model.Add(new CalendarVM
                {
                    TrainingCourseEmployeeFullName = String.Format("<-- {0} {1}", employeeLastName, employeeFirstName),
                    TrainingCourseStartDate = item.DateValidUntil,
                });
            }
            foreach (var item in medicalCheckUps)
            {
                var employeeFirstName = employees.FirstOrDefault(q => q.Id == item.EmployeeId).Firstname;
                var employeeLastName = employees.FirstOrDefault(q => q.Id == item.EmployeeId).Lastname;
                model.Add(new CalendarVM
                {
                    MedicalCheckUpEmployeeFullName = String.Format("{0} {1}", employeeLastName, employeeFirstName),
                    MedicalCheckUpStartDate = item.DateOfMedicalExamination,
                    MedicalCheckUpEndDate = item.ValidUntil
                });
            }
            foreach (var item in notifications)
            {
                var employeeFirstName = employees.FirstOrDefault(q => q.Id == item.EmployeeId).Firstname;
                var employeeLastName = employees.FirstOrDefault(q => q.Id == item.EmployeeId).Lastname;
                model.Add(new CalendarVM
                {
                    NotificationEmployeeFullName = String.Format("{0} {1}", employeeLastName, employeeFirstName),
                    NotificationStartDate = item.DateOfNotification,
                    NotificationEndDate = item.DateValidUntil
                });
            }
            foreach (var item in contracts)
            {
                var employeeFirstName = employees.FirstOrDefault(q => q.Id == item.EmployeeId).Firstname;
                var employeeLastName = employees.FirstOrDefault(q => q.Id == item.EmployeeId).Lastname;
                model.Add(new CalendarVM
                {
                    ContractEmployeeFullName = String.Format("{0} {1}", employeeLastName, employeeFirstName),
                    ContractStartDate = item.DateValidFrom,
                    ContractEndDate = item.DateValidUntil ?? item.DateValidFrom,
                    ContractDateOfContractAgreement = item.DateOfContractAgreement
                });
            }


            return View(model);
        }

        // GET: Calendar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Calendar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calendar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: Calendar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Calendar/Edit/5
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

        // GET: Calendar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Calendar/Delete/5
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