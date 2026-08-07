using AutoMapper;
using ECommerceG03.Application.DTOs.ProductDtos;
using ECommerceG03.Domain.Entities.Products;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Profiles
{
    public class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly UrlSettings _urlSettings;
        public PictureUrlResolver(IOptions<UrlSettings> options)
        {
            _urlSettings = options.Value;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            var baseUrl = _urlSettings.BaseUrl.TrimEnd('/');
            if (string.IsNullOrEmpty(baseUrl))
                throw new InvalidOperationException("UrlSettings.BaseUrl is not configured. Configure UrlSettings:BaseUrl in configuration.");
            var path = source.PictureUrl.TrimStart('/');

            return $"{baseUrl}/Files/{path}";
        }
    }

    public class UrlSettings
    {
        public string BaseUrl { get; set; } = default!;
    }
}
