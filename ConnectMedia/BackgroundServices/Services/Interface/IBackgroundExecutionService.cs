

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectMedia.BackgroundServices.Services.Interface
{
    public interface IBackgroundExecutionService
    {

        Task ActivatePlayList();
        Task DeActivatePlayList();
    }
}
