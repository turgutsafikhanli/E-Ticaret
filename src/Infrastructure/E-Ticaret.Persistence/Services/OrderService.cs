using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Ticaret.Application.Abstracts.Repositories;
using E_Ticaret.Application.Abstracts.Services;
using E_Ticaret.Application.DTOs.OrderDtos;
using E_Ticaret.Application.DTOs.OrderProductDtos;
using E_Ticaret.Application.Shared;
using E_Ticaret.Domain.Entities;
using E_Ticaret.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using static E_Ticaret.Application.Shared.Permissions;

namespace E_Ticaret.Persistence.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<string>> CreateAsync(OrderCreateDto dto)
    {
        try
        {
            var order = new Domain.Entities.Order
            {
                UserId = dto.UserId,
                Status = dto.Status
            };

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangeAsync();

            return new BaseResponse<string>(HttpStatusCode.Created)
            {
                Data = order.Id.ToString(),
                Message = "Order created successfully"
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<string>(HttpStatusCode.InternalServerError)
            {
                Success = false,
                Message = $"Error creating order: {ex.Message}"
            };
        }
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return new BaseResponse<string>(HttpStatusCode.NotFound)
                {
                    Success = false,
                    Message = "Order not found"
                };
            }

            await _orderRepository.SoftDeleteAsync(order);

            return new BaseResponse<string>(HttpStatusCode.OK)
            {
                Data = id.ToString(),
                Message = "Order deleted successfully"
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<string>(HttpStatusCode.InternalServerError)
            {
                Success = false,
                Message = $"Error deleting order: {ex.Message}"
            };
        }
    }

    public async Task<BaseResponse<List<OrderGetDto>>> GetAllAsync()
    {
        try
        {
            var orders = _orderRepository.GetAll().ToList();
            var dtos = _mapper.Map<List<OrderGetDto>>(orders);

            return new BaseResponse<List<OrderGetDto>>(HttpStatusCode.OK)
            {
                Data = dtos
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<OrderGetDto>>(HttpStatusCode.InternalServerError)
            {
                Success = false,
                Message = $"Error fetching orders: {ex.Message}"
            };
        }
    }

    public async Task<BaseResponse<OrderGetDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return new BaseResponse<OrderGetDto>(HttpStatusCode.NotFound)
                {
                    Success = false,
                    Message = "Order not found"
                };
            }

            var dto = _mapper.Map<OrderGetDto>(order);
            return new BaseResponse<OrderGetDto>(HttpStatusCode.OK)
            {
                Data = dto
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<OrderGetDto>(HttpStatusCode.InternalServerError)
            {
                Success = false,
                Message = $"Error fetching order: {ex.Message}"
            };
        }
    }

    public async Task<BaseResponse<List<OrderGetDto>>> GetByUserIdAsync(string userId)
    {
        try
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            var dtos = _mapper.Map<List<OrderGetDto>>(orders);

            return new BaseResponse<List<OrderGetDto>>(HttpStatusCode.OK)
            {
                Data = dtos
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<OrderGetDto>>(HttpStatusCode.InternalServerError)
            {
                Success = false,
                Message = $"Error fetching orders for user: {ex.Message}"
            };
        }
    }

    public async Task<BaseResponse<string>> UpdateAsync(OrderUpdateDto dto)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(dto.Id);
            if (order == null)
            {
                return new BaseResponse<string>(HttpStatusCode.NotFound)
                {
                    Success = false,
                    Message = "Order not found"
                };
            }

            order.UserId = dto.UserId;

            _orderRepository.Update(order);
            await _orderRepository.SaveChangeAsync();

            return new BaseResponse<string>(HttpStatusCode.OK)
            {
                Data = order.Id.ToString(),
                Message = "Order updated successfully"
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<string>(HttpStatusCode.InternalServerError)
            {
                Success = false,
                Message = $"Error updating order: {ex.Message}"
            };
        }
    }
}
