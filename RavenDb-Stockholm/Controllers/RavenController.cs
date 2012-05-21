using System.Threading;
using System.Web.Mvc;
using Raven.Client;
using Raven.Client.Document;

namespace RavenDb_Stockholm.Controllers
{
	public class RavenController : Controller
	{
		private static IDocumentStore _store;

		public static IDocumentStore Store
		{
			get
			{
				if(_store == null)
				{
					lock(typeof(RavenController))
					{
						Thread.MemoryBarrier();
						if(_store == null)
						{
							_store = CreateDocumentStore();
						}
					}
				}
				return _store;
			}
		}

		private static IDocumentStore CreateDocumentStore()
		{
			var documentStore = new DocumentStore
			{
				Url = "http://localhost:8080", 
				DefaultDatabase = "CornerStone"
			};
			documentStore.Initialize();
			return documentStore;
		}

		public new IDocumentSession Session { get; set; }

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Session = Store.OpenSession();
		}

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			using(Session)
			{
				if(Session != null && filterContext.Exception == null)
					Session.SaveChanges();
			}
		}

		protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			return base.Json(data, contentType, contentEncoding, JsonRequestBehavior.AllowGet);
		}
	}
}