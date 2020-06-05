using ConnectMedia.Common.Database;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectMedia.Common.Scheduler
{
    public class DeactivateClassified
    {
        private ConnectMediaDB _db;
        public async Task Execute(IJobExecutionContext context)
        {

            var Notices = _db.Classified.Where(x => x.IsDel == false && x.IsActive == true && x.End < DateTime.Now).ToList();
            Notices.ForEach(a => { a.IsActive = false; a.UpdatedOn = DateTime.Now; });
            int IsSucess = _db.SaveChanges();
        }
    }
}
