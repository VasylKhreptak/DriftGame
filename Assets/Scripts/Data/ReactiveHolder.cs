using UniRx;

namespace Data
{
    public class ReactiveHolder<T>
    {
        public readonly ReactiveProperty<T> Property = new ReactiveProperty<T>();

        public ReactiveHolder() { }

        public ReactiveHolder(T value)
        {
            Property.Value = value;
        }
    }
}