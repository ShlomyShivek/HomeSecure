using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace HomeSecure.Data.Interfaces
{
    public interface IDataBaseExtender
    {
        void OnModelCreating(DbModelBuilder modelBuilder);
    }
}
