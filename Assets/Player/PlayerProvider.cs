public interface IPlayerProvider
{
    IPlayer Player { get; }
    void SetPlayer(IPlayer player);
}

public class PlayerProvider : IPlayerProvider
{
    public IPlayer Player { get; private set; }
    
    public void SetPlayer(IPlayer player) => Player ??= player;
    
}