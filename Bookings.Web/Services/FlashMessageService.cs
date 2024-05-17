#nullable disable
using System.Text.Json;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Bookings.Web.Services;

public class FlashMessageService
{
    private const string Key = "FlashMessages";
    private readonly ITempDataDictionary _tempData;

    public FlashMessageService(ITempDataDictionaryFactory tempDataFactory, IActionContextAccessor actionContextAccessor)
    {
        _tempData = tempDataFactory.GetTempData(actionContextAccessor.ActionContext.HttpContext);
    }

    public void AddMessage(string message, string type = "info")
    {
        var messages = _tempData.ContainsKey(Key) 
            ? JsonSerializer.Deserialize<List<FlashMessage>>(_tempData[Key] as string) 
            : new List<FlashMessage>();

        messages.Add(new FlashMessage { Message = message, Type = type });
        _tempData[Key] = JsonSerializer.Serialize(messages);
    }

    public List<FlashMessage> GetMessages()
    {
        if (!_tempData.ContainsKey(Key))
        {
            return new List<FlashMessage>();
        }

        var messages = JsonSerializer.Deserialize<List<FlashMessage>>(_tempData[Key] as string);
        _tempData.Remove(Key);
        return messages;
    }
}
