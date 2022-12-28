using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AlkemyWallet.Entities;

public class EntityBase
{
    [Key]
    public int Id { get; set; }

    public bool IsDeleted { get; set; } = false;
}
