using bookstore.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace bookstore.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class bookstoreController : AbpControllerBase
{
    protected bookstoreController()
    {
        LocalizationResource = typeof(bookstoreResource);
    }
}
