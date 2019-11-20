using RemoteControl.DesktopClient.Core;
using System;
using System.Windows.Forms;
using Unity;

namespace RemoteControl.DesktopClient.Startup
{
    internal static class Program
    {
        private static readonly Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() => new UnityContainer());

        public static IUnityContainer Container => container.Value;

        [STAThread]
        private static void Main()
        {
            RegisterDependencies();
            StartApplication();
        }

        private static void RegisterDependencies()
        {
        }

        private static void StartApplication()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Container.Resolve<MainForm>());
        }
    }
}