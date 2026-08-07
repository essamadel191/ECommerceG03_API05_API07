using ECommerceG03.Application.Contracts;
using ECommerceG03.Application.DTOs.OrderDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceG03.Controllers
{

    public class OrderController : ApiBaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder (OrderDto orderDto, CancellationToken ct = default)
        {
            return ToActionResult(await _orderService.CreateOrderAsync(orderDto,GetEmailFromToken(),ct));
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id, CancellationToken ct)
        {
            return ToActionResult(await _orderService.GetOrdersByIdAndUserEmailAsync(GetEmailFromToken(), id,ct));
        }
        [Authorize]
        [HttpGet("GetOrderByEmail")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderByEmail(CancellationToken ct)
        {
            return ToActionResult(await _orderService.GetOrdersForSpecificUserAsync(GetEmailFromToken(),ct));
        }

        [AllowAnonymous]
        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethod(CancellationToken ct)
        {
            return ToActionResult(await _orderService.GetDeliveryMethodAsync(ct));
        }
    }
}
