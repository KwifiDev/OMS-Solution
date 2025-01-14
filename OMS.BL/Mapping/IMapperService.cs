namespace OMS.BL.Mapping
{
    public interface IMapperService
    {
        TDestination Map<TSource, TDestination>(TSource source);
        IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source);
        void Map<TSource, TDestination>(TSource source, TDestination destination);

    }
}
