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
using System.Net;
using System.Text;
using System.Linq;

using FigmaSharp.Helpers;
using FigmaSharp.Models;

using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace FigmaSharp
{
	public class FigmaApi
	{
		internal string Token { get; set; }

        public FigmaApi(string token)
		{
			Token = token;
		}

        //TODO: right now there is no way to detect changes in API
        public Version Version { get; } = new Version (1, 0, 1);

		#region Urls

		string GetFigmaFileUrl (string fileId) => string.Format ("https://api.figma.com/v1/files/{0}", fileId);
		string GetFigmaImageUrl (string fileId, params IImageNodeRequest[] imageIds) => string.Format ("https://api.figma.com/v1/images/{0}?ids={1}", fileId, string.Join (",", imageIds.Select(s => s.ResourceId).ToArray ()));
		string GetFigmaFileVersionsUrl (string fileId) => string.Format ("{0}/versions", GetFigmaFileUrl (fileId));

		#endregion

		public Task<string> GetContentFileAsync (FigmaFileQuery figmaQuery)
		{
			var result = GetContentUrlAsync (figmaQuery,
				(e) => {
					var queryUrl = GetFigmaFileUrl (figmaQuery.FileId);
					if (e.Version != null) {
						queryUrl += string.Format ("?version={0}", figmaQuery.Version);
					}
					return queryUrl;
				}
			);
			return result;
		}

		public Task<string> GetContentFileVersionAsync (FigmaFileVersionQuery figmaQuery)
		{
			return GetContentUrlAsync (figmaQuery, (e) => GetFigmaFileVersionsUrl (e.FileId));
		}

		public async Task<FigmaFileResponse> GetFileAsync (FigmaFileQuery figmaQuery)
		{
			var content = await GetContentFileAsync (figmaQuery);
			return WebApiHelper.GetFigmaResponseFromFileContent (content);
		}

		public async Task<FigmaFileVersionResponse> GetFileVersionsAsync (FigmaFileVersionQuery figmaQuery)
		{
			var content = await GetContentFileVersionAsync (figmaQuery);
			return WebApiHelper.GetFigmaResponseFromFileVersionContent (content);
		}

		#region Images

		public Task<FigmaImageResponse> GetImageAsync (string fileId, IImageNodeRequest[] resourceIds, ImageFormat format = ImageFormat.png, float scale = 2)
		{
			var currentIds = resourceIds.Select(s => s.ResourceId).ToArray();
			var query = new FigmaImageQuery (fileId, resourceIds);
			query.Scale = scale;
			query.Format = format;
			return GetImageAsync (query);
		}

		public async Task ProcessDownloadImagesAsync (string fileId, IImageNodeRequest[] resourceIds, ImageFormat format = ImageFormat.png, float scale = 2)
		{
			var response = await GetImageAsync(fileId, resourceIds, format, scale);
            foreach (var image in response.images)
            {
				var resourceId = resourceIds.FirstOrDefault(s => s.ResourceId == image.Key);
				if (resourceId != null)
					resourceId.Scales.Add(new ImageScale(scale, image.Value));
			}
		}

		public async Task<FigmaImageResponse> GetImageAsync (FigmaImageQuery figmaQuery)
		{
			var result = await GetContentUrlAsync (figmaQuery,
				(e) => {
					var figmaImageUrl = GetFigmaImageUrl (figmaQuery.FileId, figmaQuery.Ids);
					var stringBuilder = new StringBuilder (figmaImageUrl);

					stringBuilder.Append (string.Format ("&format={0}", figmaQuery.Format));
					stringBuilder.Append (string.Format ("&scale={0}", figmaQuery.Scale));

					if (figmaQuery.Version != null) {
						stringBuilder.Append (string.Format ("&version={0}", figmaQuery.Version));
					}
					return stringBuilder.ToString ();
				}
			);

			return JsonConvert.DeserializeObject<FigmaImageResponse> (result);
		}

		async Task<string> GetContentUrlAsync <T> (T figmaQuery, Func<T, string> handler) where T : FigmaApiBaseQuery
		{
			var token = string.IsNullOrEmpty (figmaQuery.PersonalAccessToken) ?
	Token : figmaQuery.PersonalAccessToken;

			var query = handler (figmaQuery);

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("x-figma-token", token);

            var response = await client.GetAsync(query);
            var content = await response.Content.ReadAsStringAsync();

            var json = Newtonsoft.Json.Linq.JObject.Parse(content);
            return json.ToString();
		}

		#endregion
	}
}