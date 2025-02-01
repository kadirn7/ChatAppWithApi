using System;

namespace ChatApp.Data.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDeleted { get; set; }

}
