using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace bookstore.Authors;
public class GetAuthorListDto : PagedAndSortedResultRequestDto
{
	public string? Filter { get; set; }
}
