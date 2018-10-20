namespace Tourenbuch {
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using Caliburn.Micro;
    using Tourenbuch.ServiceReference1;

    public class AppBootstrapper : BootstrapperBase {
        SimpleContainer container;

        public AppBootstrapper() {
            Initialize();
        }

        protected override void Configure() {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.Singleton<StartPageViewModel, StartPageViewModel>();
            container.PerRequest<IShell, ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key) {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            //If you use WindowState = Maximized then set MaxHeight to make sure it does not go over the task bar.

            double screen_width = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screen_height = System.Windows.SystemParameters.PrimaryScreenHeight;

            Dictionary<string, object> window_settings = new Dictionary<string, object>();

            window_settings.Add("WindowState", WindowState.Maximized);

            DisplayRootViewFor<IShell>(window_settings);
        }
    }
}