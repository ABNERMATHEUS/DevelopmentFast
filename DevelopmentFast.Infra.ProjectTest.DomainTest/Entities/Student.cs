using DevelopmentFast.Repository.Domain.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevelopmentFast.Infra.ProjectTest.DomainTest.Entities
{
    public class Student : BaseEntityDF<string>
    {
        public Student(string name) : base()
        {
            Id = Guid.NewGuid().ToString();
            Name = name;

        }

        public string Name { get; set; }
        
        public IEnumerable<Courses> Courses { get; set; }

    }
}
