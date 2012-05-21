using System;
using System.Web.Mvc;
using RavenDb_Stockholm.Models;
using System.Linq;

namespace RavenDb_Stockholm.Controllers
{
	public class CourseController : RavenCrudController<Course>
	{
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