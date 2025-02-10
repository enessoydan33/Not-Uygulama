using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using NotUyg.Data.Abstract;
using NotUyg.Data.Concrete.EfCore;
using NotUyg.Entity;
using NotUyg.Models;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Claims;

namespace NotUyg.Controllers
{
    public class UserController:Controller
    {
        private IUserRepository _userRepository;
        private INotRepository _notRepository;
        private ITagRepository _tagRepository;
        public UserController(IUserRepository userRepository, INotRepository notRepository,ITagRepository tagRepository)
        {
            _userRepository = userRepository;
            _notRepository = notRepository;
            _tagRepository=tagRepository;
        }
        public IActionResult Index() { 
            
            return View(); 
        
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginData model)
        {

            if (ModelState.IsValid)
            {
                var m = _userRepository.User.FirstOrDefault(m => m.Email == model.Email && m.Password == model.Password);
                if (m == null)
                {
                    ModelState.AddModelError("", "Mail veya şifre yanlış");
                    return View(model);
                }
                else
                {
                    var userclaims = new List<Claim>();
                     
                    userclaims.Add(new Claim(ClaimTypes.Name, m.Isim));
                    userclaims.Add(new Claim(ClaimTypes.NameIdentifier ,m.Id.ToString()));
                    userclaims.Add(new Claim (ClaimTypes.Email,m.Email));

                    var claimsIdentity = new ClaimsIdentity(userclaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authpro = new AuthenticationProperties { IsPersistent = true };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),
                     authpro );

                    return RedirectToAction("Index", "Home");
                }

            }
            return View();

        }



        public IActionResult Kayit()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Kayit(KayitData model)
        {
            if (ModelState.IsValid)
            {
               var a=  _userRepository.User.FirstOrDefault(m => m.Email == model.Email);
                if (a == null)
                {
                   await _userRepository.create(new User
                    {
                        Email = model.Email,
                        Password = model.Password,
                        Isim = model.Isim,
                        Soyad = model.Soyad,
                    });
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Bu mail daha önceden kullanılıyor.");
                   return View(model);
            
                }
               
            }
                   return View(model);


        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Not() 
        { var tags = _tagRepository.Tag.ToList();
            ViewBag.Tags = new SelectList(tags, "Id", "Name");
            return View(); 
        
        }
        [HttpPost]
        public IActionResult Not( NotData model)
        {
         var userıd = User.FindFirstValue( ClaimTypes.NameIdentifier);
       
            List<Tag> tags = new List<Tag>();
            foreach (var tagId in model.Tags)
            {
                var tag = _tagRepository.Tag.FirstOrDefault(t => t.Id == tagId);
                if (tag != null)
                {
                    tags.Add(tag); // Tag ID'si ile ilişkilendirilen Tag nesnesi ekleniyor
                }
            }

            if (ModelState.IsValid)
            {
                _notRepository.Create(new Not
                {
                  Tarih = DateTime.Now,
                    Durum = false,
                    Baslık = model.Baslık,
                    acıklama = model.acıklama,
                    UserId = Convert.ToInt32(userıd),
                    Tags = tags
                });
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        

    }
}
