using System.Threading.Tasks;

namespace bookstore.Data;

public interface IbookstoreDbSchemaMigrator
{
    Task MigrateAsync();
}
