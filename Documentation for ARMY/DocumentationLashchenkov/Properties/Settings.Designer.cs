﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DocumentationLashchenkov.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Korshuny.mdf;" +
            "Integrated Security=True")]
        public string KorshunyConnectionString {
            get {
                return ((string)(this["KorshunyConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\USERS\\HAZYDRAG\\DESKTOP\\PROJ" +
            "ECTS\\DOCUMENTATIONLASHCHENKOV\\DOCUMENTATIONLASHCHENKOV\\KORSHUNY.MDF;Integrated S" +
            "ecurity=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True")]
        public string C__USERS_HAZYDRAG_DESKTOP_PROJECTS_DOCUMENTATIONLASHCHENKOV_DOCUMENTATIONLASHCHENKOV_KORSHUNY_MDFConnectionString {
            get {
                return ((string)(this["C__USERS_HAZYDRAG_DESKTOP_PROJECTS_DOCUMENTATIONLASHCHENKOV_DOCUMENTATIONLASHCHEN" +
                    "KOV_KORSHUNY_MDFConnectionString"]));
            }
        }
    }
}
