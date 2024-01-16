using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Beat2Book.Data;
using Beat2Book.Models;
using Beat2Book.homeViewModels;

namespace Beat2Book.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context; 
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet] 
        public IActionResult Login()
        {
            var response = new LoginViewModel(); 
            return View(response);
        }

        [HttpPost] 

        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {  
                // User is found, check password 
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password); 
                if(passwordCheck)
                {  
                    // Password correct, sign in 
                    var results = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false); 
                    if (results.Succeeded)
                    {
                        return RedirectToAction("Index", "Event");
                    } 
                } 

                // Password is incorrect 

                TempData["Error"] = "Wrong credentials. Please, try again"; 
                return View(loginViewModel); 
            }
            // User Not Found
            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginViewModel);    

           
        } 

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response); 
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }

            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress,
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                // Add the user to the 'User' role
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

                // Sign in the user after registration
                await _signInManager.SignInAsync(newUser, isPersistent: false);

                return RedirectToAction("Index", "Event");
            }
            else
            {
                // If user creation failed, add errors to ModelState
                foreach (var error in newUserResponse.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(registerViewModel);
            }
        }


        [HttpPost] 
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();  
            return RedirectToAction("Index", "Event");   
        }


    }
}
