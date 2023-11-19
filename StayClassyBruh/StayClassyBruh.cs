/* StayClassyBruh by Vapok */
using System;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using ItemManager;
using JetBrains.Annotations;
using StayClassyBruh.Assets.Factories;
using StayClassyBruh.Configuration;
using Vapok.Common.Abstractions;
using Vapok.Common.Managers;
using Vapok.Common.Managers.Configuration;
using Vapok.Common.Managers.LocalizationManager;
using Vapok.Common.Tools;

namespace StayClassyBruh
{
    [BepInPlugin(_pluginId, _displayName, _version)]
    public class StayClassyBruh : BaseUnityPlugin, IPluginInfo
    {
        //Module Constants
        private const string _pluginId = "vapok.mods.StayClassyBruh";
        private const string _displayName = "StayClassyBruh";
        private const string _version = "1.0.0";
        
        //Interface Properties
        public string PluginId => _pluginId;
        public string DisplayName => _displayName;
        public string Version => _version;
        public BaseUnityPlugin Instance => _instance;
        
        //Class Properties
        public static ILogIt Log => _log;
        public static bool ValheimAwake = false;
        public static Waiting Waiter;
        
        //Class Privates
        private static StayClassyBruh _instance;
        private static ConfigSyncBase _config;
        private static ILogIt _log;
        private Harmony _harmony;
        
        [UsedImplicitly]
        // This the main function of the mod. BepInEx will call this.
        private void Awake()
        {
            //I'm awake!
            _instance = this;
            
            //Waiting For Startup
            Waiter = new Waiting();
            
            //Initialize Managers
            Initializer.LoadManagers(enableLocalizationManager:true);

            //Register Configuration Settings
            _config = new ConfigRegistry(_instance);

            //Register Logger
            LogManager.Init(PluginId,out _log);

            Localizer.Waiter.StatusChanged += InitializeModule;
            
            //Patch Harmony
            _harmony = new Harmony(Info.Metadata.GUID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());

            //???

            //Profit
        }
        
        private void Update()
        {
            if (!Player.m_localPlayer || !ZNetScene.instance)
                return;
        }

        public void InitializeModule(object send, EventArgs args)
        {
            if (ValheimAwake)
                return;
            
            //Register Effects
            var effectsFactory = new EffectsFactory(_log, _config);
            effectsFactory.RegisterEffects();
            
            //Register Assets
            
            ConfigRegistry.Waiter.ConfigurationComplete(true);

            ValheimAwake = true;
        }
        
        private void OnDestroy()
        {
            _instance = null;
            _harmony?.UnpatchSelf();
        }

        public class Waiting
        {
            public void ValheimIsAwake(bool awakeFlag)
            {
                if (awakeFlag)
                    StatusChanged?.Invoke(this, EventArgs.Empty);
            }
            public event EventHandler StatusChanged;            
        }
    }
}