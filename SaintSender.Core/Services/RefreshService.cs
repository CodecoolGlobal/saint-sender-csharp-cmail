﻿using System;
using SaintSender.Core.Models;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaintSender.Core.Services
{
    class RefreshService
    {
        public void Refresh()
        {
            Timer refresher = new Timer(_ => Authentication.GetInbox(), null, 0, 2000);
        }
    }
}
