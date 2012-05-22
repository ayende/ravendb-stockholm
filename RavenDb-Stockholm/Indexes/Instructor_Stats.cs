using System.Linq;
using Raven.Client.Indexes;
using RavenDb_Stockholm.Models;

namespace RavenDb_Stockholm.Indexes
{
	public class Instructor_Stats : AbstractMultiMapIndexCreationTask<Instructor_Stats.Result>
	{
		public class Result
		{
			public string InstructorId { get; set; }
			public int Courses { get; set; }
			public int FavoriatedByStudents { get; set; }
		}

		public Instructor_Stats()
		{
			AddMap<Student>(students => from student in students
										where student.FavoriteInstructor != null
										select new
										{
											InstructorId = student.FavoriteInstructor,
											Courses = 0,
											FavoriatedByStudents = 1
										});

			AddMap<Course>(students => from course in students
										select new
										{
											InstructorId = course.Instructor,
											Courses = 1,
											FavoriatedByStudents = 0
										});

			Reduce = results =>
			         from result in results
			         group result by result.InstructorId
			         into g
			         select new
			         {
						 InstructorId = g.Key,
						 Courses = g.Sum(x=>x.Courses),
						 FavoriatedByStudents = g.Sum(x=>x.FavoriatedByStudents)
			         };
		}
	}
}