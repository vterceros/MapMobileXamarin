using System;
using Xamarin.Forms;
using App172S.Views;
using Xamarin.Forms.Xaml;
using Plugin.Settings;
using App172S.ViewModels;
using System.Threading.Tasks;
using App172S.Services;
using App172S.Models.Negocio;
using App172S.Interfaces;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace App172S
{
	public partial class App : Application
	{
        //public static string ServiceIP = "http://192.168.0.11:40355";//"http://10.100.220.128:40355";
        public static string ServiceIP = "https://appdeltacagoservicio.azurewebsites.net";



        public static string ImeiTelefono
        {
            get
            {
                return CrossSettings.Current.GetValueOrDefault("DeltaTelefonoRegistrado", "");
            }
            set
            {
                CrossSettings.Current.AddOrUpdateValue("DeltaTelefonoRegistrado", value);
            }
        }
        public App ()
        {
			InitializeComponent();            

            //MainPage = new MainPage();                        
            //ImeiTelefono = "";
            if (string.IsNullOrEmpty(ImeiTelefono))
            {
                var deviceInfo = Xamarin.Forms.DependencyService.Get<Interfaces.IDeviceInfo>();
                //ImeiTelefono = deviceInfo.GetImei();
                this.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainViewModel.Instance().MapsPrevious = new MapsPreviousViewModel();
                this.MainPage = this.MainPage = new NavigationPage(new MapsPrevious());
            }

        }

        #region Location Service


       

        #endregion

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        public async static void ShowAlert(string title, string content)
        {
            await Current.MainPage.DisplayAlert(title, content, "Aceptar");
        }
    }
}
