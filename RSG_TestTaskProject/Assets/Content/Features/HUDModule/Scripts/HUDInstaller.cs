using Zenject;

namespace Content.Features.HUDModule
{
    public class HUDInstaller : Installer<HUDInstaller> {
        public override void InstallBindings() {
            Container.Bind<HUDController>()
                     .AsSingle();
        }
    }
}