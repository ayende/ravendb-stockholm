using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using RavenDb_Stockholm.Models;

namespace RavenDb_Stockholm.Indexes
{
	public class Instructors_ByName : AbstractIndexCreationTask<Instructor>
	{
		public Instructors_ByName()
		{
			Map = instructors =>
				  from instructor in instructors
				  select new { instructor.Name };

			Index(x => x.Name, FieldIndexing.Analyzed);
		}
	}
}