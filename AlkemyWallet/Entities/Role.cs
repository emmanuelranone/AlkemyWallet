using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities;

public class Role : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
}
