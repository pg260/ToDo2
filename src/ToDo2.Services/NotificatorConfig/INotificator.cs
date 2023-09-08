using FluentValidation.Results;

namespace ToDo2.Services.NotificatorConfig;

public interface INotificator
{
    void Handle(string message);
    void Handle(List<ValidationFailure> failures);
    void HandleNotFound();
    IEnumerable<string> GetNotifications();

    bool HasNotification { get; }
    bool IsNotFound { get; }
}