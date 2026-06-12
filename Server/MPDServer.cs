using Range = SemanticVersioning.Range;
using Path = System.IO.Path;

using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Routers;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Utils;
using System.Reflection;
//using System.Text.Json;
using SPTarkov.Server.Core.Models.Utils;
//using SPTarkov.Server.Core.Models.Logging;
//using Microsoft.Extensions.Logging;
//using SPTarkov.Server.Core.Models.Common;
//using SPTarkov.Server.Core.Services;
using WTTServerCommonLib.Models;








namespace NoTDifficult_MiyukiPropsDealer;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.NoTDifficult.MiyukiPropsDealer";
    public override string Name { get; init; } = "MiyukiPropsDealer";
    public override string Author { get; init; } = "NoTDifficult";
    public override List<string>? Contributors { get; init; } = [];
    public override SemanticVersioning.Version Version { get; init; } = new("2.0.0");
    public override Range SptVersion { get; init; } = new("~4.0.13");
    public override List<string>? Incompatibilities { get; init; } = [];
    public override Dictionary<string, Range>? ModDependencies { get; init; } = new() { { "com.wtt.commonlib", new Range("~2.0") } };
    public override string? Url { get; init; } = "https://github.com/notdifficult/SPT-Miyuki-props-dealer";
    public override bool? IsBundleMod { get; init; } = true;
    public override string License { get; init; } = "MIT";
}









[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 2)]
//[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]


public class MiyukiPropsDealer
(
    WTTServerCommonLib.WTTServerCommonLib wtt,
    ModHelper modHelper,
    ImageRouter imageRouter,
    ISptLogger<MiyukiPropsDealer> logger,
    ConfigServer configServer,
    TimeUtil timeUtil,
    AddCustomTraderHelper addCustomTraderHelper // This is a custom class we add for this mod, we made it injectable so it can be accessed like other classes here
)
    : IOnLoad
{
    
    
    private readonly TraderConfig _traderConfig = configServer.GetConfig<TraderConfig>();
    private readonly RagfairConfig _ragfairConfig = configServer.GetConfig<RagfairConfig>();

    public async Task OnLoad()
    //public Task OnLoad()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var pathToMod = modHelper.GetAbsolutePathToModFolder(Assembly.GetExecutingAssembly());
        var traderImagePath = Path.Combine(pathToMod, "db/Miyuki/images/Miyuki.png");
        var traderBase = modHelper.GetJsonDataFromFile<TraderBase>(pathToMod, "db/Miyuki/base.json");
        var assort = modHelper.GetJsonDataFromFile<TraderAssort>(pathToMod, "db/Miyuki/assort.json");

        //                         sooon                               //
        //string itemConfigsDirectory = Path.Combine("db", "Items"); 
        //string hideoutRecipesDirectory = Path.Combine("db", "HideoutRecipes");
        //string questsDirectory = Path.Combine("db", "Quests");
        //string questZonesDirectory = Path.Combine("db", "QuestZones");
        //string lootSpawnDirectory = Path.Combine("db", "LootSpawns");
        //string lootSpawnQuestsDirectory = Path.Combine("db", "LootSpawnsQuests");
        
        TraderIds.Add("Miyuki", "6a2b2d6fce04bf77dbda0df2");
        
        _ragfairConfig.Traders.TryAdd(traderBase.Id, true);
        {
            logger.Warning($"Trader {traderBase.Id} already in Ragfair config.");
        }
        
        imageRouter.AddRoute(traderBase.Avatar!.Replace(".png", ""), traderImagePath);
        
        addCustomTraderHelper.SetTraderUpdateTime(_traderConfig, traderBase, timeUtil.GetHoursAsSeconds(1), timeUtil.GetHoursAsSeconds(2));
        addCustomTraderHelper.AddTraderWithEmptyAssortToDb(traderBase);
        addCustomTraderHelper.AddTraderToLocales(traderBase, "Miyuki", "My name is miyuki, nice to meet you) I am a secret experiment number C416. developed at TerraGroup Labs. The essence of the experiment was to create intelligent beings who think no worse than humans. they took the genes of a cat and an accidental SCAV, and they created me. Something didn't go according to plan, I turned out to be much smarter than they expected, and I ran away. Now I live in the customs area and sell junk that would be enough for food. I need people like you to rise from the bottom. I will give you various tasks and we can rise from the bottom together)");
        addCustomTraderHelper.OverwriteTraderAssort(traderBase.Id, assort);
        
        await wtt.CustomItemServiceExtended.CreateCustomItems(assembly);
        
        //                         sooon                               //
        //await wtt.CustomItemServiceExtended.CreateCustomItems(assembly, itemConfigsDirectory);
        //await wtt.CustomHideoutRecipeService.CreateHideoutRecipes(assembly, hideoutRecipesDirectory);
        //await wtt.CustomQuestService.CreateCustomQuests(assembly, questsDirectory);
        //await wtt.CustomQuestZoneService.CreateCustomQuestZones(assembly, questZonesDirectory);
        //await wtt.CustomLootspawnService.CreateCustomLootSpawns(assembly, lootSpawnDirectory);
        //await wtt.CustomLootspawnService.CreateCustomLootSpawns(assembly, lootSpawnQuestsDirectory);
        
        logger.Info($"Trader {traderBase.Nickname} loaded successfully.");
        
        //return Task.CompletedTask;
        await Task.CompletedTask;
    }
}





