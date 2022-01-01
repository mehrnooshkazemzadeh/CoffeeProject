using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Framework.Core.Tools
{
    public class HtmlTemplateParser
    {
        private readonly string wwwRoot;

        public HtmlTemplateParser(IHostingEnvironment env)
        {
            wwwRoot = env.WebRootPath;
        }
        public HtmlTemplateParser(string wwwRoot)
        {
            this.wwwRoot = wwwRoot;
        }
        public string Parse(string filePath, string templateName, params KeyValuePair<string, string>[] values)
        {
            var path = Path.Combine(wwwRoot, filePath, templateName + ".html");
            return CreateTemplate(values, path);

        }
        public string Parse(string name, string templateName, CultureInfo culture, params KeyValuePair<string, string>[] values)
        {
            var path = Path.Combine(wwwRoot, "Templates", culture.Name, name, templateName + ".html");
            return CreateTemplate(values, path);

        }

        private static string CreateTemplate(KeyValuePair<string, string>[] values, string path)
        {
            var template = File.ReadAllText(path);

            foreach (var item in values)
            {
                var key = "#" + item.Key + "#";
                var value = item.Value;
                template = template.Replace(key, value);
            }
            return template;
        }
    }
}
