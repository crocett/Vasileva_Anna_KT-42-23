using System.Text.Json.Serialization;

namespace VasilevaAnnaKT_42_23.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int Value { get; set; }               
        public int StudentId { get; set; }          
        public int DisciplineId { get; set; }
        [JsonIgnore]
        public Student? Student { get; set; }
        [JsonIgnore]
        public Discipline? Discipline { get; set; } 
    }
}
