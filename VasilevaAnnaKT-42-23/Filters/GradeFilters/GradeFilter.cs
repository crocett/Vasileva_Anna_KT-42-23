namespace VasilevaAnnaKT_42_23.Filters.GradeFilters
{
    public class GroupDisciplineFilter
    {
        public int GroupId { get; set; }
        public int DisciplineId { get; set; }
    }

    public class StudentDisciplineFilter
    {
        public int StudentId { get; set; }
        public int DisciplineId { get; set; }
    }

    public class YearFilter
    {
        public int Year { get; set; }
    }
}