using System;
using System.Web.Mvc;
using RavenDb_Stockholm.Models;
using System.Linq;
using Raven.Client.Linq;

namespace RavenDb_Stockholm.Controllers
{
	public class CourseController : RavenCrudController<Course>
	{
		public override ActionResult Details(int id)
		{
			var course = Session
				.Include<Course>(x => x.Instructor)
				.Load<Course>(id);

			var instructor = Session.Load<Instructor>(id);

			Session.Advanced.GetMetadataFor(instructor)["User"] = "ayende";

			return Json(new
			{
				CourseName = course.Name,
				InstructorName = instructor.Name
			});
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
				Content = new string('*', 10),
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