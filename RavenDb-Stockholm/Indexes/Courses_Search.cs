using System.Linq;
using Raven.Client.Indexes;
using RavenDb_Stockholm.Models;

namespace RavenDb_Stockholm.Indexes
{
	public class Courses_Search : AbstractIndexCreationTask<Course>
	{
		public Courses_Search()
		{
			Map = courses =>
			      from course in courses
			      select new
			      {
					  course.Location,
					  course.Date,
					  course.Technologies,
					  course.Name
			      };
		}
	}
}