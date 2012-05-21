using System;

namespace RavenDb_Stockholm.Models
{
    public class Course
    {
        public string Name { get; set; }

        public string Instructor { get; set; }

        public string Content { get; set; }

        public string[] Technologies { get; set; }

        public string Location { get; set; }

        public DateTime Date { get; set; }
    }
}