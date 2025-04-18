﻿using PharmacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Repositories.Interfaces
{
    public interface IPendingOrderRepository
    {
        Task<IEnumerable<PendingOrder>> LoadPendingOrderInfo();
    }
}
