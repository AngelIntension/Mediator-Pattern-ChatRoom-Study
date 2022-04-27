using System;
using System.Collections.Generic;

namespace Mediator
{
    public class Mediator : IMediator
    {
        private readonly List<IColleague> colleagues;

        public Mediator(params IColleague[] colleagues)
        {
            if (colleagues == null) throw new ArgumentNullException(nameof(colleagues));
            this.colleagues = new List<IColleague>(colleagues);
        }

        public void Send(Message message)
        {
            colleagues.ForEach(c => c.ReceiveMessage(message));
        }
    }
}
