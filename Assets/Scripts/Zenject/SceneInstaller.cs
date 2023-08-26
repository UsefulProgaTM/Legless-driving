using LeglessDriving;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IHorizontalInput>().To<PlayerInput>().AsSingle();
        Container.Bind<Player>().AsSingle().NonLazy();
        Container.Bind<SmoothRotation>().AsTransient().NonLazy();
    }
}