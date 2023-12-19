using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace bookstore.Categories;
public class CategoryAppService : CrudAppService<Category, CategoryDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCategoryDto>
								  , ICategoryAppService
{
	public CategoryAppService(IRepository<Category, Guid> repository) : base(repository)
	{
	}

	public override async Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
	{
		Category category = ObjectMapper.Map<CreateUpdateCategoryDto,Category>(input);

		category = await Repository.InsertAsync(category);

		CategoryDto categoryDto = ObjectMapper.Map<Category,CategoryDto>(category);

		return categoryDto;
	}



}
