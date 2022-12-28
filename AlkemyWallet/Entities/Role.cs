using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities;

public class Role : EntityBase
{
    [Column("name")]
    public string Name { get; set; }
    [Column("description")]
    public string Description { get; set; }
}
