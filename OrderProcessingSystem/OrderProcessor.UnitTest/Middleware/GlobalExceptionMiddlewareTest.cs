using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using OrderProcessor.Middleware;
using Shouldly;

namespace OrderProcessor.UnitTest.Middleware;

public class GlobalExceptionMiddlewareTest
{
    private readonly GlobalExceptionMiddleware _globalExceptionMiddleware;

    private readonly RequestDelegate _next;

    public GlobalExceptionMiddlewareTest()
    {
        var logger = Substitute.For<ILogger<GlobalExceptionMiddleware>>();
        _next = Substitute.For<RequestDelegate>();
        _globalExceptionMiddleware = new GlobalExceptionMiddleware(_next, logger);
    }

    [Fact]
    public async Task GivenInvokeAsync_WhenExceptionOccured_ReturnErrorResponse()
    {
        var context = new DefaultHttpContext();
        _next(context).Throws<Exception>();

        await _globalExceptionMiddleware.InvokeAsync(context);
        
        context.Response.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
    }
}