using Newtonsoft.Json;
using System.IO;
using System;

namespace Wox.Stash.Settings
{
    public class PluginSettings
    {
        private static JsonSerializer Serializer = new JsonSerializer();
        private string _path;

        public Action SettingsChanged;

        public static PluginSettings Instance { get; set; }

        public static void LoadSettings(string file)
        {
            if(File.Exists(file))
            {
                var j = new JsonTextReader(new StreamReader(file));
                Instance = Serializer.Deserialize<PluginSettings>(j);
                Instance._path = file;
            }
            else
            {
                // Defaults
                Instance = new PluginSettings
                {
                    StashUrl = null,
                    CloneDestination = "C:\\git",
                    CloneMethod = "ssh"
                };
                Instance._path = file;
                Instance.Save();
            }           
        }

        public void Save()
        {
            var w = new JsonTextWriter(new StreamWriter(_path, false));
            Serializer.Serialize(w, this);
            w.Close();
            if(SettingsChanged != null)
            {
                SettingsChanged();
            }
        }

        public PluginSettings(){}

        private PluginSettings(string file) {}

        public string StashUrl { get; set; }

        public string CloneDestination { get; set; }

        public string CloneMethod { get; set; }
    }
}
