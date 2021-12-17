using DevelopmentFast.Infra.ProjectTest.DomainTest.Entities;
using DevelopmentFast.Repository.Domain.Interfaces.Repository;

namespace DevelopmentFast.Infra.ProjectTest.DomainTest.Repositories
{
    public interface IStudentRepository : IBaseGenericRepositoryDF<Student, string>
    {
    }
}
