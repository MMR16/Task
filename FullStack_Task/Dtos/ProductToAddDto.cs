using FullStack_Task.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FullStack_Task.Dtos
{
    public class ProductToAddDto
    {
        [Required(ErrorMessage = "please Add Product Name")]
        [MaxLength(100, ErrorMessage = "must be less than 100 character")]
        public string Name { get; set; }

        [Required(ErrorMessage = "please Add Product Price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "please Add Minimum Product Quantity")]
        public int MinQuantity { get; set; }

        [Required(ErrorMessage = "Please select file")]
        [ImageValidation(new[] { "image/jpeg", "image/jpg", "image/png", "image/gif"},ErrorMessage ="File type Is Invalid")]
        public IFormFile Image { get; set; }

        public double? DiscountRate { get; set; }

        public int? CategoryId { get; set; }
    }
}
