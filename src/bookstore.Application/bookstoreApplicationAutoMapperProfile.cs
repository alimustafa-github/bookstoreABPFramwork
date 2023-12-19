using AutoMapper;
using bookstore.Authors;
using bookstore.Books;
using bookstore.Categories;

namespace bookstore;

public class bookstoreApplicationAutoMapperProfile : Profile
{
    public bookstoreApplicationAutoMapperProfile()
    {
		/* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
		CreateMap<Book, BookDto>();
		CreateMap<CreateUpdateBookDto, Book>();
		CreateMap<Author, AuthorDto>();
		CreateMap<Author, AuthorLookupDto>();
		CreateMap<Category,CategoryDto>();
		CreateMap<CreateUpdateCategoryDto, Category>();

	}
}
