using Ardalis.Specification;
using Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class PagedOrderSpecification : Specification<Order>
    {
        public PagedOrderSpecification(int pageSize, int pageNumber, string? orderNumber, string? orderLocation)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(orderNumber))
            {
                Query.Search(x => x.ORDNBR, "%" + orderNumber + "%");
            }
            if (!string.IsNullOrEmpty(orderLocation))
            {
                Query.Search(x => x.ORGLOC, "%" + orderLocation + "%");
            }
        }
    }
}
