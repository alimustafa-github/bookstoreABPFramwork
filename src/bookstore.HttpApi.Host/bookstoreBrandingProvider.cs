using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace bookstore;

[Dependency(ReplaceServices = true)]
public class bookstoreBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "bookstore";
}
