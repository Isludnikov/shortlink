using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortLink.Database.Entities;
[Table("links")]
[Index(nameof(UrlShort), IsUnique = true)]
[Index(nameof(UrlLong), IsUnique = true)]
public class Link
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Column(TypeName = "varchar(15)")]
    public string UrlShort { get; set; }
    [Column(TypeName = "varchar(3000)")]
    public string UrlLong { get; set; }
    public DateTime KillTime { get; set; }
}