/*
[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 2)]
//public class MiyukiPropsDealer(WTTServerCommonLib.WTTServerCommonLib wtt) : IOnLoad

public class MiyukiPropsDealer
    (
        WTTServerCommonLib.WTTServerCommonLib wtt,
        DatabaseService db,
        ConfigServer config, 
        ModHelper helper,
        ISptLogger<MiyukiPropsDealer> logger,
        AddCustomTraderHelper addCustomTraderHelper, // This is a custom class we add for this mod, we made it injectable so it can be accessed like other classes here
        ImageRouter imageRouter,
        TimeUtil timeUtil
    ) : IOnLoad
    */

/*{
    
    //private readonly TraderConfig _traderConfig = config.GetConfig<TraderConfig>();
    //private readonly RagfairConfig _ragfairConfig = config.GetConfig<RagfairConfig>();
    private readonly TraderConfig _traderConfig = configServer.GetConfig<TraderConfig>();
    private readonly RagfairConfig _ragfairConfig = configServer.GetConfig<RagfairConfig>();
    
    public async Task OnLoad()
    {
        //Assembly assembly = Assembly.GetExecutingAssembly();
        var assembly = Assembly.GetExecutingAssembly();
        
        var pathToMod = helper.GetAbsolutePathToModFolder(Assembly.GetExecutingAssembly());
        var traderImagePath = Path.Combine(pathToMod, "db/Miyuki/images/Miyuki.png");
        var traderBase = helper.GetJsonDataFromFile<TraderBase>(pathToMod, "db/Miyuki/base.json");
        
        //string itemConfigsDirectory = Path.Combine("db", "Items");
        //string hideoutRecipesDirectory = Path.Combine("db", "HideoutRecipes");
        //string questsDirectory = Path.Combine("db", "Quests");
        //string questZonesDirectory = Path.Combine("db", "QuestZones");
        //string lootSpawnDirectory = Path.Combine("db", "LootSpawns");
        //string lootSpawnQuestsDirectory = Path.Combine("db", "LootSpawnsQuests");
        
        
        imageRouter.AddRoute(traderBase.Avatar!.Replace(".png", ""), traderImagePath);
        addCustomTraderHelper.SetTraderUpdateTime(_traderConfig, traderBase, timeUtil.GetHoursAsSeconds(1), timeUtil.GetHoursAsSeconds(2));
        
        _ragfairConfig.Traders.TryAdd(traderBase.Id, true);
        {
            logger.Warning($"Trader {traderBase.Id} already in Ragfair config.");
        }
        
        
        addCustomTraderHelper.AddTraderWithEmptyAssortToDb(traderBase);
        addCustomTraderHelper.AddTraderToLocales(traderBase, "Miyuki", "need add info");
        
        var assort = helper.GetJsonDataFromFile<TraderAssort>(pathToMod, "db/Miyuki/assort.json");
        
        addCustomTraderHelper.OverwriteTraderAssort(traderBase.Id, assort);
        
        logger.Info($"Trader {traderBase.Nickname} loaded successfully.");
        
        
        // Registering the actual custom stuff
        TraderIds.Add("Miyuki", "6a2b2d6fce04bf77dbda0df2");
        
        //await wtt.CustomItemServiceExtended.CreateCustomItems(assembly, itemConfigsDirectory);
        //await wtt.CustomHideoutRecipeService.CreateHideoutRecipes(assembly, hideoutRecipesDirectory);
        //await wtt.CustomQuestService.CreateCustomQuests(assembly, questsDirectory);
        //await wtt.CustomQuestZoneService.CreateCustomQuestZones(assembly, questZonesDirectory);
        //await wtt.CustomLootspawnService.CreateCustomLootSpawns(assembly, lootSpawnDirectory);
        //await wtt.CustomLootspawnService.CreateCustomLootSpawns(assembly, lootSpawnQuestsDirectory);
        
        
        await wtt.CustomItemServiceExtended.CreateCustomItems(assembly);
        
        await Task.CompletedTask;
        
    }
}*/

