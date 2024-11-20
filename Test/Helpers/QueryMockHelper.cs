using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helpers
{
    public static class QueryMockHelper
    {
        public static IQueryable<T> QueryMock<T>(IEnumerable<T> data)
        {
            return data.AsQueryable();
        }
    }
}
