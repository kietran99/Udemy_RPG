using System;

namespace Cycler
{
    public interface ICycler<T>
    {
        T Current { get; set; }
        Action<T> OnCycle { get; set; }
    }
}