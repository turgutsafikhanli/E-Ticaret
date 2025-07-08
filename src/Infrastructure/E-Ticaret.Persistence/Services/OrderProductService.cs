using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Ticaret.Application.Abstracts.Repositories;
using E_Ticaret.Application.Abstracts.Services;
using E_Ticaret.Application.DTOs.OrderProductDtos;
using E_Ticaret.Application.Shared;
using E_Ticaret.Domain.Entities;
using E_Ticaret.Persistence.Repositories;
using static E_Ticaret.Application.Shared.Permissions;

namespace E_Ticaret.Persistence.Services;

public class OrderProductService : IOrderProductService
{
    private readonly IOrderProductRepository _orderProductRepository;
    private readonly IMapper _mapper;

    public OrderProductService(IOrderProductRepository orderProductRepository, IMapper mapper)
    {
        _orderProductRepository = orderProductRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<string>> AddAsync(OrderProductCreateDto dto, Guid orderId)
    {
        try
        {
            var orderProduct = new Domain.Entities.OrderProduct
            {
                OrderId = orderId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UnitPrice = 0 // Əgər məhsul qiyməti varsa, burda təyin et
            };

            await _orderProductRepository.AddAsync(orderProduct);
            await _orderProductRepository.SaveChangeAsync();

            return new BaseResponse<string>(HttpStatusCode.Created)
            {
                Data = $"{orderId}:{dto.ProductId}",
                Message = "OrderProduct added successfully"
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<string>(HttpStatusCode.InternalServerError)
            {
                Success = false,
                Message = $"Error adding OrderProduct: {ex.Message}"
            };
        }
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid orderId, Guid productId)
    {
        try
        {
            var orderProduct = await _orderProductRepository.GetByOrderIdAndProductIdAsync(orderId, productId);
            if (orderProduct == null)
            {
                return new BaseResponse<string>(HttpStatusCode.NotFound)
                {
                    Success = false,
                    Message = "OrderProduct not found"
                };
            }

            await _orderProductRepository.SoftDeleteAsync(orderProduct);

            return new BaseResponse<string>(HttpStatusCode.OK)
            {
                Data = $"{orderId}:{productId}",
                Message = "OrderProduct deleted successfully"
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<string>(HttpStatusCode.InternalServerError)
            {
                Success = false,
                Message = $"Error deleting OrderProduct: {ex.Message}"
            };
        }
    }

    public async Task<BaseResponse<List<OrderProductGetDto>>> GetByOrderIdAsync(Guid orderId)
    {
        try
        {
            var orderProducts = await _orderProductRepository.GetOrderProductsByOrderIdAsync(orderId);
            var dtos = _mapper.Map<List<OrderProductGetDto>>(orderProducts);

            return new BaseResponse<List<OrderProductGetDto>>(HttpStatusCode.OK)
            {
                Data = dtos
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<OrderProductGetDto>>(HttpStatusCode.InternalServerError)
            {
                Success = false,
                Message = $"Error fetching OrderProducts: {ex.Message}"
            };
        }
    }

    public async Task<BaseResponse<string>> UpdateAsync(OrderProductUpdateDto dto)
    {
        try
        {
            var orderProduct = await _orderProductRepository.GetByOrderIdAndProductIdAsync(dto.OrderId, dto.ProductId);
            if (orderProduct == null)
            {
                return new BaseResponse<string>(HttpStatusCode.NotFound)
                {
                    Success = false,
                    Message = "OrderProduct not found"
                };
            }

            orderProduct.Quantity = dto.Quantity;

            _orderProductRepository.Update(orderProduct);
            await _orderProductRepository.SaveChangeAsync();

            return new BaseResponse<string>(HttpStatusCode.OK)
            {
                Data = $"{dto.OrderId}:{dto.ProductId}",
                Message = "OrderProduct updated successfully"
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<string>(HttpStatusCode.InternalServerError)
            {
                Success = false,
                Message = $"Error updating OrderProduct: {ex.Message}"
            };
        }
    }
}
