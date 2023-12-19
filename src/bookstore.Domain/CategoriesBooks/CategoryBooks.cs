using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace bookstore.CategoriesBooks;
public class CategoryBooks : AuditedAggregateRoot<Guid>
{
    public Guid BookId { get; set; }
    public Guid CategoryId { get; set; }

}
