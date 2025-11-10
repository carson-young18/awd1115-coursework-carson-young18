using Microsoft.AspNetCore.Mvc;
using RankenClassSchedule.Models.DataLayer;
using RankenClassSchedule.Models.DomainModels;

namespace RankenClassSchedule.Controllers
{
    public class ClassController : Controller
    {
        private Repository<Class> classes { get; set; }
        private Repository<Day> days { get; set; }
        private Repository<Teacher> teachers { get; set; }
        public ClassController(ClassScheduleContext ctx)
        {
            classes = new Repository<Class>(ctx);
            days = new Repository<Day>(ctx);
            teachers = new Repository<Teacher>(ctx);
        }

        public RedirectToActionResult Index() => RedirectToAction("Index", "Home");

        [HttpGet]
        public ViewResult Add()
        {
            this.LoadViewBag("Add");
            return View("AddEdit", new Class());
        }

        [HttpPost]
        public IActionResult Add(Class cls)
        {
            bool isAdd = cls.ClassId == 0;

            if (ModelState.IsValid)
            {
                if (isAdd)
                    classes.Insert(cls);
                else
                    classes.Update(cls);
                classes.Save();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string operation = isAdd ? "Add" : "Edit";
                this.LoadViewBag(operation);
                return View("AddEdit", cls);
            }
        }

        public ViewResult Edit(int id)
        {
            this.LoadViewBag("Edit");
            var cls = GetClass(id);
            return View("AddEdit", cls);
        }

        public ViewResult Delete(int id)
        {
            var cls = GetClass(id);
            return View(cls);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Class cls)
        {
            classes.Delete(cls);
            classes.Save();
            return RedirectToAction("Index", "Home");
        }

        private Class GetClass(int id)
        {
            var classOptions = new QueryOptions<Class>
            {
                Includes = "Teacher,Day",
                Where = c => c.ClassId == id
            };
            return classes.Get(classOptions) ?? new Class(); 
        }

        private void LoadViewBag(string operation)
        {
            ViewBag.Days = days.List(new QueryOptions<Day>
            {
                OrderBy = d => d.DayId
            });
            ViewBag.Teachers = teachers.List(new QueryOptions<Teacher>
            {
                OrderBy = t => t.LastName
            });
            ViewBag.Operation = operation;
        }
    }
}
