using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace WebApi.Helpers
{
    public class ImageUrlProvider : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ImageUrlProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (source.PictureUrl is not null)
            {
                return $"{_configuration["BaseUrl"]}/{source.PictureUrl}";
            }

            return string.Empty;
        }
    }
}