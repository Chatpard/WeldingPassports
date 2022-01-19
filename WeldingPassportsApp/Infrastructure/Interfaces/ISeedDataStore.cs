using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces
{
    public interface ISeedDataStore
    {
        public T GetSeedData<T>();
    }
}
