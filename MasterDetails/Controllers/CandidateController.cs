using MasterDetails.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasterDetails.Controllers
{
    public class CandidateController : Controller
    {
        public readonly CandidateDbContext context;
        public readonly IWebHostEnvironment environment;
        public CandidateController(CandidateDbContext _context, IWebHostEnvironment _environment )
        {
            this.context = _context;
            this.environment = _environment;
        }
        public IActionResult Index()
        {
            return View(context.candidates.Include(a=>a.Experiances).ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            Candidate candidate = new Candidate();
            candidate.Experiances.Add(new Experiance()
            {
                SkillName = "",
                SkillLevel="",
                SkillYear=  0
            });
            return View(candidate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Candidate candidate,string btn)
        {
            if (btn == "Add")
            {
                candidate.Experiances.Add(new Experiance());
            }
            if (btn == "Create")
            {
                if(candidate.Image!= null)
                {
                    var rootPath = this.environment.ContentRootPath;
                    var fileToSave = Path.Combine(rootPath, "wwwroot/Images", candidate.Image.FileName);
                    using (var fileStream = new FileStream(fileToSave, FileMode.Create))
                    {
                        candidate.Image.CopyToAsync(fileStream);
                    }
                    candidate.PicPath = "~/Images/" + candidate.Image.FileName;

                    context.candidates.Add(candidate);
                    if (context.SaveChanges() > 0)
                    {
                        return RedirectToAction("Index");
                    }  
                }
                else
                {
                    ModelState.AddModelError("", "Please Provide Valid Images!");
                    return View(candidate);
                }
            }
            else
            {
                var massage = string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                ModelState.AddModelError("", massage);
            }
            return View(candidate);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(context.candidates.Include(f=>f.Experiances).Where(f=>f.Id.Equals(id)).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(Candidate candidate, string btn) 
        {
            if (btn == "Add")
            {
                candidate.Experiances.Add(new Experiance());
            }
            if (btn == "Update")
            {
                var oldapplicant = context.candidates.Find(candidate.Id);
                if (candidate.Image != null)
                {
                    var rootPath = this.environment.ContentRootPath;
                    var fileToSave = Path.Combine(rootPath, "wwwroot/Images", candidate.Image.FileName);
                    using (var fileStream = new FileStream(fileToSave, FileMode.Create))
                    {
                        candidate.Image.CopyToAsync(fileStream);
                    }
                    candidate.PicPath = "~/Images/" + candidate.Image.FileName;
                }
                else
                {
                    oldapplicant.PicPath = oldapplicant.PicPath;
                }
                oldapplicant.Name = candidate.Name;
                oldapplicant.DateOfBirth= candidate.DateOfBirth;
                oldapplicant.Phone = candidate.Phone;
                oldapplicant.Email= candidate.Email;
                oldapplicant.Fresher= candidate.Fresher;
                context.experiances.RemoveRange(context.experiances.Where(s => s.Id == candidate.Id));
                context.SaveChanges();
                oldapplicant.Experiances = candidate.Experiances;
                context.Entry(oldapplicant).State = EntityState.Modified;
                if (context.SaveChanges() > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var massage = string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                ModelState.AddModelError("", massage);
            }
            return View(candidate);
        }
        public ActionResult Details(int id)
        {

            Candidate employee = context.candidates.Include(a => a.Experiances).FirstOrDefault(a => a.Id == id);



            return View(employee);
        }
        public ActionResult Delete(int id)
        {
            context.candidates.Remove(context.candidates.Find(id));
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
//Final