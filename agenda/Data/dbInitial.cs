using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agenda.Data;

namespace agenda.Data
{
    public static class dbInitial
    {
        public static void Initialize(dbCtx context)
        {
            context.Database.EnsureCreated();
        }
    }
}
