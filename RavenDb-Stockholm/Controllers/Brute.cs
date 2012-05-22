using System;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Runtime.Serialization.Formatters;
using System.Web.Mvc;
using Raven.Json.Linq;
using RavenDb_Stockholm.Indexes;
using RavenDb_Stockholm.Models;
using Raven.Client.Linq;
using System.Linq;

namespace RavenDb_Stockholm.Controllers
{
	public class BruteController : RavenController
	{
		public ActionResult Upload()
		{
			var downloadData = new WebClient().DownloadData("https://www.google.com/images/srpr/logo3w.png");	

			Store.DatabaseCommands.PutAttachment("logo.png", null, 
				new MemoryStream(downloadData), new RavenJObject
				{
					{"Content-Type", "image/png"}
				});
			return Content("OK");
		}

		public ActionResult Search(string name)
		{
			var results = Session.Query<People_Search.Result, People_Search>()
				.Search(x => x.Name, name)
				.OrderBy(x=>x.Name)
				.As<object>()
				.ToList();

			return Json(results);
		}

		 public ActionResult Force(string name)
		 {
		 	Session.Store(new Instructor
		 	{
		 		Name = name,
				Id = "instructors/1235"
		 	});
		 	return Content("OK");
		 }
	}
}