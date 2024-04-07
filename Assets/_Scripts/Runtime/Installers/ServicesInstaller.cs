using UnityEngine;
using Zenject;

public class ServicesInstaller : MonoInstaller
{
    [SerializeField] private JoyStick _joyStick;
    [SerializeField] private LevelLoader _levelLoader;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<UserInput>().AsSingle().NonLazy();
        Container.Bind<JoyStick>().FromInstance(_joyStick).AsSingle().NonLazy();
        Container.Bind<LevelLoader>().FromInstance(_levelLoader).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameStarter>().AsSingle().NonLazy();
    }
}