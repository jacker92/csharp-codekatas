using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Console
{
    public static class Messages
    {
        public const string InvalidArguments = "Invalid arguments, please enter ? for instructions.";
        public const string Instructions = @"
To add todo item: task -t 'task name' -d 'due date in format dd-mm-yyyy'
To mark todo item as complete: -c 'task id'";

    }
}
