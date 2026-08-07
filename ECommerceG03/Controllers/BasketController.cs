using ECommerceG03.Application.Contracts;
using ECommerceG03.Application.DTOs.BasketDTOs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceG03.Controllers
{
    public class BasketController : ApiBaseController
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        /// <summary>
        /// Get specific Basket items
        /// BaseURL/api/Basket/{id}
        /// </summary>
        /// <param name="id">Basket ID</param>
        /// <param name="ct" optional="true">Cancellation token</param>
        /// <returns>List of Basket items</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> GetBasket(string id, CancellationToken ct = default)
        {
            var result = await _basketService.GetBasketByIdAsync(id, ct);
            return ToActionResult(result);
        }

        /// <summary>
        /// Post specific Basket
        /// BaseURL/api/Basket => Body: BasketDto
        /// </summary>
        /// <returns>Create or Update Basket items</returns>
        /// <param name="basketDto">Basket data transfer object</param>
        /// <param name="ct" optional="true">Cancellation token</param>
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket([FromBody] BasketDto basketDto, CancellationToken ct = default)
        {
            var result = await _basketService.CreateOrUpdateBasketAsync(basketDto, ct:ct);
            return ToActionResult(result);
        }

        /// <summary>
        /// Delete specific Basket
        /// BaseURL/api/Basket/{id}
        /// </summary>
        /// <param name="id">Basket ID</param>
        /// <param name="ct" optional="true">Cancellation token</param>
        /// <returns>Action result indicating the outcome of the delete operation</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id, CancellationToken ct = default)
        {
            var result = await _basketService.DeleteBasketAsync(id, ct);
            return ToActionResult(result);
        }
    }
}
