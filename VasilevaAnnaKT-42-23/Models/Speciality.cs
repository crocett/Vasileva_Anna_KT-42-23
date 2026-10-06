using System.Text.Json.Serialization;

namespace VasilevaAnnaKT_42_23.Models
{
    public class Speciality
    {
            public int SpecialityId { get; set; }
            public string Title { get; set; }
            public string Code { get; set; }

        [JsonIgnore]
        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
