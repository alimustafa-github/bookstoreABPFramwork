using System;
using System.Collections.Generic;
using System.Text;
using bookstore.Localization;
using Volo.Abp.Application.Services;
using Volo.Abp.MultiTenancy;

namespace bookstore;

/* Inherit your application services from this class.
 */
public abstract class bookstoreAppService : ApplicationService
{
    protected bookstoreAppService()
    {
        LocalizationResource = typeof(bookstoreResource);
    }
}
