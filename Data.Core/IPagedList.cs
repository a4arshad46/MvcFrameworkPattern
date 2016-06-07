using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Core
{
    public interface IPagedList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        int PageIndex
        {
            get;
        }
        int PageSize
        {
            get;
        }
        int TotalCount
        {
            get;
        }
        int TotalPages
        {
            get;
        }
        bool HasPreviousPage
        {
            get;
        }
        bool HasNextPage
        {
            get;
        }
    }
}
