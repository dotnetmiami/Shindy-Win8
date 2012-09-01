namespace ShindyUI.App
{
    using System;
    using System.Threading.Tasks;

    using GalaSoft.MvvmLight.Ioc;

    using Microsoft.Practices.ServiceLocation;

    using ShindyUI.App.Common;

    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Activation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class App : Application
    {
        #region Constructors

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        #endregion

        #region Methods

        protected async override void OnSearchActivated(SearchActivatedEventArgs args)
        {
            await this.EnsureMainPageActivatedAsync(args);

            if (string.Empty == args.QueryText)
            {
                MainPage.Current.Frame.Navigate(typeof(SearchPage), args.QueryText);
            }
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
            {
                Window.Current.Activate();
                return;
            }

            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                await SuspensionManager.RestoreAsync();
            }

            var rootFrame = new Frame();
            SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

            if (rootFrame.Content == null)
            {
                if (!rootFrame.Navigate(typeof(MainPage), "AllEvents"))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }

        private async Task EnsureMainPageActivatedAsync(IActivatedEventArgs args)
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                await SuspensionManager.RestoreAsync();

            }

            if (Window.Current.Content == null)
            {
                var rootFrame = new Frame();
                rootFrame.Navigate(typeof(MainPage));
                Window.Current.Content = rootFrame;
            }

            Window.Current.Activate();
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        #endregion
    }
}
