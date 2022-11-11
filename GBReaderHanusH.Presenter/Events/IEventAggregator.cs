using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.Events
{
    public interface IEventAggregator
    {
        void Publish<T>(T message);
        void Subscribe(object subscriber);
        void Unsubscribe(object subscriber);
    }
}
