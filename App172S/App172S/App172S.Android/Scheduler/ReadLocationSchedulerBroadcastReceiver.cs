using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

//[assembly: UsesPermission(Manifest.Permission.ReceiveBootCompleted)]
namespace App172S.Droid.Scheduler
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    public class ReadLocationSchedulerBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            ReadLocationServiceSchedulerUtil.Create(context);
        }
    }
}