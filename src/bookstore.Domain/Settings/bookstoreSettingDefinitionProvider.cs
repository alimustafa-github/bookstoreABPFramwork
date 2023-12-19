using Volo.Abp.Settings;

namespace bookstore.Settings;

public class bookstoreSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(bookstoreSettings.MySetting1));
    }
}
