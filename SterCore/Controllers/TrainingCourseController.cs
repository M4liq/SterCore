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

namespace leave_management.Controllers
{
    [Authorize]
    public class TrainingCourseController : Controller
    {
        private readonly ITrainingCourseRepository _trainingCourseRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITrainingCourseTypeRepository _trainingCourseTypeRepository;
        private readonly IMapper _mapper;
        public TrainingCourseController(ITrainingCourseRepository trainingCourseRepository, IEmployeeRepository employeeRepository, ITrainingCourseTypeRepository trainingCourseTypeRepository, IMapper mapper)
        {
            _trainingCourseRepository = trainingCourseRepository;
            _employeeRepository = employeeRepository;
            _trainingCourseTypeRepository = trainingCourseTypeRepository;
            _mapper = mapper;
        }

        // GET: TrainingCourse
        public async Task<ActionResult> Index()
        {
            var employees = await _employeeRepository.FindAll();
            var trainingCourseTypes = await _trainingCourseTypeRepository.FindAll();
            var TrainingCourses = await _trainingCourseRepository.FindAll();
            var model = _mapper.Map<List<TrainingCourse>, List<TrainingCourseVM>>(TrainingCourses.ToList());
            foreach (var item in model)
            {
                var employee = employees.FirstOrDefault(q => q.Id == item.EmployeeId);
                var trainingCourseType = trainingCourseTypes.FirstOrDefault(q => q.Id == item.TrainingCourseTypeId);
                var employeeFulName = String.Format("{0} {1}", employee.Lastname, employee.Firstname);
                item.EmployeeFullName = employeeFulName;
                item.TrainingCourseTypeName = trainingCourseType.Name;
            }
            return View(model);
        }

        // GET: TrainingCourse/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _trainingCourseRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var employees = await _employeeRepository.FindAll();
            var trainingCourseTypes = await _trainingCourseTypeRepository.FindAll();
            var TrainingCourses = await _trainingCourseRepository.FindById(id);
            var model = _mapper.Map<TrainingCourseVM>(TrainingCourses);
            var employee = employees.FirstOrDefault(q => q.Id == model.EmployeeId);
            var trainingCourseType = trainingCourseTypes.FirstOrDefault(q => q.Id == model.TrainingCourseTypeId);
            var employeeFulName = String.Format("{0} {1}", employee.Lastname, employee.Firstname);

            model.TrainingCourseTypeName = trainingCourseType.Name;
            model.EmployeeFullName = employeeFulName;
            return View(model);
        }
        [Authorize(Roles = "Administrator, Employer, Agent")]
        // GET: TrainingCourse/Create
        public async Task<ActionResult> Create()
        {
            var model = new CreateTrainingCourseVM();
            var employees = await _employeeRepository.FindAll();
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                Value = q.Id.ToString()
            });
            var trainingCourseTypes = await _trainingCourseTypeRepository.FindAll();
            var trainingCourseTypesItems = trainingCourseTypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            model.Employees = employeesItems;
            model.TrainingCourseTypes = trainingCourseTypesItems;
            return View(model);
        }

        // POST: TrainingCourse/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTrainingCourseVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (model.DateOfTrainingCourse.Date > model.DateValidUntil.Date)
                {
                    var employees = await _employeeRepository.FindAll();
                    var employeesItems = employees.Select(q => new SelectListItem
                    {
                        Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                        Value = q.Id.ToString()
                    });
                    var trainingCourseTypes = await _trainingCourseTypeRepository.FindAll();
                    var trainingCourseTypesItems = trainingCourseTypes.Select(q => new SelectListItem
                    {
                        Text = q.Name,
                        Value = q.Id.ToString()
                    });

                    model.Employees = employeesItems;
                    model.TrainingCourseTypes = trainingCourseTypesItems;

                    ModelState.AddModelError("", "Podane daty są nieprawidłowe");
                    return View(model);
                }
                var TrainingCourse = _mapper.Map<TrainingCourse>(model);
                var isSuccess = await _trainingCourseRepository.Create(TrainingCourse);
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

        // GET: TrainingCourse/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var success = await _trainingCourseRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var TrainingCourse = await _trainingCourseRepository.FindById(id);
            var model = _mapper.Map<CreateTrainingCourseVM>(TrainingCourse);




            var employees = await _employeeRepository.FindAll();
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                Value = q.Id.ToString()
            });
            var trainingCourseTypes = await _trainingCourseTypeRepository.FindAll();
            var trainingCourseTypesItems = trainingCourseTypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            model.Employees = employeesItems;
            model.TrainingCourseTypes = trainingCourseTypesItems;

            return View(model);
        }

        // POST: TrainingCourse/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CreateTrainingCourseVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (model.DateOfTrainingCourse.Date > model.DateValidUntil.Date)
                {
                    var employees = await _employeeRepository.FindAll();
                    var employeesItems = employees.Select(q => new SelectListItem
                    {
                        Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                        Value = q.Id.ToString()
                    });
                    var trainingCourseTypes = await _trainingCourseTypeRepository.FindAll();
                    var trainingCourseTypesItems = trainingCourseTypes.Select(q => new SelectListItem
                    {
                        Text = q.Name,
                        Value = q.Id.ToString()
                    });

                    model.Employees = employeesItems;
                    model.TrainingCourseTypes = trainingCourseTypesItems;

                    ModelState.AddModelError("", "Podane daty są nieprawidłowe");
                    return View(model);
                }
                var TrainingCourse = _mapper.Map<TrainingCourse>(model);
                var isSuccess = await _trainingCourseRepository.Update(TrainingCourse);
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

        // GET: TrainingCourse/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var TrainingCourse = await _trainingCourseRepository.FindById(id);
            if (TrainingCourse == null)
            {
                return NotFound();
            }
            var isSuccess = await _trainingCourseRepository.Delete(TrainingCourse);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: TrainingCourse/Delete/5
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