using System;
using System.Web.Mvc;
using Cake.Core.DataInterfaces;
using Cake.Core.Domain;
using Cake.DAL;
using Cake.UI.Models.Forms;
using Cake.UI.Models.Settings;

namespace Cake.UI.Controllers
{
    public class CakeScheduleController : Controller
    {
        private readonly CakeSchedule _cakeSchedule;
        private readonly ICakeScheduleDao _cakeScheduleDao;


        public CakeScheduleController()
        {
            _cakeScheduleDao = new CakeScheduleDao(SiteSettings.RepositoryPath);
            _cakeSchedule = _cakeScheduleDao.Get();
        }

        public CakeScheduleController(ICakeScheduleDao cakeScheduleDao)
        {
            _cakeScheduleDao = cakeScheduleDao;
            _cakeSchedule = _cakeScheduleDao.Get();
        }


        public ActionResult Index()
        {
            CakeSchedule cakeSchedule = _cakeScheduleDao.Get();
            return View(cakeSchedule);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            HolidayEditForm holidayEntryForm = GetHolidayEditFormFromId(id);
            if (holidayEntryForm == null)
                return RedirectToAction("Index");

            return View(holidayEntryForm);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            DateTime newDate, existingDate;

            if (DateTime.TryParse(collection.Get("NewDate"), out newDate) == false)
                return View();
            if (DateTime.TryParse(collection.Get("ExistingDate"), out existingDate) == false)
                return View();

            if (_cakeSchedule.EditHoliday(existingDate, newDate) == false)
                return View(new HolidayEditForm(existingDate));

            _cakeScheduleDao.Save(_cakeSchedule);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View(DateTime.Now);
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            DateTime newDate;

            if (DateTime.TryParse(collection.Get("newDate"), out newDate) == false)
                return View();

            if (_cakeSchedule.AddHoliday(newDate) == false)
                return View();

            _cakeScheduleDao.Save(_cakeSchedule);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(string id)
        {
            HolidayEditForm holidayEntryForm = GetHolidayEditFormFromId(id);

            if (holidayEntryForm == null)
                return RedirectToAction("Index");

            return View(holidayEntryForm);
        }

        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            DateTime existingDate;

            if (DateTime.TryParse(collection.Get("ExistingDate"), out existingDate) == false)
                return View();

            if (_cakeSchedule.DeleteHoliday(existingDate) == false)
                return View();

            _cakeScheduleDao.Save(_cakeSchedule);

            return RedirectToAction("Index");
        }


        private HolidayEditForm GetHolidayEditFormFromId(string id)
        {
            DateTime entryDate;
            if (id == null || DateTime.TryParse(id, out entryDate) == false)
                return null;

            CakeSchedule cakeSchedule = _cakeScheduleDao.Get();

            if (cakeSchedule.IsValidHolidayDate(entryDate) == false)
                return null;

            return new HolidayEditForm(entryDate);
        }
    }
}