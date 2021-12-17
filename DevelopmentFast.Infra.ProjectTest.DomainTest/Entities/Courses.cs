using DevelopmentFast.Repository.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DevelopmentFast.Infra.ProjectTest.DomainTest.Entities
{
    public class Courses : BaseEntityDF<string>
    {
        public string Name { get; set; }
        public Courses(string name) : base()
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
        }


        public string IdStudent { get; set; }

        [ForeignKey("IdStudent")]
        [JsonIgnore]
        public Student Student { get; set; }

    }
}
