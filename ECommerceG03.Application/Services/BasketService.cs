using AutoMapper;
using ECommerceG03.Application.Common;
using ECommerceG03.Application.Contracts;
using ECommerceG03.Application.DTOs.BasketDTOs;
using ECommerceG03.Domain.Contracts;
using ECommerceG03.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepo = basketRepository;
            _mapper = mapper;
        }

        public async Task<Result<BasketDto>> GetBasketByIdAsync(string basketId, CancellationToken ct = default)
        {
            var basketResult = await _basketRepo.GetBasketByIdAsync(basketId, ct);
            return basketResult == null ? Result<BasketDto>.Fail(Error.Failure($"Failed to retrieve basket {basketId}")) 
                : Result<BasketDto>.Ok(_mapper.Map<BasketDto>(basketResult));
        }

        public async Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, TimeSpan? TimeToLive = null, CancellationToken ct = default)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basket);
            var basketResult = await _basketRepo.CreateOrUpdateBasketAsync(customerBasket, TimeToLive, ct);

            return basketResult == null ? Result<BasketDto>.Fail(Error.Failure("Failed to create or update basket")) 
                : Result<BasketDto>.Ok(basket);
        }

        public async Task<Result<bool>> DeleteBasketAsync(string basketId, CancellationToken ct = default)
        {
            var result = await _basketRepo.DeleteBasketAsync(basketId, ct);
           return result ? Result<bool>.Ok(true)
                : Result<bool>.Fail(Error.NotFound($"Failed To Delete Basket {basketId}"));
        }

    }
}
