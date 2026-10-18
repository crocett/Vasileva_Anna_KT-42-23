using VasilevaAnnaKT_42_23.Interfaces.DisciplinesInterfaces;
using VasilevaAnnaKT_42_23.Interfaces.GradesInterfaces;
using VasilevaAnnaKT_42_23.Interfaces.GroupsInterfaces;
using VasilevaAnnaKT_42_23.Interfaces.StudentsInterfaces;

namespace VasilevaAnnaKT_42_23.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IDisciplineService, DisciplineService>();
            services.AddScoped<IGradeService, GradeService>();
            return services;
        }
    }
}
