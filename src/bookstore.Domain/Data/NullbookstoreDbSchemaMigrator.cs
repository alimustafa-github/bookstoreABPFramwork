using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace bookstore.Data;

/* This is used if database provider does't define
 * IbookstoreDbSchemaMigrator implementation.
 */
public class NullbookstoreDbSchemaMigrator : IbookstoreDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
