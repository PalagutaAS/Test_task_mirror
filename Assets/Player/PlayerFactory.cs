public interface IPlayerFactory
{
    IPlayer Create(string nickname, int health, int lives);
}

public class PlayerFactory : IPlayerFactory
{
    private readonly IEquipment _equipment;
    private readonly IPlayerProvider _provider;
    private readonly ISkills _skills;

    public PlayerFactory(IEquipment equipment, IPlayerProvider provider, ISkills skills)
    {
        _equipment = equipment;
        _provider = provider;
        _skills = skills;
    }

    public IPlayer Create(string nickname, int health, int lives)
    {
        var player = new Player(nickname, health, lives, _equipment, _skills);
        _provider.SetPlayer(player);
        return player;
    }
}