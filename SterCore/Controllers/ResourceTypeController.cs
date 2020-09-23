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
    public class ResourceTypeController : Controller
    {
        private readonly IResourceTypeRepository _resourceTypeRepository;
        private readonly IMapper _mapper;
        public ResourceTypeController(IResourceTypeRepository resourceTypeRepository, IMapper mapper)
        {
            _resourceTypeRepository = resourceTypeRepository;
            _mapper = mapper;
        }

        // GET: resourceType
        public async Task<ActionResult> Index()
        {
            var resourceTypes = await _resourceTypeRepository.FindAll();
            var model = _mapper.Map<List<ResourceType>, List<ResourceTypeVM>>(resourceTypes.ToList());
            return View(model);
        }

        // GET: resourceType/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var success = await _resourceTypeRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var resourceTypes = await _resourceTypeRepository.FindById(id);
            var model = _mapper.Map<ResourceTypeVM>(resourceTypes);
            return View(model);
        }
        [Authorize(Roles = "Administrator, Employer, Agent")]
        // GET: resourceType/Create
        public async Task<ActionResult> Create()
        {
            var model = new ResourceTypeVM();
            return View(model);
        }

        // POST: resourceType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ResourceTypeVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var resourceType = _mapper.Map<ResourceType>(model);
                var isSuccess = await _resourceTypeRepository.Create(resourceType);
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

        // GET: resourceType/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var success = await _resourceTypeRepository.Exists(id);
            if (!success)
            {
                return NotFound();
            }
            var resourceType = await _resourceTypeRepository.FindById(id);
            var model = _mapper.Map<ResourceTypeVM>(resourceType);

            return View(model);
        }

        // POST: resourceType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ResourceTypeVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var resourceType = _mapper.Map<ResourceType>(model);
                var isSuccess = await _resourceTypeRepository.Update(resourceType);
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

        // GET: resourceType/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var resourceType = await _resourceTypeRepository.FindById(id);
            if (resourceType == null)
            {
                return NotFound();
            }
            var isSuccess = await _resourceTypeRepository.Delete(resourceType);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: resourceType/Delete/5
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