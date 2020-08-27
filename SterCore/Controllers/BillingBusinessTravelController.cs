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
    public class BillingBusinessTravelController : Controller
    {
        private readonly IBillingBusinessTravelRepository _billingBusinessTravelRepo;
        private readonly IBusinessTravelRepository _businessTravelRepo;
        private readonly ITypeOfBillingRepository _typeOfBillingRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;


        public BillingBusinessTravelController(IBillingBusinessTravelRepository repo, 
            IBusinessTravelRepository businessTravelRepo,
            ITypeOfBillingRepository typeOfBillingRepository,
            ICurrencyRepository currencyRepository,
            IMapper mapper
            )
        { 
            _billingBusinessTravelRepo = repo;
            _businessTravelRepo = businessTravelRepo;
            _typeOfBillingRepository = typeOfBillingRepository;
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }
        // GET: BillingBusinessTravel
        public async Task<ActionResult> Index()
        {
            var billingBusinessTravels = await _billingBusinessTravelRepo.FindAll();
            var currencies = _currencyRepository.FindAll().Result;
            var typeOfBillings = _typeOfBillingRepository.FindAll().Result;
            var businessTravels = _businessTravelRepo.FindAll().Result;
            
            var model = _mapper.Map<List<BillingBusinessTravel>, List<BillingBusinessTravelVM>>(billingBusinessTravels.ToList());
            foreach (var item in model)
            {
                item.ApplicationId = businessTravels.FirstOrDefault(q => q.Id == item.BusinessTravelId).ApplicationId;
                item.CurrencyName = currencies.FirstOrDefault(q => q.Id==item.CurrencyId).Name;
                item.TypeOfBillingName = typeOfBillings.FirstOrDefault(q => q.Id==item.TypeOfBillingId).Name;
            }
            return View(model);
        }

        // GET: BillingBusinessTravel/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _billingBusinessTravelRepo.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var BillingBusinessTravel = await _billingBusinessTravelRepo.FindById(id);
            var businessTravels = _businessTravelRepo.FindAll().Result;
            var currencies = _currencyRepository.FindAll().Result;
            var typeOfBillings = _typeOfBillingRepository.FindAll().Result;

            var model = _mapper.Map<BillingBusinessTravelVM>(BillingBusinessTravel);
            model.CurrencyName= currencies.FirstOrDefault(q=>q.Id == model.CurrencyId).Name;
            model.TypeOfBillingName= typeOfBillings.FirstOrDefault(q => q.Id == model.TypeOfBillingId).Name;
            model.ApplicationId = businessTravels.FirstOrDefault(q => q.Id == model.BusinessTravelId).ApplicationId;
            return View(model);
        }

        // GET: BillingBusinessTravel/Create
        public async Task<ActionResult> Create()
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
            var typeOfBillings = _typeOfBillingRepository.FindAll().Result;
            var typeOfBillingsItems = typeOfBillings.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });


            var model = new CreateBillingBusinessTravelVM
            {
                BusinessTravels = businessTravelsItems,
                Curencies = currenciesItems,
                TypeOfBillings = typeOfBillingsItems
                
            };

            return View(model);
        }

        // POST: BillingBusinessTravel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBillingBusinessTravelVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var billingBusinessTravel = _mapper.Map<BillingBusinessTravel>(model);
                var isSuccess = await _billingBusinessTravelRepo.Create(billingBusinessTravel);
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

        // GET: BillingBusinessTravel/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var success = await _billingBusinessTravelRepo.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var BillingBusinessTravel = await _billingBusinessTravelRepo.FindById(id);
            var model = _mapper.Map<CreateBillingBusinessTravelVM>(BillingBusinessTravel);
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
            var typeOfBillings = _typeOfBillingRepository.FindAll().Result;
            var typeOfBillingsItems = typeOfBillings.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            model.BusinessTravels = businessTravelsItems;
            model.Curencies = currenciesItems;
            model.TypeOfBillings = typeOfBillingsItems;
            return View(model);
        }

        // POST: BillingBusinessTravel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CreateBillingBusinessTravelVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var BillingBusinessTravel = _mapper.Map<BillingBusinessTravel>(model);
                var isSuccess = await _billingBusinessTravelRepo.Update(BillingBusinessTravel);
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

        // GET: BillingBusinessTravel/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var businessTravel = await _billingBusinessTravelRepo.FindById(id);
            if (businessTravel == null)
            {
                return NotFound();
            }
            var isSuccess = await _billingBusinessTravelRepo.Delete(businessTravel);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: BillingBusinessTravel/Delete/5
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