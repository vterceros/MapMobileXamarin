using App172S.Interfaces;
using App172S.Models.Negocio;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App172S.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : TabbedPage
	{
		public MainPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);            
        }
        public MainPage(del_UsuarioViaje viaje, del_UsuarioViajeDetalle ultimoPunto, List<del_UsuarioViajeDetalle> detalle)
        {
            Children.Add(new Maps(viaje, ultimoPunto, detalle));
            Children[0].Title = "Viaje";
            Children.Add(new DocsPage());
            Children[1].Title = "Documentos";
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //var dep = DependencyService.Get<IJobScheduler>();
            //dep.CancelAllJobs();
            //dep.ScheduleJob();
        }
    }
}
   