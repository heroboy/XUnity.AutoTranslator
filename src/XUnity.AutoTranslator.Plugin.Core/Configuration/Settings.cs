﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using XUnity.AutoTranslator.Plugin.Core.Constants;

namespace XUnity.AutoTranslator.Plugin.Core.Configuration
{
   public static class Settings
   {
      // cannot be changed
      public static readonly string DefaultLanguage = "en";
      public static readonly string DefaultFromLanguage = "ja";
      public static readonly int MaxErrors = 5;
      public static readonly float ClipboardDebounceTime = 1f;
      public static readonly int MaxTranslationsBeforeShutdown = 8000;
      public static readonly int MaxUnstartedJobs = 3500;
      public static readonly float IncreaseBatchOperationsEvery = 30;
      public static readonly bool EnableObjectTracking = true;
      public static readonly int MaximumStaggers = 5;
      public static readonly int MaximumConsecutiveFramesTranslated = 90;
      public static readonly int MaximumConsecutiveSecondsTranslated = 60;


      public static bool IsShutdown = false;
      public static int TranslationCount = 0;
      public static int MaxAvailableBatchOperations = 40;

      public static readonly float MaxTranslationsQueuedPerSecond = 5;
      public static readonly int MaxSecondsAboveTranslationThreshold = 30;
      public static readonly int TranslationQueueWatchWindow = 6;

      public static readonly int BatchSize = 10;

      // can be changed
      public static string ServiceEndpoint;
      public static string Language;
      public static string FromLanguage;
      public static string OutputFile;
      public static string TranslationDirectory;
      public static float Delay;
      public static int MaxCharactersPerTranslation;
      public static bool EnablePrintHierarchy;
      public static bool EnableConsole;
      public static bool EnableDebugLogs;
      public static string AutoTranslationsFilePath;
      public static bool EnableIMGUI;
      public static bool EnableUGUI;
      public static bool EnableNGUI;
      public static bool EnableTextMeshPro;
      public static bool EnableUtage;
      public static bool AllowPluginHookOverride;
      public static bool IgnoreWhitespaceInDialogue;
      public static bool IgnoreWhitespaceInNGUI;
      public static int MinDialogueChars;
      public static string BaiduAppId;
      public static string BaiduAppSecret;
      public static string YandexAPIKey;
      public static string WatsonAPIUrl;
      public static string WatsonAPIUsername;
      public static string WatsonAPIPassword;
      public static int ForceSplitTextAfterCharacters;
      public static bool EnableMigrations;
      public static string MigrationsTag;
      public static bool EnableBatching;
      public static bool TrimAllText;
      public static bool EnableUIResizing;
      public static string GoogleAPIKey;
      public static bool UseStaticTranslations;
      public static string OverrideFont;
      public static string UserAgent;

      public static bool CopyToClipboard;
      public static int MaxClipboardCopyCharacters;

