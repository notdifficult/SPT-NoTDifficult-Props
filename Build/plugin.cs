using System.Reflection;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Logging;
using SPTarkov.Server.Core.Models.Utils;
using WTTServerCommonLib.Services;

namespace SPTNoTDifficultProps;

[Injectable(InjectionType = InjectionType.Singleton, TypePriority = OnLoadOrder.PostDBModLoader + 2)]

public class Plugin(ISptLogger<Plugin> props, WTTCustomItemServiceExtended itemService) : IOnLoad
{
    private static Assembly Assembly = Assembly.GetExecutingAssembly();
    
    public Task OnLoad()
    {
        props.LogWithColor("Hello NoTDifficultProps Loading...",LogTextColor.Cyan);

        itemService.CreateCustomItems(Assembly);
        
        return Task.CompletedTask; 
    }
}