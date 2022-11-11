﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.Events
{
    public interface IHandle<T>
    {
        void Handle(T message);
    }
}
