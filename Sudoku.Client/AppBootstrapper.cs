using Caliburn.Micro;
using Sudoku.Client.Main;
using System;
using System.Collections.Generic;

namespace Sudoku.Client
{
    public class AppBootstrapper : BootstrapperBase
    {
        #region Constructor

        public AppBootstrapper()
        {
			// start Caliburn.Micro framework
            Initialize();
        }

        #endregion Constructor

        #region Members

        private SimpleContainer container;

        #endregion Members

        #region Methods

        protected override void Configure()
        {
			// initialize container

            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.Singleton<IShell, MainViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            object instance = null;
			
            try
            {
				// use GetInstance from container
                instance = container.GetInstance(service, key);
            }
            catch (Exception/* ex*/)
            {
            }

            if (instance == null)
            {
                throw new InvalidOperationException("Could not locate any instances.");
            }
            
			return instance;
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
			// use GetAllInstances from container
            var instances = container.GetAllInstances(service);
			
			return instances;
        }

        protected override void BuildUp(object instance)
        {
			// use BuildUp from container
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
			// display main view
            DisplayRootViewFor<IShell>();
        }

        #endregion Methods
    }
}