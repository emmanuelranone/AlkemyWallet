namespace AlkemyWallet.Core.Models.DTO;

public class TransactionPagedDTO
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Concept { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; }
    //public int ToAccountId { get; set; }
}
