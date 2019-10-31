using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.App.Job;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App172S.Droid.Scheduler
{
    public class ReadLocationServiceSchedulerUtil
    {
        public static void Create(Context context)
        {
            #region Scheduler

            var jobBuilder = ReadLocationSchedulerFactory.CreateJobBuilderUsingJobId<ReadLocationScheduler>(context,152);
            jobBuilder.SetPeriodic(30 * 60 * 1000);
            jobBuilder.SetPersisted(true);
            jobBuilder.SetBackoffCriteria(120 * 1000, BackoffPolicy.Linear);
            //jobBuilder.SetRequiresBatteryNotLow(false);
            //jobBuilder.SetRequiresStorageNotLow(false);
            //jobBuilder.SetRequiresDeviceIdle(false);
            var jobInfo = jobBuilder.Build();

            JobScheduler jobScheduler = (JobScheduler)context.GetSystemService("jobscheduler");
            jobScheduler.Cancel(152);
            var scheduleResult = jobScheduler.Schedule(jobInfo);

            if (JobScheduler.ResultSuccess == scheduleResult)
            {
                //Snackbar.Make(FindViewById(Android.Resource.Id.Content), "Delca Cargo, Reading Location", Snackbar.LengthShort);
            }
            else
            {
                //Snackbar.Make(FindViewById(Android.Resource.Id.Content), "Delca Cargo, Failed to read Location", Snackbar.LengthShort);
            }

            #endregion
        }
    }
}