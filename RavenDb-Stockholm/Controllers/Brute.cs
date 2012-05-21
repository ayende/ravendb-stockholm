using System;
using System.Runtime.Serialization.Formatters;
using System.Web.Mvc;
using RavenDb_Stockholm.Models;
using Raven.Client.Linq;

namespace RavenDb_Stockholm.Controllers
{
	public class BruteController : RavenController
	{
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