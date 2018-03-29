using NordicStore.Shared.Commands;
using System;

namespace NordicStore.Domain.Commands.Results
{
    public abstract class CommandResult : ICommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
