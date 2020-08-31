using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace leave_management.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IBusinessTravelRepository _businessTravelRepo;
        private readonly ITypeOfBillingRepository _typeOfBillingRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;


        public ExpenseController(IExpenseRepository expenseRepository,
            IBusinessTravelRepository businessTravelRepo,
            ITypeOfBillingRepository typeOfBillingRepository,
            ICurrencyRepository currencyRepository,
            IMapper mapper
            )
        {
            _expenseRepository = expenseRepository;
            _businessTravelRepo = businessTravelRepo;
            _currencyRepository = currencyRepository;
            _typeOfBillingRepository = typeOfBillingRepository;
            _mapper = mapper;
        }
        // GET: Expense
        public async Task<ActionResult> Index()
        {
            var expenses = await _expenseRepository.FindAll();
            var currencies = await _currencyRepository.FindAll();
            var businessTravels = await _businessTravelRepo.FindAll();

            var model = _mapper.Map<List<Expense>, List<ExpenseVM>>(expenses.ToList());
            foreach (var item in model)
            {
                item.ApplicationId = businessTravels.FirstOrDefault(q => q.Id == item.BusinessTravelId).ApplicationId;
                item.CurrencyName = currencies.FirstOrDefault(q => q.Id == item.CurrencyId).Name;
            }
            return View(model);
        }

        // GET: Expense/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _expenseRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var expense = await _expenseRepository.FindById(id);
            var businessTravels = _businessTravelRepo.FindAll().Result;
            var currencies = _currencyRepository.FindAll().Result;

            var model = _mapper.Map<ExpenseVM>(expense);
            model.CurrencyName = currencies.FirstOrDefault(q => q.Id == model.CurrencyId).Name;
            model.ApplicationId = businessTravels.FirstOrDefault(q => q.Id == model.BusinessTravelId).ApplicationId;
            return View(model);
        }

        // GET: Expense/Create
        public async Task<ActionResult> Create(int businessTravelId)
        {
            var businessTravels = _businessTravelRepo.FindAll().Result;
            var businessTravelsItems = businessTravels.Select(q => new SelectListItem
            {
                Text = q.ApplicationId.ToString(),
                Value = q.Id.ToString()
            });

            var currencies = _currencyRepository.FindAll().Result;
            var currenciesItems = currencies.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            var model = new CreateExpenseVM
            {
                BusinessTravels = businessTravelsItems,
                Curencies = currenciesItems,
            };
            if (await _typeOfBillingRepository.Exists(businessTravelId))
            {
                model.BusinessTravelId = businessTravelId;
            }

            return View(model);
        }

        // POST: Expense/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateExpenseVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var expense = _mapper.Map<Expense>(model);
                var isSuccess = await _expenseRepository.Create(expense);
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
                return View(model);
            }
        }

        // GET: Expense/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var success = await _expenseRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var BillingBusinessTravel = await _expenseRepository.FindById(id);
            var model = _mapper.Map<CreateExpenseVM>(BillingBusinessTravel);
            var businessTravels = _businessTravelRepo.FindAll().Result;
            var businessTravelsItems = businessTravels.Select(q => new SelectListItem
            {
                Text = q.ApplicationId.ToString(),
                Value = q.Id.ToString()
            });

            var currencies = _currencyRepository.FindAll().Result;
            var currenciesItems = currencies.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            model.BusinessTravels = businessTravelsItems;
            model.Curencies = currenciesItems;
            return View(model);
        }

        // POST: Expense/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CreateExpenseVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var expense = _mapper.Map<Expense>(model);
                var isSuccess = await _expenseRepository.Update(expense);
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
                return View(model);
            }
        }

        // GET: Expense/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var expense = await _expenseRepository.FindById(id);
            if (expense == null)
            {
                return NotFound();
            }
            var isSuccess = await _expenseRepository.Delete(expense);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Expense/Delete/5
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