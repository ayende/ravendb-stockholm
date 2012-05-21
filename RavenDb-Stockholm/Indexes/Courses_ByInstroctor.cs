using System.Linq;
using Raven.Client.Indexes;
using RavenDb_Stockholm.Models;

namespace RavenDb_Stockholm.Indexes
{
	public class Courses_ByInstructor : AbstractIndexCreationTask<Course, Courses_ByInstructor.Result>
	{
		 public class Result
		 {
			 public string Instructor { get; set; }
			 public int Count { get; set; }
		 }

		 public Courses_ByInstructor()
		{
			Map = courses => from course in courses
			                 select new
			                 {
								 course.Instructor,
								 Count= 1
			                 };

			Reduce = results => from result in results
			                    group result by result.Instructor
			                    into g
			                    select new
			                    {
									Instructor = g.Key,
									Count = g.Sum(x=>x.Count)
			                    };

		 	TransformResults = (database, results) =>
		 	                   from result in results
		 	                   let instructor = database.Load<Instructor>(result.Instructor)
		 	                   select new
		 	                   {
		 	                   	InstructorName = instructor.Name,
								result.Instructor,
		 	                   	result.Count
		 	                   };
		}
	}
}