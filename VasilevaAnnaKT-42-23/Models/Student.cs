using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace VasilevaAnnaKT_42_23.Models
{
        public class Student
        {
            public int StudentId { get; set; }
            public string FirstName { get; set; }       
            public string LastName { get; set; }      
            public int GroupId { get; set; }          
                    
            public bool IsDeleted { get; set; }
            public Group Group { get; set; }

            [JsonIgnore]
            public ICollection<Grade> Grades { get; set; } = new List<Grade>();
        }
}
