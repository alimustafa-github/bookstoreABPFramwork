﻿using bookstore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace bookstore.Authors;
public class EfCoreAuthorRepository
	: EfCoreRepository<bookstoreDbContext, Author, Guid>,
		IAuthorRepository
{
	public EfCoreAuthorRepository(
		IDbContextProvider<bookstoreDbContext> dbContextProvider)
		: base(dbContextProvider)
	{
	}

	public async Task<Author> FindByNameAsync(string name)
	{
		var dbSet = await GetDbSetAsync();
		return await dbSet.FirstOrDefaultAsync(author => author.Name == name);
	}

	public async Task<List<Author>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
	{
		var dbSet = await GetDbSetAsync();
		return await dbSet
			.WhereIf(!filter.IsNullOrWhiteSpace(), author => author.Name.Contains(filter))
			.OrderBy(sorting)
			.Skip(skipCount)
			.Take(maxResultCount)
			.ToListAsync();
	}
}