using System;
using System.Collections.Generic;
using StayClassyBruh.Assets.Effects;
using Vapok.Common.Abstractions;
using Vapok.Common.Managers.Configuration;

namespace StayClassyBruh.Assets.Factories;

public enum StayClassyBruhEffects
{
}
public class EffectsFactory : FactoryBase
{
    public static Dictionary<StayClassyBruhEffects,EffectsBase> EffectList => _effectList;

    private static Dictionary<StayClassyBruhEffects,EffectsBase> _effectList = new();
    
    public EffectsFactory(ILogIt logger, ConfigSyncBase configs) : base(logger, configs)
    {
    
    }

    public void RegisterEffects()
    {
    }
}