using System.Web.Mvc;
using Cake.Core.DataInterfaces;
using Cake.Core.Services;
using Cake.DAL;
using Cake.UI.Models.Forms;
using Cake.UI.Models.Settings;

namespace Cake.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICakeScheduleDao _cakeScheduleDao;
        private readonly ICakeScheduleServices _cakeScheduleService;
        private readonly IDepartmentDao _departmentDao;
        private readonly IDepartmentServices _departmentServices;

        public HomeController()
        {
            _departmentDao = new DepartmentDao(SiteSettings.RepositoryPath);
            _departmentServices = new DepartmentServices();
            _cakeScheduleDao = new CakeScheduleDao(SiteSettings.RepositoryPath);
            _cakeScheduleService = new CakeScheduleServices();
        }

        public HomeController(IDepartmentDao departmentDao, IDepartmentServices departmentServices,
                              ICakeScheduleDao cakeScheduleDao, ICakeScheduleServices cakeScheduleServices)
        {
            _departmentDao = departmentDao;
            _departmentServices = departmentServices;
            _cakeScheduleDao = cakeScheduleDao;
            _cakeScheduleService = cakeScheduleServices;
        }


        public ActionResult Index()
        {
            ViewData.Model = new StartPageForm(_departmentDao, _departmentServices, _cakeScheduleDao,
                                               _cakeScheduleService);

            return View();
        }
    }
}