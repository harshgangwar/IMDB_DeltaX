using System.Web.Mvc;
using DeltaX.EF.DeltaXService;

namespace DeltaX.Controllers
{
    public class HomeController : Controller
    {
        #region Private member variables

        private readonly DeltaXService _deltaXService;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize DeltaXService instance
        /// </summary>
        public HomeController()
        {
            _deltaXService = new DeltaXService();
        }

        #endregion

        public ActionResult Index()
        {
            var listOfMovies = _deltaXService.GetListOfMovies();
            return View(listOfMovies);
        }

        public JsonResult ShowGridData()
        {
            var listOfMovies = _deltaXService.GetListOfMovies();
            return Json(listOfMovies, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShowMovieData(int pkMovieId)
        {
            if (pkMovieId < 0)
            {
                return null;
            }
            var data = _deltaXService.GetMovieDetailsById(pkMovieId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "DeltaX IMDB consists the information of new released movies and actors";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Us:";

            return View();
        }
    }
}