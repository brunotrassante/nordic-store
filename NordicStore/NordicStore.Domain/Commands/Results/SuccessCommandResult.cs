using NordicStore.Shared.Commands;
using System;

namespace NordicStore.Domain.Commands.Results
{
    public class SuccessCommandResult : CommandResult
    {
        public SuccessCommandResult(string message, int data)
        {
            Success = true;
            Message = message;
            Data = data;
        }
    }
}
