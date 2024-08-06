using Volo.Abp.Settings;

namespace ImportSample.Settings;

public class ImportSampleSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ImportSampleSettings.MySetting1));
    }
}
