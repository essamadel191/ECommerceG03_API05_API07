using ECommerceG03.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Specification
{
    public class ProductWithIdSpecification : BaseSpecification<Product,int>
    {
        // HashSet for Uniqueness 
        public ProductWithIdSpecification(HashSet<int> productIds):base(p => productIds.Contains(p.Id))
        {
            
        }
    }
}
