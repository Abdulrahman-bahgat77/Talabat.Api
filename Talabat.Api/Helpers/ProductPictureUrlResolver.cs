using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using System.Linq.Expressions;
using System.Reflection;
using Talabat.Api.DTOs;
using Talabat.Core.Entities;

namespace Talabat.Api.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";
            }
            return string.Empty ;
        }
    }
}
