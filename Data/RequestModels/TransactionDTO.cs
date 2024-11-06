namespace Data.RequestModels
{
    public class TransactionDTO
    {
        public int AccountFromId { get; set; }
        public int AccountToId { get; set; }
        public decimal Amount { get; set; }

        public TransactionDTO(int accountFromId, int accountToId, decimal amount)
        {
            AccountFromId = accountFromId;
            AccountToId = accountToId;
            Amount = amount;
        }

    }
}
