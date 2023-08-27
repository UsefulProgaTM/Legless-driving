using LeglessDriving;
using Zenject;
using UnityEngine;

public class SceneInstaller : MonoInstaller
{
    [SerializeField]
    private GasPedal _gasPedal;
    [SerializeField]
    private BrakePedal _brakePedal;
    [SerializeField]
    private ClutchPedal _clutchPedal;
    [SerializeField]
    private PlayerHandbrake _handbrake;
    [SerializeField]    
    private PlayerShifter _shifter;



    public override void InstallBindings()
    {
        Container.Bind<Player>().AsSingle().NonLazy();
        Container.Bind<SmoothRotation>().AsTransient().NonLazy();

        #region Car parts
        Container.Bind<IBody>().To<Body>().AsTransient().NonLazy();
        Container.Bind<IEngine>().To<Engine>().AsTransient().NonLazy();
        Container.Bind<IBrakes>().To<Breaks>().AsTransient().NonLazy();
        Container.Bind<IHandling>().To<Handling>().AsTransient().NonLazy();
        Container.Bind<ITransmission>().To<Transmission>().AsTransient().NonLazy();
        Container.Bind<IShifter>().FromInstance(_shifter).AsSingle();
        #endregion

        #region Input
        Container.Bind<IClutch>().FromInstance(_clutchPedal).AsSingle();
        Container.Bind<IHorizontalInput>().To<PlayerInput>().AsSingle();
        Container.Bind<IHandbrake>().FromInstance(_handbrake).AsSingle();
        Container.Bind<IGasInput>().FromInstance(_gasPedal).AsSingle();
        Container.Bind<IBreakInput>().FromInstance(_brakePedal).AsSingle();
        #endregion 
    }
}