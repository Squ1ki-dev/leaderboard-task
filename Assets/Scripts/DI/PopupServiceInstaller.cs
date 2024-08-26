using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

using SimplePopupManager;

public class PopupServiceInstaller : MonoInstaller
{
    [SerializeField] private Canvas canvas;

    public override void InstallBindings()
    {
        Container
            .Bind<Canvas>()
            .FromInstance(canvas)
            .AsSingle();

        Container
            .Bind<IAssetLoader>()
            .To<AddressableAssetLoader>()
            .AsTransient();

        Container
            .Bind<PopupFactory>()
            .AsTransient();

        Container
            .Bind<PopupService>()
            .AsSingle();
    }
}
