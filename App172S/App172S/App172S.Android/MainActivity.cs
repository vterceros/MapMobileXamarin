using System;
using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using System.Threading.Tasks;
using Android.Content;
using Android.App.Job;
using Android.Widget;

using JobSchedulerType = Android.App.Job.JobScheduler;
using App172S.Droid.Scheduler;
using Android.Support.Design.Widget;

namespace App172S.Droid
{
    [Activity(Label = "Delta Cargo", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {    
        #region props
        public ComponentName serviceComponent;
        #endregion

        #region ctor

        static MainActivity()
        {}

        #endregion

        #region Events

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Xamarin.FormsMaps.Init(this, bundle);
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App172S.App());

            #region Scheduler            

            ReadLocationServiceSchedulerUtil.Create(this);
            //var jobBuilder = this.CreateJobBuilderUsingJobId<ReadLocationScheduler>(152);
            //jobBuilder.SetPeriodic(20 * 60 * 1000);
            //jobBuilder.SetPersisted(true);
            //jobBuilder.SetBackoffCriteria(120 * 1000, BackoffPolicy.Linear);
            //jobBuilder.SetRequiresBatteryNotLow(false);
            //jobBuilder.SetRequiresStorageNotLow(false);
            //jobBuilder.SetRequiresDeviceIdle(false);
            //var jobInfo = jobBuilder.Build();
            //
            //var jobScheduler = (JobScheduler)GetSystemService(JobSchedulerService);
            //jobScheduler.Cancel(152);
            //var scheduleResult = jobScheduler.Schedule(jobInfo);
            //
            //if (JobScheduler.ResultSuccess == scheduleResult)
            //{
            //    Snackbar.Make(FindViewById(Android.Resource.Id.Content), "Delca Cargo, Reading Location", Snackbar.LengthShort);
            //}
            //else
            //{
            //    Snackbar.Make(FindViewById(Android.Resource.Id.Content), "Delca Cargo, Failed to read Location", Snackbar.LengthShort);
            //}

            #endregion

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);      
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        #endregion

    }
}

