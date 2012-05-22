using System;
using System.Web.Mvc;
using RavenDb_Stockholm.Indexes;
using RavenDb_Stockholm.Models;
using System.Linq;
using Raven.Client.Linq;

namespace RavenDb_Stockholm.Controllers
{
	public class CourseController : RavenCrudController<Course>
	{

		public ActionResult Search(string q)
		{
			var results = Session.Query<Courses_Search2.Result, Courses_Search2>()
				.Search(x=>x.Query, q)
				.As<Course>()
				.ToList();

			return Json(results);
		}

		public ActionResult ByInstructor()
		{
			var results = Session.Query<Courses_ByInstructor.Result, Courses_ByInstructor>()
				.ToList();
			return Json(results);
		}

		public ActionResult Tech(string name)
		{
			var results = Session.Query<Course>()
				.Where(x => x.Technologies.Any(t => t == name))
				.ToList();

			return Json(results);
		}

		public ActionResult Create(string name, string instructor, string[] tech)
		{
			var course = new Course  
			{
				Name = name,
				Content = new string('*', 11),
				Date = DateTime.Today,
				Instructor = instructor,
				Location = "Stockholm",
				Technologies = tech
			};
			Session.Store(course);

			var documentId = Session.Advanced.GetDocumentId(course);
			return RedirectToAction("Details", new { id = documentId.Split('/').Last() });

		}
	}
}