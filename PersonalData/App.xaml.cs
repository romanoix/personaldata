using Ninject;
using PersonalData.IServices;
using PersonalData.Services;
using System.Windows;

namespace PersonalData
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            this.container = new StandardKernel();
            container.Bind<IAddressService>().To<AddressService>().InTransientScope();
            container.Bind<IDeleteService>().To<DeleteService>().InTransientScope();
            container.Bind<IPersonService>().To<PersonService>().InTransientScope();
            container.Bind<IPhoneService>().To<PhoneService>().InTransientScope();
        }

        private void ComposeObjects()
        {
            Current.MainWindow = this.container.Get<MainView>();
            Current.MainWindow.Title = "PersonData";
        }
    }
}
