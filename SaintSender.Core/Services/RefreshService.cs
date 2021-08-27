using SaintSender.Core.Models;
using System.Threading;

namespace SaintSender.Core.Services
{
    internal class RefreshService
    {
        public void Refresh()
        {
            Timer refresher = new Timer(_ => Authentication.GetInbox(), null, 0, 2000);
        }
    }
}
