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

namespace leave_management.Controllers
{
    public class MedicalCheckUpController : Controller
    {
        private readonly ITypeOfMedicalCheckUpRepository _typeOfMedicalCheckUpRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;


        public MedicalCheckUpController(ITypeOfMedicalCheckUpRepository typeOfMedicalCheckUpRepo,
            IEmployeeRepository employeeRepo,
            IMapper mapper
            )
        {
            _typeOfMedicalCheckUpRepo = typeOfMedicalCheckUpRepo;
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }
        // GET: MedicalCheckUp
        public ActionResult Index()
        {
            return View();
        }

        // GET: MedicalCheckUp/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MedicalCheckUp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicalCheckUp/Create
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

        // GET: MedicalCheckUp/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MedicalCheckUp/Edit/5
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

        // GET: MedicalCheckUp/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MedicalCheckUp/Delete/5
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