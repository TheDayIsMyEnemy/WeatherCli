using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;
using WeatherCli.Options;

namespace WeatherCli
{
    public class WritableOptions<TOptions> : IWritableOptions<TOptions> where TOptions : class, new()
    {
        private const string SettingsFileName = "settings.json";

        public TOptions Value => GetSection(typeof(TOptions).Name);

        public void Update(Action<TOptions> applyChanges)
        {
            string sectionName = typeof(TOptions).Name;
            UpdateSection(sectionName, applyChanges);
        }

        private void UpdateSection(string sectionName, Action<TOptions> applyChanges)
        {
            string settingsFullPath = GetSettingsFullPath();

            if (!File.Exists(settingsFullPath))
            {
                File.WriteAllText(settingsFullPath, "{}");
            }

            var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(settingsFullPath));
            var sectionObject = jObject.TryGetValue(sectionName, out JToken section) ?
                JsonConvert.DeserializeObject<TOptions>(section.ToString()) : new TOptions();

            applyChanges(sectionObject);

            jObject[sectionName] = JObject.Parse(JsonConvert.SerializeObject(sectionObject));
            File.WriteAllText(settingsFullPath, JsonConvert.SerializeObject(jObject, Formatting.Indented));
        }

        public TOptions GetSection(string sectionName)
        {
            string settingsFullPath = GetSettingsFullPath();

            if (File.Exists(settingsFullPath))
            {
                var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(settingsFullPath));
                return jObject.TryGetValue(sectionName, out JToken section) ?
                    JsonConvert.DeserializeObject<TOptions>(section.ToString()) : new TOptions();
            }

            return new TOptions();
        }

        private static string GetSettingsFullPath()
        {
            string assemblyFullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string settingsFullPath = Path.Combine(assemblyFullPath, SettingsFileName);
            return settingsFullPath;
        }
    }
}
