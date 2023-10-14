using AutoMapper;
using Core.Entities;
using FullStack_Task.Dtos;

namespace FullStack_Task.Helpers
{
    public class ProductUrlRssolver :  IValueResolver<Product, ProductToReturnDto, string>
    {
        public IConfiguration _config { get; }
        public ProductUrlRssolver(IConfiguration config)
        {
            _config = config;
        }
        //implement interface IValueResolver
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImagePath))
            {
                return _config.GetValue<string>("ApiUrl") + source.ImagePath;
            }
            return null;
        }
    }
}
