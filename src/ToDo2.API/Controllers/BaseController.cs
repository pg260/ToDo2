﻿using Microsoft.AspNetCore.Mvc;
using ToDo2.API.Reponses;
using ToDo2.Services.NotificatorConfig;

namespace ToDo2.API.Controllers;

[ApiController]
public class BaseController : Controller
{
    public BaseController(INotificator notificator)
    {
        _notificator = notificator;
    }
    
    private readonly INotificator _notificator;
    
    protected IActionResult NoContentResponse() => CustomResponse(NoContent());

    protected IActionResult CreatedResponse(string uri = "", object? result = null) => CustomResponse(Created(uri, result));

    protected IActionResult OkResponse(object? result = null) => CustomResponse(Ok(result));
    
    protected IActionResult CustomResponse(IActionResult objectResult)
    {
        if (OperacaoValida) return objectResult;

        if (_notificator.IsNotFound) return NotFound();

        var response = new BadRequestResponse(_notificator.GetNotifications().ToList());
        return BadRequest(response);
    }
    
    private bool OperacaoValida => !(_notificator.HasNotification || _notificator.IsNotFound);
}