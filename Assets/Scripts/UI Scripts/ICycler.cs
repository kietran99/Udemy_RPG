using System;

namespace Cycler
{
    public interface ICycler<T>
    {
        T CurrPos { get; set; }
        Action<T> OnCycle { get; set; }
    }
}