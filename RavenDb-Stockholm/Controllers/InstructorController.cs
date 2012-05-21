using System.Linq;
using System.Web.Mvc;
using Raven.Abstractions.Data;
using Raven.Client.Linq;
using RavenDb_Stockholm.Models;

namespace RavenDb_Stockholm.Controllers
{
	public class InstructorController : RavenCrudController<Instructor>
    {
		public ActionResult CoursesBy(string name)
		{
			var query = Session.Query<Instructor>("Instructors/ByName")
				.Search(x => x.Name, name);

			var instructor = query.FirstOrDefault();
			if (instructor == null)
			{
				var suggestionQueryResult = query.Suggest();

				switch (suggestionQueryResult.Suggestions.Length)
				{
					case 0:
						return Json(new
						{
							Error = "No instructor named " + name
						});
					case 1:
						return CoursesBy(suggestionQueryResult.Suggestions[0]);
					default:
						return Json(new
						{
							suggestionQueryResult.Suggestions
						});
				}
			}
			return Json(new
			{
				Instructor = instructor,
				Courses = Session.Query<Course>()
			            	.Where(x => x.Instructor == instructor.Id)
			            	.ToList()
			});
		}

		public ActionResult Create(string name)
        {
        	var instructor = new Instructor
        	{
        		Name = name,
        	};
        	Session.Store(instructor);

        	var documentId = Session.Advanced.GetDocumentId(instructor);
        	return RedirectToAction("Details", new { id = documentId.Split('/').Last()});
        }
    }
}
