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
    public class DocumentController : Controller
    {
        private readonly IDocumentsRepository _documentsRepository;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;
        public DocumentController(IDocumentsRepository documentsRepository, IEmployeeRepository employeeRepo, UserManager<Employee> userManager, IMapper mapper)
        {
            _documentsRepository = documentsRepository;
            _employeeRepo = employeeRepo;
            _userManager = userManager;
            _mapper = mapper;
        }
        // GET: Document
        public async Task<ActionResult> Index()
        {
            var documents = _documentsRepository.FindAll().Result;
            var mappedModel = _mapper.Map<List<Document>, List<DocumentVM>>(documents.ToList());
            var model = new List<DocumentVM>();
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            foreach (var item in mappedModel)
            {
                model.Add(new DocumentVM
                {
                    Id = item.Id,
                    DateCreated = item.DateCreated,
                    DocumentName = item.DocumentName,
                    EmployeeId = item.EmployeeId,
                    ShowCompanyWide = item.ShowCompanyWide,
                    ShowSelectedDepartment = item.ShowSelectedDepartment,
                    ShowSelectedEmployee = item.ShowSelectedEmployee,
                    EmployeeFullName = employees.FirstOrDefault(q => q.Id == item.EmployeeId).Firstname.ToString() + " " + employees.FirstOrDefault(q => q.Id == item.EmployeeId).Lastname.ToString()
                });
            }
            return View(model);
        }

        // GET: Document/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _documentsRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var document = await _documentsRepository.FindById(id);
            var model = _mapper.Map<DocumentVM>(document);
            model.EmployeeFullName = _employeeRepo.FindById(model.EmployeeId).Result.Lastname + " " + _employeeRepo.FindById(model.EmployeeId).Result.Firstname;
            return View(model);
        }

        // GET: Document/Create
        public async Task<ActionResult> Create()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = q.Firstname + " " + q.Lastname,
                Value = q.Id.ToString()
            });

            var model = new CreateDocumentVM
            {
                Employees = employeesItems,
            };
            return View(model);
        }

        // POST: Document/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateDocumentVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
               
                var document = _mapper.Map<Document>(model);
                document.DateCreated = DateTime.Now;
                var isSuccess = await _documentsRepository.Create(document);
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

        // GET: Document/Edit/5
        public async Task<ActionResult> Edit(int id, string employeeId, DateTime dateCreated)
        {
            var success = await _documentsRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var document = await _documentsRepository.FindById(id);
            var model = _mapper.Map<CreateDocumentVM>(document);
            model.EmployeeId = employeeId;
            model.DateCreated = dateCreated;
            return View(model);
        }

        // POST: Document/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CreateDocumentVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var document = _mapper.Map<Document>(model);
                var isSuccess = await _documentsRepository.Update(document);
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

        // GET: Document/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var document = await _documentsRepository.FindById(id);
            if (document == null)
            {
                return NotFound();
            }
            var isSuccess = await _documentsRepository.Delete(document);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Document/Delete/5
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