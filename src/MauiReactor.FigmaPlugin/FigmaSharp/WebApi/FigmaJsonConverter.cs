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

using FigmaSharp.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FigmaSharp
{
    public class FigmaResponseConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(FigmaNode).IsAssignableFrom(objectType) ||
            typeof(FigmaComponent) == objectType ||
            typeof(FigmaPoint) == objectType;
        }

        public override object ReadJson(JsonReader reader,
            Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);

            object figmaObject = null;

            if (!jsonObject.ContainsKey ("type") || string.IsNullOrEmpty (jsonObject["type"].Value<string> ()))
            {
                if (objectType == typeof (FigmaPoint))
                {
                    return jsonObject.ToObject<FigmaPoint>();
                }
                if (objectType == typeof(FigmaComponent))
                {
                    //var obj = jsonObject.ToObject<FigmaComponent>();
                    serializer.MissingMemberHandling = MissingMemberHandling.Ignore;
                    serializer.NullValueHandling = NullValueHandling.Ignore;
                    figmaObject = jsonObject.ToObject<FigmaComponent> ();
                    serializer.Populate (jsonObject.CreateReader (), figmaObject);
                    return figmaObject;
                }

                throw new NotImplementedException(objectType.ToString ());
            }

            if (jsonObject["type"].Value<string>() == "DOCUMENT")
            {
                figmaObject = jsonObject.ToObject<FigmaDocument>();
            }
            else if (jsonObject["type"].Value<string>() == "CANVAS")
            {
                figmaObject = jsonObject.ToObject<FigmaCanvas>();
            }
            else if (jsonObject["type"].Value<string>() == "SLICE")
            {
                figmaObject = jsonObject.ToObject<FigmaSlice>();
            }
            else if (jsonObject["type"].Value<string>() == "FRAME")
            {
                figmaObject = jsonObject.ToObject<FigmaFrame>();
            }
            else if (jsonObject["type"].Value<string>() == "VECTOR")
            {
                figmaObject = jsonObject.ToObject<FigmaVector>();
            }
            else if (jsonObject["type"].Value<string>() == "TEXT")
            {
                figmaObject = jsonObject.ToObject<FigmaText>();
                return figmaObject;
            }
            else if (jsonObject["type"].Value<string>() == "GROUP")
            {
                figmaObject = jsonObject.ToObject<FigmaGroup>();
            }
            else if (jsonObject["type"].Value<string>() == "RECTANGLE")
            {
                figmaObject = jsonObject.ToObject<RectangleVector>();
                return figmaObject;
            }
            else if (jsonObject["type"].Value<string>() == "LINE")
            {
                figmaObject = jsonObject.ToObject<FigmaLine>();
                return figmaObject;
            }
            else if (jsonObject["type"].Value<string>() == "ELLIPSE")
            {
                figmaObject = jsonObject.ToObject<FigmaElipse>();
                return figmaObject;
            }
            else if (jsonObject["type"].Value<string>() == "STAR")
            {
                figmaObject = jsonObject.ToObject<FigmaStar>();
                return figmaObject;
            }
            else if (jsonObject["type"].Value<string>() == "REGULAR_POLYGON")
            {
                figmaObject = jsonObject.ToObject<FigmaRegularPolygon>();
                return figmaObject;
            }
            else if (jsonObject["type"].Value<string>() == "INSTANCE")
            {
                figmaObject = jsonObject.ToObject<FigmaInstance>();
            }
            else if (jsonObject["type"].Value<string>() == "COMPONENT")
            {
                figmaObject = jsonObject.ToObject<FigmaComponentEntity>();
            }
            else if (jsonObject["type"].Value<string>() == "BOOLEAN_OPERATION")
            {
                figmaObject = jsonObject.ToObject<FigmaBoolean>();
            }
			else if (jsonObject["type"].Value<string> () == "BOOLEAN_OPERATION") {
                figmaObject = jsonObject.ToObject<FigmaBoolean> ();
            } else
            {
                return jsonObject.ToObject<object>();
            }
            serializer.MissingMemberHandling = MissingMemberHandling.Ignore;
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Populate(jsonObject.CreateReader(), figmaObject);
            return figmaObject;
        }


        public override void WriteJson(JsonWriter writer,
            object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
