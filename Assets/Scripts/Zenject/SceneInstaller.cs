using LeglessDriving;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IMovementInput>().To<PlayerInput>().AsSingle();
        Container.Bind<Player>().AsSingle().NonLazy();
    }
}