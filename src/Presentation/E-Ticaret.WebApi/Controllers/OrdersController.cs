using System.Net;
using E_Ticaret.Application.Abstracts.Services;
using E_Ticaret.Application.DTOs.OrderDtos;
using E_Ticaret.Application.DTOs.OrderProductDtos;
using E_Ticaret.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Ticaret.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IOrderProductService _orderProductService;

    public OrderController(IOrderService orderService, IOrderProductService orderProductService)
    {
        _orderService = orderService;
        _orderProductService = orderProductService;
    }

    [HttpPost]
    [Authorize(Policy = "Permissions.Order.Create")]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Post([FromBody] OrderCreateDto dto)
    {
        var result = await _orderService.CreateAsync(dto);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut]
    [Authorize(Policy = "Permissions.Order.Update")]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Put(Guid id, [FromBody] OrderUpdateDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(new BaseResponse<string>(HttpStatusCode.BadRequest)
            {
                Success = false,
                Message = "ID mismatch between URL and body"
            });
        }

        var result = await _orderService.UpdateAsync(dto);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet]
    [Authorize(Policy = "Permissions.Order.Get")]
    [ProducesResponseType(typeof(BaseResponse<OrderGetDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _orderService.GetByIdAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("GetAll")]
    [Authorize(Policy = "Permissions.Order.Get")]
    [ProducesResponseType(typeof(BaseResponse<List<OrderGetDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _orderService.GetAllAsync();
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("GetByUserId")]
    [Authorize(Policy = "Permissions.Order.Get")]
    [ProducesResponseType(typeof(BaseResponse<List<OrderGetDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetByUserId([FromRoute] string userId)
    {
        var result = await _orderService.GetByUserIdAsync(userId);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete]
    [Authorize(Policy = "Permissions.Order.Delete")]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _orderService.DeleteAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }

    //[HttpPut("products")]
    //[Authorize(Policy = "Permissions.OrderProduct.Update")]
    //[ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    //[ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
    //[ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    //public async Task<IActionResult> UpdateOrderProduct([FromBody] OrderProductUpdateDto dto)
    //{
    //    var result = await _orderProductService.UpdateAsync(dto);
    //    return StatusCode((int)result.StatusCode, result);
    //}

    //[HttpDelete("products")]
    //[Authorize(Policy = "Permissions.OrderProduct.Delete")]
    //[ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    //[ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    //[ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    //public async Task<IActionResult> DeleteOrderProduct([FromRoute] Guid orderId, [FromRoute] Guid productId)
    //{
    //    var result = await _orderProductService.DeleteAsync(orderId, productId);
    //    return StatusCode((int)result.StatusCode, result);
    //}

    //[HttpGet("/products")]
    //[Authorize(Policy = "Permissions.OrderProduct.Get")]
    //[ProducesResponseType(typeof(BaseResponse<List<OrderProductGetDto>>), (int)HttpStatusCode.OK)]
    //[ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    //[ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    //public async Task<IActionResult> GetOrderProducts([FromRoute] Guid orderId)
    //{
    //    var result = await _orderProductService.GetByOrderIdAsync(orderId);
    //    return StatusCode((int)result.StatusCode, result);
    //}
}
