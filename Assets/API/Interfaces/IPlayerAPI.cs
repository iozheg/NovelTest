namespace API.Interfaces
{
    public interface IPlayerAPI<T> : IDataHandler<T> where T : new()
    {
        string GetPlayerId();
    }
}