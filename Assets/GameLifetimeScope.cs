using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<Equipment>(Lifetime.Transient).AsImplementedInterfaces();

        builder.Register<PlayerFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<PlayerProvider>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<Skills>(Lifetime.Singleton).AsImplementedInterfaces();
        
        builder.RegisterEntryPoint<EntryPoint>();
    }
}

public class EntryPoint : IInitializable
{
    private readonly IPlayer _player;
    private IObjectResolver _container;

    public EntryPoint(IPlayerFactory playerFactory , IObjectResolver container)
    {
        _container = container;
        _player = playerFactory.Create("User", 100, 3); // создали игрока до начали игровых действий
    }

    public void Initialize()
    {
        var gameLoopObj = new GameObject("GameLoop");
        var gameLoop = gameLoopObj.AddComponent<GameLoop>();
        //Создали игровой условный луп через контейнер для того что бы в него прокинулись зависимости
        _container.InjectGameObject(gameLoopObj);
    }
}
