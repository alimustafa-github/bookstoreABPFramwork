using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace bookstore.Books;
public class BookDto : AuditedEntityDto<Guid>
{
	public string Name { get; set; }

    //public string Category { get; set; }

    public List<string> Categories { get; set; }
    public Guid AuthorId { get; set; }
	public string AuthorName { get; set; }

	public BookType Type { get; set; }

	public DateTime PublishDate { get; set; }

	public float Price { get; set; }
	public Guid? TenantId { get; set; }

}
