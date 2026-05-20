using System.ComponentModel.DataAnnotations;

namespace CodingTask.Data.Entities;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}