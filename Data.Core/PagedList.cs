using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Core
{
    public class PagedList<T> : List<T>, IPagedList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        public int PageIndex
        {
            get;
            private set;
        }
        public int PageSize
        {
            get;
            private set;
        }
        public int TotalCount
        {
            get;
            private set;
        }
        public int TotalPages
        {
            get;
            private set;
        }
        public bool HasPreviousPage
        {
            get
            {
                return this.PageIndex > 0;
            }
        }
        public bool HasNextPage
        {
            get
            {
                return this.PageIndex + 1 < this.TotalPages;
            }
        }
        public PagedList(IQueryable<T> oSource, int intPageIndex, int intPageSize, string sidx, string sord)
        {
            int num = oSource.Count<T>();
            this.TotalCount = num;
            this.TotalPages = num / intPageSize;
            if (num % intPageSize > 0)
            {
                this.TotalPages++;
            }
            this.PageSize = intPageSize;
            this.PageIndex = intPageIndex;
            if (!string.IsNullOrEmpty(sidx))
            {
                base.AddRange(DynamicQueryable.OrderBy<T>(oSource, sidx + " " + sord, new object[0]).Skip(intPageIndex * intPageSize).Take(intPageSize).ToList<T>());
            }
            else
            {
                base.AddRange(oSource.Skip(intPageIndex * intPageSize).Take(intPageSize).ToList<T>());
            }
        }
        public PagedList(IList<T> oSource, int intPageIndex, int intPageSize)
        {
            this.TotalCount = oSource.Count<T>();
            this.TotalPages = this.TotalCount / intPageSize;
            if (this.TotalCount % intPageSize > 0)
            {
                this.TotalPages++;
            }
            this.PageSize = intPageSize;
            this.PageIndex = intPageIndex;
            base.AddRange(oSource.Skip(intPageIndex * intPageSize).Take(intPageSize).ToList<T>());
        }
        public PagedList(IEnumerable<T> oSource, int intPageIndex, int intPageSize, int intTotalCount)
        {
            this.TotalCount = intTotalCount;
            this.TotalPages = this.TotalCount / intPageSize;
            if (this.TotalCount % intPageSize > 0)
            {
                this.TotalPages++;
            }
            this.PageSize = intPageSize;
            this.PageIndex = intPageIndex;
            base.AddRange(oSource);
        }
    }
}
