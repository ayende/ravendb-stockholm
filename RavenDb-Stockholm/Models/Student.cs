using System.Collections.Generic;

namespace RavenDb_Stockholm.Models
{
    public class Student
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string[] Interests { get; set; }

        public List<string> Courses { get; set; }
    }
}