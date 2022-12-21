namespace WebApplication1.Models
{
    public class Images
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
