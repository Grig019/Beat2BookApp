

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Beat2Book.Models;
using Beat2Book.Data;
using Beat2Book.Interfaces;
using Beat2Book.Models;
using Beat2Book.Repository;
using Beat2Book.Services;
using Beat2Book.homeViewModels;

namespace Beat2Book.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpsContextAccessor;

        public EventController(IEventRepository raceRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _eventRepository = raceRepository;
            _photoService = photoService;
            _httpsContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Event> races = await _eventRepository.GetAll();
            return View(races);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Event race = await _eventRepository.GetByIdAsync(id);
            return View(race);
        }

        public IActionResult Create()
        {
            var curUserId = _httpsContextAccessor.HttpContext?.User.GetUserId();
            var createRaceViewModel = new CreateEventViewModel { AppUserId = curUserId };
            return View(createRaceViewModel);
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateEventViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);

                var race = new Event
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = raceVM.AppUserId,
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State,
                        ZipCode = raceVM.Address.ZipCode,

                    }
                };
                _eventRepository.Add(race);
                return RedirectToAction("Index");
            }

            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(raceVM);


        }

        public async Task<IActionResult> Edit(int id)
        {
            var race = await _eventRepository.GetByIdAsync(id);
            if (race == null) return View("Error");
            var clubVM = new EditEventViewModel
            {
                Title = race.Title,
                Description = race.Description,
                AddressId = race.AddressId,
                Address = race.Address,
                URL = race.Image,
                EventCategory = race.EventCategory
            };
            return View(clubVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditEventViewModel raceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", raceVM);
            }

            var userClub = await _eventRepository.GetByIdAsyncNoTracking(id);

            if (userClub != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userClub.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(raceVM);
                }

                var photoResult = await _photoService.AddPhotoAsync(raceVM.Image);

                var race = new Event
                {
                    Id = id,
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = raceVM.AddressId,
                    Address = raceVM.Address,
                };

                _eventRepository.Update(race);

                return RedirectToAction("Index");
            }
            else
            {
                return View(raceVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _eventRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var raceDetails = await _eventRepository.GetByIdAsync(id);

            if (raceDetails == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(raceDetails.Image))
            {
                _ = _photoService.DeletePhotoAsync(raceDetails.Image);
            }

            _eventRepository.Delete(raceDetails);
            return RedirectToAction("Index");
        }

    }

}
