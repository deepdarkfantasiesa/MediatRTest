using MediatR;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace MediatRTest.Applications
{
    public class CustomExceptions
    {
        public ConcurrentBag<Exception> Exceptions {  get; set; }
        public CustomExceptions()
        {

            var stackTrace = new StackTrace();
            var callingClass = stackTrace.GetFrame(1).GetMethod().DeclaringType;

            Exceptions = new ConcurrentBag<Exception>();
        }
    }

}
