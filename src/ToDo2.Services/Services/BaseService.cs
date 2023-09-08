using AutoMapper;
using ToDo2.Services.NotificatorConfig;

namespace ToDo2.Services.Services;

public abstract class BaseService
{
    protected readonly IMapper Mapper;
    protected readonly INotificator Notificator;

    protected BaseService(IMapper mapper, INotificator notificator)
    {
        Mapper = mapper;
        Notificator = notificator;
    }
}