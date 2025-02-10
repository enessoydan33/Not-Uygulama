using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotUyg.Data.Abstract;
using NotUyg.Data.Concrete.EfCore;
using NotUyg.Entity;
using NotUyg.Models;
using System.Security.Claims;


namespace NotUyg.Controllers
{
    public class NotController: Controller
    {
        readonly INotRepository _notRepository;
        readonly IUserRepository _userRepository;
        readonly ITagRepository _tagRepository;
        public NotController(   INotRepository notRepository, IUserRepository userRepository, ITagRepository tagRepository)
        {
            _notRepository = notRepository;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
        }
        public IActionResult Index()
        {
            var userıd = Convert.ToInt32( User.FindFirstValue(ClaimTypes.NameIdentifier));
   
            var model = _notRepository.Nots.Where(m => m.UserId == userıd).Include(m => m.Tags).ToList();

            return View(model);

        }

        public IActionResult Update(int id)
        {
            var model = _notRepository.Nots.FirstOrDefault(m => m.Id == id);
            
            var tags = _tagRepository.Tag.ToList();
            ViewBag.Tags = new SelectList(tags, "Id", "Name");
            
         NotUpdateData data =new NotUpdateData
            {
                Id = model.Id,
                Baslık = model.Baslık,
                acıklama = model.acıklama,
                Durum = model.Durum,
                

         };

            return View(data);
        }

        [HttpPost]

        public IActionResult Update(NotUpdateData model)
        {
            if (ModelState.IsValid)
            {
            var m= _notRepository.Nots.FirstOrDefault(m => m.Id == model.Id);
                var userıd = User.FindFirstValue(ClaimTypes.NameIdentifier);


                List<Tag> tags = new List<Tag>();
                foreach (var tagId in model.Tags)
                {
                    var tag = _tagRepository.Tag.FirstOrDefault(t => t.Id == tagId);
                    if (tag != null)
                    {
                        tags.Add(tag); // Tag ID'si ile ilişkilendirilen Tag nesnesi ekleniyor
                    }
                }

                _notRepository.UpdateNot(
                new Not
               {
                    Tags = tags,
                    Id = model.Id,
                    Baslık = model.Baslık,
                   acıklama = model.acıklama,
                   Durum = model.Durum,
                    Tarih = m.Tarih,
                    UserId = Convert.ToInt32(userıd)
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public IActionResult Delete(int id)
        {
            var m = _notRepository.Nots.FirstOrDefault(a=>a.Id==id);
           if(m != null)
            _notRepository.DeleteNot(m);

            if (m == null)
                return NotFound();


            return RedirectToAction("Index");
        }


        public IActionResult TagListele(int id)
        {
            var userıd = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var model = _notRepository.Nots.Where(m => m.Tags.Any(t => t.Id == id) && m.UserId ==userıd).ToList();
            return View(model);
        }





    }
}
