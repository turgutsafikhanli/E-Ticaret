﻿using System.Net;

namespace E_Ticaret.Application.Shared;

public class BaseResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }


    public BaseResponse(HttpStatusCode statusCode)
    {
        Success = true;
        StatusCode = statusCode;
    }
    public BaseResponse(string message, HttpStatusCode statusCode)
    {
        Message = message;
        Success = false;
        StatusCode = statusCode;
    }
    public BaseResponse(string message, bool isSuccess, HttpStatusCode statusCode)
    {
        Message = message;
        Success = isSuccess;
        StatusCode = statusCode;
    }
    public BaseResponse(string message, T? data, HttpStatusCode statusCode)
    {
        Success = true;
        Message = message;
        Data = data;
        StatusCode = statusCode;
    }
}