      public static void Configure()
      {
         try
         {
            // clear configuration from old versions...
            var section = Config.Current.Preferences[ "AutoTranslator" ];
            foreach( var key in section.Keys.ToList() )
            {
               section.DeleteKey( key.Key );
            }

            Config.Current.Preferences.DeleteSection( "AutoTranslator" );
            Config.Current.Preferences[ "Service" ].DeleteKey( "EnableSSL" );
         }
         catch { }



         ServiceEndpoint = Config.Current.Preferences[ "Service" ][ "Endpoint" ].GetOrDefault( KnownEndpointNames.GoogleTranslate, true );

         Language = Config.Current.Preferences[ "General" ][ "Language" ].GetOrDefault( DefaultLanguage );
         FromLanguage = Config.Current.Preferences[ "General" ][ "FromLanguage" ].GetOrDefault( DefaultFromLanguage, true );

         TranslationDirectory = Config.Current.Preferences[ "Files" ][ "Directory" ].GetOrDefault( @"Translation" );
         OutputFile = Config.Current.Preferences[ "Files" ][ "OutputFile" ].GetOrDefault( @"Translation\_AutoGeneratedTranslations.{lang}.txt" );

         EnableIMGUI = Config.Current.Preferences[ "TextFrameworks" ][ "EnableIMGUI" ].GetOrDefault( false );
         EnableUGUI = Config.Current.Preferences[ "TextFrameworks" ][ "EnableUGUI" ].GetOrDefault( true );
         EnableNGUI = Config.Current.Preferences[ "TextFrameworks" ][ "EnableNGUI" ].GetOrDefault( true );
         EnableTextMeshPro = Config.Current.Preferences[ "TextFrameworks" ][ "EnableTextMeshPro" ].GetOrDefault( true );
         EnableUtage = Config.Current.Preferences[ "TextFrameworks" ][ "EnableUtage" ].GetOrDefault( true );
         AllowPluginHookOverride = Config.Current.Preferences[ "TextFrameworks" ][ "AllowPluginHookOverride" ].GetOrDefault( true );

         Delay = Config.Current.Preferences[ "Behaviour" ][ "Delay" ].GetOrDefault( 0f );
         MaxCharactersPerTranslation = Config.Current.Preferences[ "Behaviour" ][ "MaxCharactersPerTranslation" ].GetOrDefault( 200 );
         IgnoreWhitespaceInDialogue = Config.Current.Preferences[ "Behaviour" ][ "IgnoreWhitespaceInDialogue" ].GetOrDefault( Types.AdvEngine == null );
         IgnoreWhitespaceInNGUI = Config.Current.Preferences[ "Behaviour" ][ "IgnoreWhitespaceInNGUI" ].GetOrDefault( true );
         MinDialogueChars = Config.Current.Preferences[ "Behaviour" ][ "MinDialogueChars" ].GetOrDefault( 20 );
         ForceSplitTextAfterCharacters = Config.Current.Preferences[ "Behaviour" ][ "ForceSplitTextAfterCharacters" ].GetOrDefault( 0 );
         CopyToClipboard = Config.Current.Preferences[ "Behaviour" ][ "CopyToClipboard" ].GetOrDefault( false );
         MaxClipboardCopyCharacters = Config.Current.Preferences[ "Behaviour" ][ "MaxClipboardCopyCharacters" ].GetOrDefault( 450 );
         EnableUIResizing = Config.Current.Preferences[ "Behaviour" ][ "EnableUIResizing" ].GetOrDefault( true );
         EnableBatching = Config.Current.Preferences[ "Behaviour" ][ "EnableBatching" ].GetOrDefault( true );
         TrimAllText = Config.Current.Preferences[ "Behaviour" ][ "TrimAllText" ].GetOrDefault( Types.AdvEngine == null );
         UseStaticTranslations = Config.Current.Preferences[ "Behaviour" ][ "UseStaticTranslations" ].GetOrDefault( true );
         OverrideFont = Config.Current.Preferences[ "Behaviour" ][ "OverrideFont" ].GetOrDefault( string.Empty );

         UserAgent = Config.Current.Preferences[ "Http" ][ "UserAgent" ].GetOrDefault( string.Empty );

         GoogleAPIKey = Config.Current.Preferences[ "GoogleLegitimate" ][ "GoogleAPIKey" ].GetOrDefault( "" );

         BaiduAppId = Config.Current.Preferences[ "Baidu" ][ "BaiduAppId" ].GetOrDefault( "" );
         BaiduAppSecret = Config.Current.Preferences[ "Baidu" ][ "BaiduAppSecret" ].GetOrDefault( "" );

         YandexAPIKey = Config.Current.Preferences[ "Yandex" ][ "YandexAPIKey" ].GetOrDefault( "" );

         WatsonAPIUrl = Config.Current.Preferences[ "Watson" ][ "WatsonAPIUrl" ].GetOrDefault( "" );
         WatsonAPIUsername = Config.Current.Preferences[ "Watson" ][ "WatsonAPIUsername" ].GetOrDefault( "" );
         WatsonAPIPassword = Config.Current.Preferences[ "Watson" ][ "WatsonAPIPassword" ].GetOrDefault( "" );

         EnablePrintHierarchy = Config.Current.Preferences[ "Debug" ][ "EnablePrintHierarchy" ].GetOrDefault( false );
         EnableConsole = Config.Current.Preferences[ "Debug" ][ "EnableConsole" ].GetOrDefault( false );
         EnableDebugLogs = Config.Current.Preferences[ "Debug" ][ "EnableLog" ].GetOrDefault( false );

         EnableMigrations = Config.Current.Preferences[ "Migrations" ][ "Enable" ].GetOrDefault( true );
         MigrationsTag = Config.Current.Preferences[ "Migrations" ][ "Tag" ].GetOrDefault( string.Empty );

         AutoTranslationsFilePath = Path.Combine( Config.Current.DataPath, OutputFile.Replace( "{lang}", Language ) );

         if( EnableMigrations )
         {
            Migrate();
         }

         // update tag
         MigrationsTag = Config.Current.Preferences[ "Migrations" ][ "Tag" ].Value = PluginData.Version;

         Config.Current.SaveConfig();
      }

      private static void Migrate()
      {
         var currentTag = MigrationsTag;
         var newTag = PluginData.Version;

         // migrate from unknown version to known version. Reset to google translate
         if( string.IsNullOrEmpty( currentTag ) )
         {
            if( ServiceEndpoint == KnownEndpointNames.GoogleTranslateHack )
            {
               ServiceEndpoint = Config.Current.Preferences[ "Service" ][ "Endpoint" ].Value = KnownEndpointNames.GoogleTranslate;
            }
         }
      }

      public static string GetUserAgent( string defaultUserAgent )
      {
         if( !string.IsNullOrEmpty( UserAgent ) )
         {
            return UserAgent;
         }
         return defaultUserAgent;
      }
   }
}
