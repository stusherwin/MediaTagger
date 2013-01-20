namespace MediaTagger.Mvc.IOC
{
    public interface IProvider<T>
    {
        T Get();
    }
}
