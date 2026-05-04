using VContainer;
using VContainer.Unity;

public class LifetimeScopeNetworkInstaller : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<NetworkMessageService>(Lifetime.Singleton)
            .As<INetworkMessageService>();
    }

}