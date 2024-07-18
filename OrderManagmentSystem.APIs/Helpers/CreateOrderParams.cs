namespace OrderManagmentSystem.APIs.Helpers
{
    public class CreateOrderParams
    {
        public int CustmorId { get; set; }
        public string PaymentMethod { get; set; }
        public List<CreateOrderItemParams> OrderItems { get; set; }
    }
}
