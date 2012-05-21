using System.Linq;
using System.Web.Mvc;
using RavenDb_Stockholm.Models;

namespace RavenDb_Stockholm.Controllers
{
    public class StudentController : RavenCrudController<Student>
    {
        
        public ActionResult Create(string name, string email, string[] interests)
        {
        	var student = new Student
        	{
        		Name = name, 
				Email = email, 
				Interests = interests
        	};
        	Session.Store(student);

			var documentId = Session.Advanced.GetDocumentId(student);
			return RedirectToAction("Details", new { id = documentId.Split('/').Last() });
        }
    }
}
