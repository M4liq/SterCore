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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace leave_management.Controllers
{
    [Authorize]
    public class ResourceController : Controller
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IResourceTypeRepository _resourceTypeRepository;
        private readonly IMapper _mapper;
        public ResourceController(IResourceRepository resourceRepository, IEmployeeRepository employeeRepository, IResourceTypeRepository resourceTypeRepository, IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _employeeRepository = employeeRepository;
            _resourceTypeRepository = resourceTypeRepository;
            _mapper = mapper;
        }

        // GET: Resource
        public async Task<ActionResult> Index()
        {
            var employees = await _employeeRepository.FindAll();
            var resourceTypes = await _resourceTypeRepository.FindAll();
            var Resources = await _resourceRepository.FindAll();
            var model = _mapper.Map<List<Resource>, List<ResourceVM>>(Resources.ToList());
            foreach (var item in model)
            {
                var employee = employees.FirstOrDefault(q => q.Id == item.EmployeeId);
                var resourceType = resourceTypes.FirstOrDefault(q => q.Id == item.ResourceTypeId);
                var employeeFulName = String.Format("{0} {1}", employee.Lastname, employee.Firstname);
                item.EmployeeFullName = employeeFulName;
                item.ResourceTypeName = resourceType.Name;
            }
            return View(model);
        }

        // GET: Resource/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _resourceRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var employees = await _employeeRepository.FindAll();
            var resourceTypes = await _resourceTypeRepository.FindAll();
            var Resources = await _resourceRepository.FindById(id);
            var model = _mapper.Map<ResourceVM>(Resources);
            var employee = employees.FirstOrDefault(q => q.Id == model.EmployeeId);
            var resourceType = resourceTypes.FirstOrDefault(q => q.Id == model.ResourceTypeId);
            var employeeFulName = String.Format("{0} {1}", employee.Lastname, employee.Firstname);

            model.ResourceTypeName = resourceType.Name;
            model.EmployeeFullName = employeeFulName;
            return View(model);
        }
        [Authorize(Roles = "Administrator, Employer, Agent")]
        // GET: Resource/Create
        public async Task<ActionResult> Create()
        {
            var model = new CreateResourceVM();
            var employees = await _employeeRepository.FindAll();
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                Value = q.Id.ToString()
            });
            var resourceTypes = await _resourceTypeRepository.FindAll();
            var resourceTypesItems = resourceTypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            model.Employees = employeesItems;
            model.ResourceTypes = resourceTypesItems;
            return View(model);
        }

        // POST: Resource/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateResourceVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (model.DateFrom.Date > model.DateUntil.Date)
                {
                    var employees = await _employeeRepository.FindAll();
                    var employeesItems = employees.Select(q => new SelectListItem
                    {
                        Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                        Value = q.Id.ToString()
                    });
                    var resourceTypes = await _resourceTypeRepository.FindAll();
                    var resourceTypesItems = resourceTypes.Select(q => new SelectListItem
                    {
                        Text = q.Name,
                        Value = q.Id.ToString()
                    });

                    model.Employees = employeesItems;
                    model.ResourceTypes = resourceTypesItems;

                    ModelState.AddModelError("", "Podane daty są nieprawidłowe");
                    return View(model);
                }
                var Resource = _mapper.Map<Resource>(model);
                var isSuccess = await _resourceRepository.Create(Resource);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View();
            }
        }

        // GET: Resource/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var success = await _resourceRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var Resource = await _resourceRepository.FindById(id);
            var model = _mapper.Map<CreateResourceVM>(Resource);




            var employees = await _employeeRepository.FindAll();
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                Value = q.Id.ToString()
            });
            var resourceTypes = await _resourceTypeRepository.FindAll();
            var resourceTypesItems = resourceTypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            model.Employees = employeesItems;
            model.ResourceTypes = resourceTypesItems;

            return View(model);
        }

        // POST: Resource/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CreateResourceVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (model.DateFrom.Date > model.DateUntil.Date)
                {
                    var employees = await _employeeRepository.FindAll();
                    var employeesItems = employees.Select(q => new SelectListItem
                    {
                        Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                        Value = q.Id.ToString()
                    });
                    var resourceTypes = await _resourceTypeRepository.FindAll();
                    var resourceTypesItems = resourceTypes.Select(q => new SelectListItem
                    {
                        Text = q.Name,
                        Value = q.Id.ToString()
                    });

                    model.Employees = employeesItems;
                    model.ResourceTypes = resourceTypesItems;

                    ModelState.AddModelError("", "Podane daty są nieprawidłowe");
                    return View(model);
                }
                var Resource = _mapper.Map<Resource>(model);
                var isSuccess = await _resourceRepository.Update(Resource);
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

        // GET: Resource/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var Resource = await _resourceRepository.FindById(id);
            if (Resource == null)
            {
                return NotFound();
            }
            var isSuccess = await _resourceRepository.Delete(Resource);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Resource/Delete/5
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