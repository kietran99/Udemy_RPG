using System;

namespace Cycler
{
    public interface ICycler<T>
    {
        T Current { get; }
        Action<T> OnCycle { get; set; }
        void GoNext();
        void GoPrev();
    }
}