namespace MonoGame.Extras.Ecs
{
    public interface IComponentMapperService
    {
        ComponentMapper<T> GetMapper<T>() where T : IEntityComponent;
    }
}
