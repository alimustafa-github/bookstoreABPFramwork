using bookstore.Authors;
using bookstore.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using bookstore.Categories;
using bookstore.CategoriesBooks;

namespace bookstore.Books;

[Authorize(bookstorePermissions.Books.Default)]
public class BookAppService :
	CrudAppService<
		Book, //The Book entity
		BookDto, //Used to show books
		Guid, //Primary key of the book entity
		PagedAndSortedResultRequestDto, //Used for paging/sorting
		CreateUpdateBookDto>, //Used to create/update a book
	IBookAppService //implement the IBookAppService
{


	private readonly IRepository<CategoryBooks> _categoryBooksRepository;
	private readonly IAuthorRepository _authorRepository;
	private readonly IRepository<Category, Guid> _categoryRepository;

	public BookAppService(
		IRepository<Book, Guid> repository,
		IAuthorRepository authorRepository,
		IRepository<Category, Guid> categoryRepository,
		IRepository<CategoryBooks> categoryBooksRepository)
		: base(repository)
	{
		_authorRepository = authorRepository;
		GetPolicyName = bookstorePermissions.Books.Default;
		GetListPolicyName = bookstorePermissions.Books.Default;
		CreatePolicyName = bookstorePermissions.Books.Create;
		UpdatePolicyName = bookstorePermissions.Books.Edit;
		DeletePolicyName = bookstorePermissions.Books.Delete;
		_categoryBooksRepository = categoryBooksRepository;
		_categoryRepository = categoryRepository;
	}

	public override async Task<BookDto> GetAsync(Guid id)
	{
		//Get the IQueryable<Book> from the repository
		var queryable = await Repository.GetQueryableAsync();

		//Prepare a query to join books and authors
		var query = from book in queryable
					join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
					where book.Id == id
					select new { book, author };



		//Execute the query and get the book with author
		var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
		if (queryResult == null)
		{
			throw new EntityNotFoundException(typeof(Book), id);
		}

		var bookDto = ObjectMapper.Map<Book, BookDto>(queryResult.book);
		bookDto.AuthorName = queryResult.author.Name;
		return bookDto;
	}

	public override async Task<PagedResultDto<BookDto>> GetListAsync(PagedAndSortedResultRequestDto input)
	{
		//Get the IQueryable<Book> from the repository
		var queryable = await Repository.GetQueryableAsync();

		//Prepare a query to join books and authors
		var query = from book in queryable
					join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
					select new { book, author }; 





		//Paging
		query = query
			.OrderBy(NormalizeSorting(input.Sorting))
			.Skip(input.SkipCount)
			.Take(input.MaxResultCount);


		//Execute the query and get a list
		var queryResult = await AsyncExecuter.ToListAsync(query);

		//Convert the query result to a list of BookDto objects
		var bookDtos = queryResult.Select(x =>
		{
			var bookDto = ObjectMapper.Map<Book, BookDto>(x.book);
			bookDto.AuthorName = x.author.Name;
			return bookDto;
		}).ToList();

		//Get the total count with another query
		var totalCount = await Repository.GetCountAsync();

		return new PagedResultDto<BookDto>(
			totalCount,
			bookDtos
		);


	}

	public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
	{
		var authors = await _authorRepository.GetListAsync();

		return new ListResultDto<AuthorLookupDto>(
			ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors)
		);
	}

	private static string NormalizeSorting(string sorting)
	{
		if (sorting.IsNullOrEmpty())
		{
			return $"book.{nameof(Book.Name)}";
		}

		if (sorting.Contains("authorName", StringComparison.OrdinalIgnoreCase))
		{
			return sorting.Replace(
				"authorName",
				"author.Name",
				StringComparison.OrdinalIgnoreCase
			);
		}

		return $"book.{sorting}";
	}

	public override async Task<BookDto> CreateAsync(CreateUpdateBookDto input)
	{
		Book book = ObjectMapper.Map<CreateUpdateBookDto,Book>(input);

		book = await Repository.InsertAsync(book);


		foreach (var category in input.CategoryIds)
		{
			await _categoryBooksRepository.InsertAsync(new CategoryBooks
			{
				BookId = book.Id,
				CategoryId = category
			});
		}

		return ObjectMapper.Map<Book, BookDto>(book);

	}
}

