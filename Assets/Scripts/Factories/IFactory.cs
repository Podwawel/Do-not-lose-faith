public interface IFactory<T, F>
{
    T Create(T objectOfType, F secondArg);
}
