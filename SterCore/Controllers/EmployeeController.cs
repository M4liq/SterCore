using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using leave_management.Repository;
using leave_management.Services.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace leave_management.Controllers
{
    [Authorize(Roles = "Administrator, Agent, Employer")]
    public class EmployeeController : Controller
    {
        public EmployeeController(
            UserManager<Employee> userManager,
            IMapper mapper,
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _userManager = userManager;
            _employeeRepository = employeeRepository;
        }

       private  UserManager<Employee> _userManager { get; }
       private  IMapper _mapper { get; }
       public IEmployeeRepository _employeeRepository { get; }
       public IDepartmentRepository _departmentRepository { get; }

        // GET: Employee
        public async Task<ActionResult> Index()
        {
            var sameOrginEmployees = await _employeeRepository.FindAll();

            var model = _mapper.Map<IEnumerable<EmployeeVM>>(sameOrginEmployees);
            return View(model);
        }

        // GET: Employee/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var employee = await _userManager.FindByIdAsync(id);
            var model = _mapper.Map<EmployeeVM>(employee);
            return View(model);
        }

        // GET: Employee/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var employee = await _userManager.FindByIdAsync(id); 

            if (employee == null)
               return NotFound();

            var model = _mapper.Map<EditEmployeeVM>(employee);
            model.PreviousEmail = employee.Email;

            var departments = await _departmentRepository.FindAll();
            var departmentItems = departments.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            model.Departments = departmentItems;
            return View(model);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditEmployeeVM model)
        {
            try
            {
                //loading deaprtments in case of error
                var departments = await _departmentRepository.FindAll();
                var departmentItems = departments.Select(q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Id.ToString()
                });
                model.Departments = departmentItems;

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var employee = await _userManager.FindByIdAsync(model.Id);
                if(employee == null)
                    {
                        ModelState.AddModelError("", "Nie ma takiego pracownika ...");
                        return View(model);
                    }

                //Mapping fields manually due tu errors in DB update. Automapper does not assing all required fields.
                employee.Email = model.Email;
                employee.Firstname = model.Firstname;
                employee.Lastname = model.Lastname;
                employee.PhoneNumber = model.PhoneNumber;
                employee.DateOfBirth = model.DateOfBirth;
                employee.DepartmentId = model.DepartmentId;

                var isUnique = ((await _userManager.FindByEmailAsync(employee.Email) != null) && model.PreviousEmail!=model.Email);
                
                if(!isUnique)
                {
                    ModelState.AddModelError("", "Istnieje już użytkownik z takim adresem email ...");
                    return View(model);
                }

                var result = await _userManager.UpdateAsync(employee);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Coś poszło nie tak, skontaktuj się z administratorem ...");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Coś poszło nie tak, skontaktuj się z administratorem ...");
                return View(model);
            
            }
        }

        // GET: Employee/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
 
            if (user == null)
            {
                return NotFound();
            }

            if (user.InitialAdministrator)
            {
                return RedirectToAction(nameof(Index));
            }

            var deleting = await _userManager.DeleteAsync(user);
            if (!deleting.Succeeded)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}