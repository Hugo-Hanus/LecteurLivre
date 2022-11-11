using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.Events
{
    public class EventAggregator : IEventAggregator
    {
        private readonly List<object> subscribers = new();
        public void Publish<T>(T message)
        {
            foreach(var subscriber in subscribers)
            {
                var handler = subscriber as IHandle<T>;
                if (handler != null)
                {
                    handler.Handle(message);
                }
            }
        }



        public void Subscribe(object subscriber) => subscribers.Add(subscriber);

        public void Unsubscribe(object subscriber)=>subscribers.Remove(subscriber);
        


    }
}
