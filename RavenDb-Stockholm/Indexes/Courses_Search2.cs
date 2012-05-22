using System.Linq;
using Raven.Client.Indexes;
using RavenDb_Stockholm.Models;

namespace RavenDb_Stockholm.Indexes
{
	public class Courses_Search2 : AbstractIndexCreationTask<Course, Courses_Search2.Result>
	{
		public class Result
		{
			public string Query { get; set; }
		}

		public Courses_Search2()
		{
			Map = courses =>
			      from course in courses
			      select new
			      {
			      	Query = new object[]
			      	{
			      		course.Location,
			      		course.Date,
			      		course.Technologies,
			      		course.Name
			      	}
			      };
		}
	}
}