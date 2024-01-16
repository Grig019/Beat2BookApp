using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using Beat2Book.Helpers;
using Beat2Book.Interfaces;
using Beat2Book.Models;
using Beat2Book.homeViewModels;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Authorization;
using Beat2Book.HomeViewModels;

namespace Beat2Book.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBandRepository _clubRepository;

        public HomeController(ILogger<HomeController> logger, IBandRepository clubRepository) 
        {
            _logger = logger; 
            _clubRepository = clubRepository;   
        }

        public async Task<IActionResult> Index()
        {
            var ipInfo = new IPInfo();
            var homeViewModel = new HomeViewModel();  
            try
            {
                string url = "http://ipinfo.io?token=fcfa82c45d044b";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
                homeViewModel.City = ipInfo.City;
                homeViewModel.State = ipInfo.Region; 
                if(homeViewModel.City != null)
                {
                    homeViewModel.Clubs = await _clubRepository.GetClubByCity(homeViewModel.City); 
                } 
                else
                {
                    homeViewModel.Clubs = null; 
                }
                return View(homeViewModel); 
            } 
            catch (Exception ex) 
            {
                homeViewModel.Clubs = null;
            }
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
        public IActionResult Chat()
        {
            return View();
        }
    }
}