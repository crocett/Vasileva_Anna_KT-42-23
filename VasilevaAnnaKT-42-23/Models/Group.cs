using System.Text.Json.Serialization;

namespace VasilevaAnnaKT_42_23.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int Course { get; set; }
        public int SpecialityId { get; set; } 
        public Speciality Speciality { get; set; } 
        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
