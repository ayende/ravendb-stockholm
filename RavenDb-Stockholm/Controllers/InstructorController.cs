using System.Linq;
using System.Web.Mvc;
using RavenDb_Stockholm.Models;

namespace RavenDb_Stockholm.Controllers
{
	public class InstructorController : RavenCrudController<Instructor>
    {
       
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
