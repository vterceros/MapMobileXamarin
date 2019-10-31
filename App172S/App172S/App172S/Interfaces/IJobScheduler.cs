using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App172S.Interfaces
{
    public interface IJobScheduler
    {
        void ScheduleJob();
        void CancelAllJobs();
    }
}
