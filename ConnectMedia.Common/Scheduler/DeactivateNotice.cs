using ConnectMedia.Common.Database;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectMedia.Common.Scheduler
{
    class DeactivateNotice : IJob
    {
        private ConnectMediaDB _db;
        public async Task Execute(IJobExecutionContext context)
        {

            var Notices = _db.Notice.Where(x => x.IsDel == false && x.IsActive == true && x.Expire == true && x.EndDate < DateTime.Now && x.EndTime < DateTime.Now.TimeOfDay).ToList();
            Notices.ForEach(a => { a.IsActive = false; a.UpdatedOn = DateTime.Now; });
            int IsSucess = _db.SaveChanges();
        }

    }    
}
