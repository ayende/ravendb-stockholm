using System.Linq;
using System.Web.Mvc;

namespace RavenDb_Stockholm.Controllers
{
	public class RavenCrudController<T> : RavenController
	{
		public ActionResult Index()
		{
			return Json(Session.Query<T>().ToList());
		}

		public virtual ActionResult Details(int id)
		{
			return Json(Session.Load<T>(id));
		}
	}
}