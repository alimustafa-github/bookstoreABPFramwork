using bookstore.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace bookstore.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(bookstoreEntityFrameworkCoreModule),
    typeof(bookstoreApplicationContractsModule)
    )]
public class bookstoreDbMigratorModule : AbpModule
{
}
