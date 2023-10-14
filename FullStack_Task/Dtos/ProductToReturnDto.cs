namespace FullStack_Task.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int MinQuantity { get; set; }
        public string ImagePath { get; set; }
        public double? DiscountRate { get; set; }
        public Guid Code { get; set; }
        public int? CategoryId { get; set; }
    }
}
