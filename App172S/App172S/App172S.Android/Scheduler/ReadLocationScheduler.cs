using System;
using System.Collections.Generic;
using Android.App.Job;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;

using JobSchedulerType = Android.App.Job.JobScheduler;
using Plugin.Geolocator;
using System.Threading.Tasks;
using App172S.Helpers;

namespace App172S.Droid.Scheduler
{
    [Service(Exported = true, Permission = "android.permission.BIND_JOB_SERVICE")]
    public class ReadLocationScheduler : JobService
    {
        public override void OnCreate()
        {
            base.OnCreate();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override StartCommandResult OnStartCommand(Intent intent, Android.App.StartCommandFlags flags, int startId)
        {
            //var callback = (Messenger)intent.GetParcelableExtra("messenger");
            //var m = Message.Obtain();
            //m.What = MainActivity.MessageServiceObj;
            //m.Obj = this;
            //try
            //{
            //    callback.Send(m);
            //}
            //catch (RemoteException e)
            //{
            //    Log.Error(Tag, e, "Error passing service object back to activity.");
            //}
            return StartCommandResult.NotSticky;
        }

        public override bool OnStartJob(JobParameters args)
        {
            //jobParamsMap.Add(args);
            //if (owner != null)
            //{
            //    owner.OnReceivedStartJob(args);
            //}

            Task.Run(async() =>
            {
                try
                {
                    //CrossGeolocator.Current.DesiredAccuracy = 50;
                    //var position = await CrossGeolocator.Current.GetPositionAsync(timeout: TimeSpan.FromSeconds(5));
                    //if (position != null)
                    //{
                    //    var viajeId = Plugin.Settings.CrossSettings.Current.GetValueOrDefault("DeltaViajeId", 0);
                    //    await Services.ApiService.Post<int>(string.Format("{0}/Api/UsuarioViajeDetalle/PostDetalle?viajeId={1}&lat={2}&lon={3}", App.ServiceIP, viajeId, position.Latitude, position.Longitude));
                    //}

                    await Utils.PostLocation();
                }
                catch (Exception ex)
                {
                }
                //JobFinished(null, false);
            });
            //ReadLocationServiceSchedulerUtil.Create(Android.App.Application.Context);
            return true;
        }

        public override bool OnStopJob(JobParameters args)
        {
            //jobParamsMap.Remove(args);
            //if (owner != null)
            //{
            //owner.OnReceivedStopJob();
            //}
            return true;
        }

        /** Send job to the JobScheduler. */
        public void ScheduleJob(JobInfo t)
        {
            var tm = (JobSchedulerType)GetSystemService(Context.JobSchedulerService);
            var status = tm.Schedule(t);
            //Log.Info(Tag, "Scheduling job: " + (status == JobSchedulerType.ResultSuccess ? "Success" : "Failure"));
        }

        /**
	     * Called when Task Finished button is pressed. 
	     */
        public bool CallJobFinished()
        {
            //if (jobParamsMap.Count == 0)
            //{
            //    return false;
            //}
            //else
            //{
            //    var args = jobParamsMap[0];
            //    jobParamsMap.Remove(args);
            //    JobFinished(args, false);
                return true;
            //}
        }
    }
}