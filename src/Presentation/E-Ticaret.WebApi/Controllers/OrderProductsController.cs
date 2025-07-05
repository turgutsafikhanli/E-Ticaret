using System.Net;
using E_Ticaret.Application.Abstracts.Services;
using E_Ticaret.Application.DTOs.OrderProductDtos;
using E_Ticaret.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Ticaret.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderProductsController : ControllerBase
{
    private readonly IOrderProductService _orderProductService;

    public OrderProductsController(IOrderProductService orderProductService)
    {
        _orderProductService = orderProductService;
    }

    [HttpPost("{orderId:guid}")]
    [Authorize(Policy = "Permissions.OrderProduct.Create")]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Add([FromRoute] Guid orderId, [FromBody] OrderProductCreateDto dto)
    {
        var result = await _orderProductService.AddAsync(dto, orderId);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut]
    [Authorize(Policy = "Permissions.OrderProduct.Update")]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Update([FromBody] OrderProductUpdateDto dto)
    {
        var result = await _orderProductService.UpdateAsync(dto);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("{orderId:guid}/{productId:guid}")]
    [Authorize(Policy = "Permissions.OrderProduct.Delete")]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid orderId, [FromRoute] Guid productId)
    {
        var result = await _orderProductService.DeleteAsync(orderId, productId);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("order/{orderId:guid}")]
    [Authorize(Policy = "Permissions.OrderProduct.Read")]
    [ProducesResponseType(typeof(BaseResponse<List<OrderProductGetDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetByOrderId([FromRoute] Guid orderId)
    {
        var result = await _orderProductService.GetByOrderIdAsync(orderId);
        return StatusCode((int)result.StatusCode, result);
    }
}
