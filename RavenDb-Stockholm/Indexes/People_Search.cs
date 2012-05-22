using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using RavenDb_Stockholm.Models;
using Raven.Client.Linq.Indexing;

namespace RavenDb_Stockholm.Indexes
{
	public class People_Search : AbstractMultiMapIndexCreationTask<People_Search.Result>
	{
		public class Result
		{
			public string Name { get; set; }
		}

		public People_Search()
		{
			AddMap<Student>(students => from student in students 
										select new
										{
											Name = student.Name.Boost(2)
										}
										);

			AddMap<Instructor>(instructors => from student in instructors
										select new
										{
											student.Name
										}
										);

			Index(x=>x.Name, FieldIndexing.Analyzed);

		}
	}
}