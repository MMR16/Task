using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FullStack_Task.Validators
{
    public class ImageValidationAttribute:ValidationAttribute
    {
        public ImageValidationAttribute(string[] allowedImageTypes)
        {
            AllowedImageTypes = allowedImageTypes;
        }

        public string[] AllowedImageTypes { get; set; }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;

            if (file != null && file.Length > 0)
            {
              // var allowedImageTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif" };

                return AllowedImageTypes.Contains(file.ContentType);
            }

            return false;
        }

    }
}
