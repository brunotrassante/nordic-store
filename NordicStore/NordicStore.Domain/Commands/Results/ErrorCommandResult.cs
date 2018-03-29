using NordicStore.Shared.Commands;
using System;
using System.Collections;

namespace NordicStore.Domain.Commands.Results
{
    public class ErrorCommandResult : CommandResult
    {
        public ErrorCommandResult(string message, IEnumerable notifications)
        {
            Success = false;
            Message = message;
            Data = notifications;
        }
    }
}
