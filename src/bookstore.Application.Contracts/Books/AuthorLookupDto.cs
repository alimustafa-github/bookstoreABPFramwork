﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace bookstore.Books;
public class AuthorLookupDto : EntityDto<Guid>
{
	public string Name { get; set; }
}