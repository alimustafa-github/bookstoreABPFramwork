using bookstore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace bookstore.Permissions;

public class bookstorePermissionDefinitionProvider : PermissionDefinitionProvider
{
	public override void Define(IPermissionDefinitionContext context)
	{
		var bookStoreGroup = context.AddGroup(bookstorePermissions.GroupName, L("Permission:BookStore"));

		var booksPermission = bookStoreGroup.AddPermission(bookstorePermissions.Books.Default, L("Permission:Books"));
		booksPermission.AddChild(bookstorePermissions.Books.Create, L("Permission:Books.Create"));
		booksPermission.AddChild(bookstorePermissions.Books.Edit, L("Permission:Books.Edit"));
		booksPermission.AddChild(bookstorePermissions.Books.Delete, L("Permission:Books.Delete"));


		var authorsPermission = bookStoreGroup.AddPermission(bookstorePermissions.Authors.Default, L("Permission:Authors"));

		authorsPermission.AddChild(bookstorePermissions.Authors.Create, L("Permission:Authors.Create"));

		authorsPermission.AddChild(bookstorePermissions.Authors.Edit, L("Permission:Authors.Edit"));

		authorsPermission.AddChild(bookstorePermissions.Authors.Delete, L("Permission:Authors.Delete"));


	}

	private static LocalizableString L(string name)
	{
		return LocalizableString.Create<bookstoreResource>(name);
	}
}
