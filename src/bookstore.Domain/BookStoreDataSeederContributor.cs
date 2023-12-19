using bookstore.Authors;
using bookstore.Books;
using bookstore.CategoriesBooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.TenantManagement;

namespace bookstore;
public class BookStoreDataSeederContributor
	: IDataSeedContributor, ITransientDependency
{
	private readonly IRepository<CategoryBooks,Guid> _categoryBooksRepository;
	private readonly IRepository<Book, Guid> _bookRepository;
	private readonly IAuthorRepository _authorRepository;
	private readonly AuthorManager _authorManager;
	private readonly ITenantManager _tenantManager;
	private readonly ITenantRepository _tenantRepository;

	public BookStoreDataSeederContributor(
		IRepository<Book, Guid> bookRepository,
		IAuthorRepository authorRepository,
		AuthorManager authorManager,
		ITenantManager tenantManager,
		ITenantRepository tenantRepository,
		IRepository<CategoryBooks, Guid> categoryBooksRepository)
	{
		_bookRepository = bookRepository;
		_authorRepository = authorRepository;
		_authorManager = authorManager;
		_tenantManager = tenantManager;
		_tenantRepository = tenantRepository;
		_categoryBooksRepository = categoryBooksRepository;
	}

	public async Task SeedAsync(DataSeedContext context)
	{


		
		
		if (await _bookRepository.GetCountAsync() > 0)
		{
			return;
		}

		//await _tenantRepository.InsertAsync(await _tenantManager.CreateAsync("Microsoft"), autoSave: true);
		//await _tenantRepository.InsertAsync(await _tenantManager.CreateAsync("Emenu"), autoSave: true);



		long count = await _bookRepository.CountAsync();

		var orwell = await _authorRepository.InsertAsync(
			await _authorManager.CreateAsync(
				"George Orwell",
				new DateTime(1903, 06, 25),
				"Orwell produced literary criticism and poetry, fiction and polemical journalism; and is best known for the allegorical novella Animal Farm (1945) and the dystopian novel Nineteen Eighty-Four (1949)."
			)
		);

		var douglas = await _authorRepository.InsertAsync(
			await _authorManager.CreateAsync(
				"Douglas Adams",
				new DateTime(1952, 03, 11),
				"Douglas Adams was an English author, screenwriter, essayist, humorist, satirist and dramatist. Adams was an advocate for environmentalism and conservation, a lover of fast cars, technological innovation and the Apple Macintosh, and a self-proclaimed 'radical atheist'."
			)
		);

		await _bookRepository.InsertAsync(
			new Book
			{
				AuthorId = orwell.Id, // SET THE AUTHOR
				Name = "1984",
				Type = BookType.Dystopia,
				PublishDate = new DateTime(1949, 6, 8),
				Price = 19.84f,
				TenantId = Guid.NewGuid()
			},
			autoSave: true
		);

		await _bookRepository.InsertAsync(
			new Book
			{
				AuthorId = douglas.Id, // SET THE AUTHOR
				Name = "The Hitchhiker's Guide to the Galaxy",
				Type = BookType.ScienceFiction,
				PublishDate = new DateTime(1995, 9, 27),
				Price = 42.0f,
				TenantId = Guid.NewGuid()
			},
			autoSave: true
		);
	}
}
