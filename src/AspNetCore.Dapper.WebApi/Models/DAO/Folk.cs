using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Dapper.WebApi.Models.DAO;

public class Folk
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; }

    public DateTimeOffset? Birthday { get; set; }

    [StringLength(100)]
    public string Address { get; set; }

    [StringLength(20)]
    public string Phone { get; set; }
    public DateTimeOffset? EmploymentOn { get; set; }
}
