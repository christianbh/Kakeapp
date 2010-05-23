using System;
using System.Web.Mvc;
using Cake.Core.DataInterfaces;
using Cake.Core.Domain;
using Cake.DAL;
using Cake.UI.Models.Settings;

namespace Cake.UI.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentDao _departmentsDao;


        public DepartmentController()
        {
            _departmentsDao = new DepartmentDao(SiteSettings.RepositoryPath);
        }

        public DepartmentController(IDepartmentDao departmentsDao)
        {
            _departmentsDao = departmentsDao;
        }


        [HttpGet]
        public ActionResult List()
        {
            return View(_departmentsDao.GetAll());
        }


        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            Department department = _departmentsDao.Get(id);

            return View(department);
        }

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _departmentsDao.Add(department);
                    return RedirectToAction("List");
                }
                catch
                {
                    return View();
                }
            }
            return View(department);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (!ModelState.IsValid || department == null)
                return View(department);

            try
            {
                _departmentsDao.Add(department);
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            Department department = _departmentsDao.Get(id);

            return View(department);
        }

        [HttpPost]
        public ActionResult Delete(Department department)
        {
            try
            {
                _departmentsDao.Delete(department);
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }
    }
}