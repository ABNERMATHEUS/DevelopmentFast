using DevelopmentFast.Infra.ProjectTest.DomainTest.Entities;
using DevelopmentFast.Infra.ProjectTest.DomainTest.Repositories;
using DevelopmentFast.Infra.ProjectTest.InfraTest.DataContext;
using DevelopmentFast.Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace DevelopmentFast.Infra.ProjectTest.InfraTest.Repositories
{
    public class StudentRepository : BaseGenericRepositoryDF<Student, string>, IStudentRepository
    {
        public StudentRepository(Context dbContext) : base(dbContext)
        {           
                        
        }
    }
}
