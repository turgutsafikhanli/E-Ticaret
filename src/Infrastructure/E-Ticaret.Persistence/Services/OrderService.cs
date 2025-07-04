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
using E_Ticaret.Application.Shared;
using E_Ticaret.Domain.Entities;

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
        var entity = _mapper.Map<Order>(dto);
        await _orderRepository.AddAsync(entity);
        await _orderRepository.SaveChangeAsync();

        return new BaseResponse<string>("Sifariş yaradıldı", HttpStatusCode.Created);
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            return new BaseResponse<string>("Sifariş tapılmadı", HttpStatusCode.NotFound);

        _orderRepository.Delete(order);
        await _orderRepository.SaveChangeAsync();

        return new BaseResponse<string>("Sifariş silindi", HttpStatusCode.OK);
    }

    public async Task<BaseResponse<OrderGetDto>> GetByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            return new BaseResponse<OrderGetDto>("Sifariş tapılmadı", HttpStatusCode.NotFound);

        var dto = _mapper.Map<OrderGetDto>(order);
        return new BaseResponse<OrderGetDto>("Data", dto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<OrderGetDto>>> GetAllAsync()
    {
        var orders = _orderRepository.GetAll().ToList();
        if (!orders.Any())
            return new BaseResponse<List<OrderGetDto>>("Sifariş yoxdur", HttpStatusCode.NotFound);

        var dtos = _mapper.Map<List<OrderGetDto>>(orders);
        return new BaseResponse<List<OrderGetDto>>("Data", dtos, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<OrderGetDto>>> GetByUserIdAsync(string userId)
    {
        var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
        if (orders == null || !orders.Any())
            return new BaseResponse<List<OrderGetDto>>("İstifadəçiyə aid sifariş tapılmadı", HttpStatusCode.NotFound);

        var dtos = _mapper.Map<List<OrderGetDto>>(orders);
        return new BaseResponse<List<OrderGetDto>>("Data", dtos, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> UpdateAsync(OrderUpdateDto dto)
    {
        var order = await _orderRepository.GetByIdAsync(dto.Id);
        if (order == null)
            return new BaseResponse<string>("Sifariş tapılmadı", HttpStatusCode.NotFound);

        _mapper.Map(dto, order);
        _orderRepository.Update(order);
        await _orderRepository.SaveChangeAsync();

        return new BaseResponse<string>("Sifariş yeniləndi", HttpStatusCode.OK);
    }
}
