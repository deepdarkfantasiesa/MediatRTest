using MediatR;

namespace MediatRTest.Applications.Commands
{
    public class MyRequestCommand:IRequest<bool>
    {
        public MyRequestCommand()
        {
            
        }
    }
}
