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
    public class ContractController : Controller
    {
        private readonly IContractRepository _contractRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IContractTypeRepository _contractTypeRepository;
        private readonly IMapper _mapper;
        public ContractController(IContractRepository contractRepository, IEmployeeRepository employeeRepository, IContractTypeRepository contractTypeRepository, IMapper mapper)
        {
            _contractRepository = contractRepository;
            _employeeRepository = employeeRepository;
            _contractTypeRepository = contractTypeRepository;
            _mapper = mapper;
        }

        // GET: Contract
        public async Task<ActionResult> Index()
        {
            var employees = await _employeeRepository.FindAll();
            var contractTypes = await _contractTypeRepository.FindAll();
            var Contracts = await _contractRepository.FindAll();
            var model = _mapper.Map<List<Contract>, List<ContractVM>>(Contracts.ToList());
            foreach (var item in model)
            {
                var employee = employees.FirstOrDefault(q => q.Id == item.EmployeeId);
                var contractType = contractTypes.FirstOrDefault(q => q.Id == item.ContractTypeId);
                var employeeFulName = String.Format("{0} {1}", employee.Lastname, employee.Firstname);
                item.EmployeeFullName = employeeFulName;
                item.ContractTypeName = contractType.Name;
            }
            return View(model);
        }

        // GET: Contract/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _contractRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var employees = await _employeeRepository.FindAll();
            var contractTypes = await _contractTypeRepository.FindAll();
            var Contracts = await _contractRepository.FindById(id);
            var model = _mapper.Map<ContractVM>(Contracts);
            var employee = employees.FirstOrDefault(q => q.Id == model.EmployeeId);
            var contractType = contractTypes.FirstOrDefault(q => q.Id == model.ContractTypeId);
            var employeeFulName = String.Format("{0} {1}", employee.Lastname, employee.Firstname);

            model.ContractTypeName = contractType.Name;
            model.EmployeeFullName = employeeFulName;
            return View(model);
        }

        // GET: Contract/Create
        public async Task<ActionResult> Create()
        {
            var model = new CreateContractVM();
            var employees = await _employeeRepository.FindAll();
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                Value = q.Id.ToString()
            });
            var contractTypes = await _contractTypeRepository.FindAll();
            var contractTypesItems = contractTypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            model.Employees = employeesItems;
            model.ContractTypes = contractTypesItems;
            return View(model);
        }

        // POST: Contract/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateContractVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (model.DateValidFrom.Date > model.DateValidUntil.Date)
                {
                    var employees = await _employeeRepository.FindAll();
                    var employeesItems = employees.Select(q => new SelectListItem
                    {
                        Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                        Value = q.Id.ToString()
                    });
                    var contractTypes = await _contractTypeRepository.FindAll();
                    var contractTypesItems = contractTypes.Select(q => new SelectListItem
                    {
                        Text = q.Name,
                        Value = q.Id.ToString()
                    });

                    model.Employees = employeesItems;
                    model.ContractTypes = contractTypesItems;

                    ModelState.AddModelError("", "Podane daty są nieprawidłowe");
                    return View(model);
                }
                var Contract = _mapper.Map<Contract>(model);
                var isSuccess = await _contractRepository.Create(Contract);
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

        // GET: Contract/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var success = await _contractRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var Contract = await _contractRepository.FindById(id);
            var model = _mapper.Map<CreateContractVM>(Contract);




            var employees = await _employeeRepository.FindAll();
            var employeesItems = employees.Select(q => new SelectListItem
            {
                Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                Value = q.Id.ToString()
            });
            var contractTypes = await _contractTypeRepository.FindAll();
            var contractTypesItems = contractTypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            model.Employees = employeesItems;
            model.ContractTypes = contractTypesItems;

            return View(model);
        }

        // POST: Contract/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CreateContractVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (model.DateValidFrom.Date > model.DateValidUntil.Date)
                {
                    var employees = await _employeeRepository.FindAll();
                    var employeesItems = employees.Select(q => new SelectListItem
                    {
                        Text = String.Format("{0} {1}", q.Firstname, q.Lastname),
                        Value = q.Id.ToString()
                    });
                    var contractTypes = await _contractTypeRepository.FindAll();
                    var contractTypesItems = contractTypes.Select(q => new SelectListItem
                    {
                        Text = q.Name,
                        Value = q.Id.ToString()
                    });

                    model.Employees = employeesItems;
                    model.ContractTypes = contractTypesItems;

                    ModelState.AddModelError("", "Podane daty są nieprawidłowe");
                    return View(model);
                }
                var Contract = _mapper.Map<Contract>(model);
                var isSuccess = await _contractRepository.Update(Contract);
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

        // GET: Contract/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var Contract = await _contractRepository.FindById(id);
            if (Contract == null)
            {
                return NotFound();
            }
            var isSuccess = await _contractRepository.Delete(Contract);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Contract/Delete/5
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