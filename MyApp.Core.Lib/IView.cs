using System;

namespace MyApp.Core
{
    public interface IView
    {
        event EventHandler Disposed;
    }
}
