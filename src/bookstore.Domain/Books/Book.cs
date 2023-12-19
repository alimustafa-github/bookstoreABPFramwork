using bookstore.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;

namespace bookstore.Books;
public class Book : AuditedAggregateRoot<Guid>, IMultiTenant
{
	public string Name { get; set; }

	public BookType Type { get; set; }

	public DateTime PublishDate { get; set; }

	public float Price { get; set; }
	public Guid AuthorId { get; set; }

	public Guid? TenantId { get; set; }


}


