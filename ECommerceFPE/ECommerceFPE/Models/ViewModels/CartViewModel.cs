namespace ECommerceFPE.Models.ViewModels
{
    public class CartViewModel
    {
        public Cart Cart { get; set; }
        public List<CartItems> CartItems { get; set; }
        public int ProductIdToAdd { get; set; }
    }
}
