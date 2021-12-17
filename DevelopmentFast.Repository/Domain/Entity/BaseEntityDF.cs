using System.ComponentModel.DataAnnotations;

namespace DevelopmentFast.Repository.Domain.Entity
{
    public abstract class BaseEntityDF<TPrimaryKey> where TPrimaryKey : IComparable<TPrimaryKey>

    {

        protected BaseEntityDF()
        {
        }

        protected BaseEntityDF(TPrimaryKey id)
        {
            Id = id;
        }

        [Key]
        public TPrimaryKey Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

    }
}
