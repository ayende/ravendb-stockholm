using System.Collections.Generic;

namespace RavenDb_Stockholm.Models
{
    public class Instructor
    {
        public string Name { get; set; }

        public List<string> CoursesGiven { get; set; }
    }
}