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
    public class MedicalCheckUpController : Controller
    {
        private readonly IMedicalCheckUpRepository _medicalCheckUpRepository;
        private readonly ITypeOfMedicalCheckUpRepository _typeOfMedicalCheckUpRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;


        public MedicalCheckUpController(IMedicalCheckUpRepository medicalCheckUpRepository,
            ITypeOfMedicalCheckUpRepository typeOfMedicalCheckUpRepo,
            IEmployeeRepository employeeRepo,
            UserManager<Employee> userManager,
            IMapper mapper
            )
        {
            _medicalCheckUpRepository = medicalCheckUpRepository;
            _typeOfMedicalCheckUpRepo = typeOfMedicalCheckUpRepo;
            _employeeRepo = employeeRepo;
            _userManager = userManager;
            _mapper = mapper;
        }
        // GET: MedicalCheckUp
        public async Task<ActionResult> Index()
        {
            var medicalCheckUps = await _medicalCheckUpRepository.FindAll();
            var MappedModel = _mapper.Map<List<MedicalCheckUp>, List<MedicalCheckUpVM>>(medicalCheckUps.ToList());
            var model = new List<MedicalCheckUpVM>();
            foreach (var item in MappedModel)
            {
                model.Add(new MedicalCheckUpVM
                {
                    Comment = item.Comment,
                    DateOfMedicalExamination = item.DateOfMedicalExamination,
                    ValidUntil = item.ValidUntil,
                    EmployeeFullName = _employeeRepo.FindById(item.EmployeeId).Result.Lastname + _employeeRepo.FindById(item.EmployeeId).Result.Firstname,
                    TypeOfMedicalCheckUpName = _typeOfMedicalCheckUpRepo.FindById(item.TypeOfMedicalCheckUpId).Result.name,
                    EmployeeId=item.EmployeeId,
                    TypeOfMedicalCheckUpId = item.TypeOfMedicalCheckUpId,
                    IsDisplayedToEmployee = item.IsDisplayedToEmployee,
                    IsDisplayedToSupervisor = item.IsDisplayedToSupervisor
                });
            }

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
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = q.Firstname + " " + q.Lastname,
                Value = q.Id.ToString()
            });

            var TypeOfMedicalCheckUps = _typeOfMedicalCheckUpRepo.FindAll().Result;
            var TypeOfMedicalCheckUpsItems = TypeOfMedicalCheckUps.Select(q => new SelectListItem
            {
                Text = q.name.ToString(),
                Value = q.Id.ToString()
            });
            var SelectListItemTrue = new SelectListItem("Tak", "true");
            var SelectListItemFalse = new SelectListItem("Nie", "false");
            List<SelectListItem> SelectListWithTrueOrFalse = new List<SelectListItem>();
            SelectListWithTrueOrFalse.Add(SelectListItemTrue);
            SelectListWithTrueOrFalse.Add(SelectListItemFalse);

            var model = new CreateMedicalCheckUpVM
            {
                Employees = employeesItems,
                TypeOfMedicalCheckUps = TypeOfMedicalCheckUpsItems,
                isDisplayedToSupervisors = SelectListWithTrueOrFalse,
                isDisplayedToEmployees = SelectListWithTrueOrFalse
            };
            return View(model);
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