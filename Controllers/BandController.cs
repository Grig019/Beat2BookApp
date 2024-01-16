using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Beat2Book.Models;
using Beat2Book.Data;
using Beat2Book.Interfaces;
using Beat2Book.Models;
using Beat2Book.homeViewModels;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Authorization;
using Beat2Book.ViewModels;

namespace Beat2Book.Controllers
{
    public class BandController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBandRepository _clubRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor; 

        public BandController(IBandRepository clubRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {

            _clubRepository = clubRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor; 
        }
        public async Task<IActionResult> Index()
        {

            IEnumerable<Band> bands = await _clubRepository.GetAll();

            return View(bands);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Band club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }

        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createClubViewModel = new CreateBandViewModel {AppUserId = curUserId};
            return View(createClubViewModel); 
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateBandViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);

                var club = new Band
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = clubVM.AppUserId, 
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State,
                        ZipCode = clubVM.Address.ZipCode,

                    }
                };
                _clubRepository.Add(club);
                return RedirectToAction("Index");
            }

            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(clubVM);

        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            if (club == null) return View("Error");
            var clubVM = new EditBandViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId ?? 0,
                Address = club.Address,
                URL = club.Image,
                ClubCategory = club.BandCategory
            };
            return View(clubVM);
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBandViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", clubVM);
            }

            var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);
            {
                if (userClub != null)
                {
                    try
                    {
                        await _photoService.DeletePhotoAsync(userClub.Image);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Could not delete photo");
                        return View(clubVM);
                    }

                    var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);

                    var club = new Band
                    {
                        Id = id,
                        Title = clubVM.Title,
                        Description = clubVM.Description,
                        Image = photoResult.Url.ToString(),
                        AddressId = clubVM.AddressId,
                        Address = clubVM.Address,

                    };

                    _clubRepository.Update(club);

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(clubVM);
                }


            }

        }

        [Authorize(Roles = "admin")]
        [HttpGet] 
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error"); 
            return View(clubDetails); 
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);

            if (clubDetails == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(clubDetails.Image))
            {
                _ = _photoService.DeletePhotoAsync(clubDetails.Image);
            }

            _clubRepository.Delete(clubDetails);
            return RedirectToAction("Index");
        }

    }

}
