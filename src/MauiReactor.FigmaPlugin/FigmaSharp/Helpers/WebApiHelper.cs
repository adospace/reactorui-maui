// Authors:
//   Jose Medrano <josmed@microsoft.com>
//
// Copyright (C) 2018 Microsoft, Corp
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the
// following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
// OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN
// NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE
// USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.IO;
using System.Linq;
using System.Reflection;

using FigmaSharp.Models;
using Newtonsoft.Json;

namespace FigmaSharp.Helpers
{
	public static class WebApiHelper
    {
        const string prefix = "/file/";
        const char urlSeparatorChar = '/';

        public static bool TryParseFileUrl (string link, out string fileId)
        {
            if (string.IsNullOrEmpty(link))
            {
                fileId = string.Empty;
                return false;
            }

            if (Uri.TryCreate (link, UriKind.Absolute, out var result))
            {
                try
                {
                    string path = result.AbsolutePath;
                    if (path.StartsWith(prefix))
                    {
                        string id = path.Substring(prefix.Length);
                        if (id.Contains(urlSeparatorChar))
                            id = id.Substring(0, id.IndexOf(urlSeparatorChar));
                        fileId = id;
                        return true;
                    }
                }
                catch
                {
                }
            }
          
            fileId = link;
            return false;
        }

        public static string GetManifestResource (Assembly assembly, string resource)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetEntryAssembly();
            }
            try
            {
                foreach (var fullResourceName in new string[]
                  {
                        assembly.GetName().Name,
                        string.Format("{0}.{1}", assembly.GetName().Name, resource)
                  })
                {
                    var resources = assembly.GetManifestResourceNames();
                    using (var stream = assembly.GetManifestResourceStream(fullResourceName))
                    {
                        if (stream == null)
                        {
                            //LoggingService.LogWarning("Resource '{0}' not found in assembly '{1}'", resource, assembly.FullName);
                            return null;
                        }
                        using (TextReader tr = new StreamReader(stream))
                        {
                            return tr.ReadToEnd();
                        };
                    }
                }
            }
            catch (System.Exception ex)
            {
                //LoggingService.LogError(string.Format("[FIGMA] Cannot read resource '{0}' in assembly '{1}'", resource), ex);
            }
            return null;
        }

        public static FigmaFileResponse GetFigmaResponseFromFileContent (string figmaContent)
        {
            return JsonConvert.DeserializeObject<FigmaFileResponse> (figmaContent, new FigmaResponseConverter ());
        }

        public static FigmaFileVersionResponse GetFigmaResponseFromFileVersionContent (string figmaVersionContent)
        {
            return JsonConvert.DeserializeObject<FigmaFileVersionResponse> (figmaVersionContent);
        }

        public static string GetUrlContent(string url, string version)
        {
            try
            {
                var wc = new System.Net.WebClient();
                byte[] raw = wc.DownloadData(url);

                string webData = System.Text.Encoding.UTF8.GetString(raw);
                return webData;
            }
            catch (Exception ex)
            {
                //LoggingService.LogError("[FIGMA] Error.", ex);
            }
            return string.Empty;
        }
    }
}
