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
    public class CompetenceTypeController : Controller
    {
        private readonly ICompetenceTypeRepository _competenceTypeRepository;
        private readonly IMapper _mapper;
        public CompetenceTypeController(ICompetenceTypeRepository competenceTypeRepository, IMapper mapper)
        {
            _competenceTypeRepository = competenceTypeRepository;
            _mapper = mapper;
        }

        // GET: CompetenceType
        public async Task<ActionResult> Index()
        {
            var competences = await _competenceTypeRepository.FindAll();
            var model = _mapper.Map<List<CompetenceType>, List<CompetenceTypeVM>>(competences.ToList());
            return View(model);
        }

        // GET: CompetenceType/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _competenceTypeRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var comptenceType = await _competenceTypeRepository.FindById(id);
            var model = _mapper.Map<CompetenceTypeVM>(comptenceType);
            return View(model);
        }

        // GET: CompetenceType/Create
        public async Task<ActionResult> Create()
        {
            var model = new CompetenceTypeVM();
            return View(model);
        }

        // POST: CompetenceType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CompetenceTypeVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var competenceType = _mapper.Map<CompetenceType>(model);
                var isSuccess = await _competenceTypeRepository.Create(competenceType);
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

        // GET: CompetenceType/Edit/5
        public async Task<ActionResult> Edit(int id, string organizationToken)
        {
            var success = await _competenceTypeRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var competenceType = await _competenceTypeRepository.FindById(id);
            var model = _mapper.Map<CompetenceTypeVM>(competenceType);
            model.OrganizationToken = organizationToken;
            return View(model);
        }

        // POST: CompetenceType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CompetenceTypeVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var competenceType = _mapper.Map<CompetenceType>(model);
                var isSuccess = await _competenceTypeRepository.Update(competenceType);
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

        // GET: CompetenceType/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var competenceType = await _competenceTypeRepository.FindById(id);
            if (competenceType == null)
            {
                return NotFound();
            }
            var isSuccess = await _competenceTypeRepository.Delete(competenceType);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: CompetenceType/Delete/5
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