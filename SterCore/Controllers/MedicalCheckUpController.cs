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
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            var typesOfMedicalCheckUps = _typeOfMedicalCheckUpRepo.FindAll().Result;
            var model = new List<MedicalCheckUpVM>();
            foreach (var item in MappedModel)
            {
                model.Add(new MedicalCheckUpVM
                {
                    Id = item.Id,
                    Comment = item.Comment,
                    DateOfMedicalExamination = item.DateOfMedicalExamination,
                    ValidUntil = item.ValidUntil,
                    EmployeeFullName = employees.FirstOrDefault(q => q.Id == item.EmployeeId).Lastname + " " + employees.FirstOrDefault(q => q.Id == item.EmployeeId).Firstname,
                    TypeOfMedicalCheckUpName = typesOfMedicalCheckUps.FirstOrDefault(q => q.Id == item.TypeOfMedicalCheckUpId).name,
                    EmployeeId = item.EmployeeId,
                    TypeOfMedicalCheckUpId = item.TypeOfMedicalCheckUpId,
                    IsDisplayedToEmployee = item.IsDisplayedToEmployee,
                    IsDisplayedToSupervisor = item.IsDisplayedToSupervisor,
                    OrganizationToken = item.OrganizationToken
                });
            }

            return View(model);
        }

        // GET: MedicalCheckUp/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _medicalCheckUpRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var medicalCheckUp = await _medicalCheckUpRepository.FindById(id);
            var model = _mapper.Map<MedicalCheckUpVM>(medicalCheckUp);
            model.EmployeeFullName = _employeeRepo.FindById(model.EmployeeId).Result.Lastname + " " + _employeeRepo.FindById(model.EmployeeId).Result.Firstname;
            model.TypeOfMedicalCheckUpName = _typeOfMedicalCheckUpRepo.FindById(model.TypeOfMedicalCheckUpId).Result.name;
            return View(model);
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
        public async Task<ActionResult> Create(CreateMedicalCheckUpVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (model.DateOfMedicalExamination.Date > model.ValidUntil.Date)
                {
                    ModelState.AddModelError("", "Podane daty są nieprawidłowe");
                    return View(model);
                }
                var medicalCheckUp = _mapper.Map<MedicalCheckUp>(model);
                var isSuccess = await _medicalCheckUpRepository.Create(medicalCheckUp);
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

        // GET: MedicalCheckUp/Edit/5
        public async Task<ActionResult> Edit(int id, int typeOfMedicalCheckUpId, string employeeId, string organizationToken)
        {
            var success = await _medicalCheckUpRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var SelectListItemTrue = new SelectListItem("Tak", "true");
            var SelectListItemFalse = new SelectListItem("Nie", "false");
            List<SelectListItem> SelectListWithTrueOrFalse = new List<SelectListItem>();
            SelectListWithTrueOrFalse.Add(SelectListItemTrue);
            SelectListWithTrueOrFalse.Add(SelectListItemFalse);

            var medicalCheckUp = await _medicalCheckUpRepository.FindById(id);
            var model = _mapper.Map<EditMedicalCheckUpVM>(medicalCheckUp);
            model.EmployeeId = employeeId;
            model.TypeOfMedicalCheckUpId = typeOfMedicalCheckUpId;
            model.isDisplayedToSupervisors = SelectListWithTrueOrFalse;
            model.isDisplayedToEmployees = SelectListWithTrueOrFalse;
            model.OrganizationToken = organizationToken;
            return View(model);
        }

        // POST: MedicalCheckUp/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditMedicalCheckUpVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (model.DateOfMedicalExamination.Date > model.ValidUntil.Date)
                {
                    var SelectListItemTrue = new SelectListItem("Tak", "true");
                    var SelectListItemFalse = new SelectListItem("Nie", "false");
                    List<SelectListItem> SelectListWithTrueOrFalse = new List<SelectListItem>();
                    SelectListWithTrueOrFalse.Add(SelectListItemTrue);
                    SelectListWithTrueOrFalse.Add(SelectListItemFalse);
                    model.isDisplayedToSupervisors = SelectListWithTrueOrFalse;
                    model.isDisplayedToEmployees = SelectListWithTrueOrFalse;
                    ModelState.AddModelError("", "Podane daty są nieprawidłowe");
                    return View(model);
                }
                var medicalCheckUp = _mapper.Map<MedicalCheckUp>(model);
                var isSuccess = await _medicalCheckUpRepository.Update(medicalCheckUp);
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

        // GET: MedicalCheckUp/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var medicalCheckUp = await _medicalCheckUpRepository.FindById(id);
            if (medicalCheckUp == null)
            {
                return NotFound();
            }
            var isSuccess = await _medicalCheckUpRepository.Delete(medicalCheckUp);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
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