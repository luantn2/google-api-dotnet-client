//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Authentication;
using Google.Apis.Discovery;
using Google.Apis.Requests;
using Google.Apis.Samples.TranslateApiWin.Services.Data;
using Google.Apis.Util;
using log4net;
using Newtonsoft.Json;

namespace Google.Apis.Samples.TranslateApiWin.Services.Data
{
    public class DetectionsListResponse
    {
        /// <summary>A detections contains detection results of several text</summary>
        [JsonProperty("detections")]
        public virtual IList<DetectionsResource> Detections { get; set; }
    }

    /// <summary>An array of languages which we detect for the given text The most likely language list first.</summary>
    public class DetectionsResource {}

    public class LanguagesListResponse
    {
        /// <summary>List of source/target languages supported by the translation API. If target parameter is unspecified, the list is sorted by the ASCII code point order of the language code. If target parameter is specified, the list is sorted by the collation order of the language name in the target language.</summary>
        [JsonProperty("languages")]
        public virtual IList<LanguagesResource> Languages { get; set; }
    }

    public class LanguagesResource
    {
        /// <summary>The language code.</summary>
        [JsonProperty("language")]
        public virtual string Language { get; set; }

        /// <summary>The localized name of the language if target parameter is given.</summary>
        [JsonProperty("name")]
        public virtual string Name { get; set; }
    }

    public class TranslationsListResponse
    {
        /// <summary>Translations contains list of translation results of given text</summary>
        [JsonProperty("translations")]
        public virtual IList<TranslationsResource> Translations { get; set; }
    }

    public class TranslationsResource
    {
        /// <summary>Detected source language if source parameter is unspecified.</summary>
        [JsonProperty("detectedSourceLanguage")]
        public virtual string DetectedSourceLanguage { get; set; }

        /// <summary>The translation.</summary>
        [JsonProperty("translatedText")]
        public virtual string TranslatedText { get; set; }
    }
}

namespace Google.Apis.Samples.Services
{
    public class TranslateService : ISchemaAwareRequestExecutor
    {
        private const string Version = "v2";

        private const string Name = "translate";

        private const string BaseUri = "https://www.googleapis.com/language/translate/";

        private const DiscoveryVersion DiscoveryVersionUsed = DiscoveryVersion.Version_1_0;
        private readonly IAuthenticator authenticator;

        private readonly Detections detections;
        private readonly IService genericService;

        private readonly Languages languages;

        private readonly Translations translations;
        private JsonSerializer newtonJsonSerilizer;

        public TranslateService(IService genericService, IAuthenticator authenticator)
        {
            this.genericService = genericService;
            this.authenticator = authenticator;
            detections = new Detections(this);
            languages = new Languages(this);
            translations = new Translations(this);
        }

        public TranslateService() :
            this(
            new DiscoveryService(
                new WebDiscoveryDevice(
                    new Uri(string.Format("https://www.googleapis.com/discovery/v1/apis/{0}/{1}/rest", Name, Version))))
                .GetService(Version, DiscoveryVersionUsed, new FactoryParameterV1_0("https://www.googleapis.com", "/language/translate/")),
            AuthenticatorFactory.GetInstance().GetRegisteredAuthenticator()) {}

        private JsonSerializer NewtonJsonSerilizer
        {
            get
            {
                if ((newtonJsonSerilizer == null))
                {
                    var settings = new JsonSerializerSettings();
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    newtonJsonSerilizer = JsonSerializer.Create(settings);
                }
                return newtonJsonSerilizer;
            }
        }

        /// <summary>Sets the DeveloperKey which this service uses for all requests</summary>
        public virtual string DeveloperKey { get; set; }

        public virtual Detections Detections
        {
            get { return detections; }
        }

        public virtual Languages Languages
        {
            get { return languages; }
        }

        public virtual Translations Translations
        {
            get { return translations; }
        }

        #region ISchemaAwareRequestExecutor Members

        public virtual Stream ExecuteRequest(string resource, string method, string body,
                                             IDictionary<string, object> parameters)
        {
            IRequest request = genericService.CreateRequest(resource, method);
            if (string.IsNullOrEmpty(DeveloperKey) == false)
            {
                request = request.WithDeveloperKey(DeveloperKey);
            }
            return request.WithParameters(parameters).WithAuthentication(authenticator).WithBody(body).ExecuteRequest();
        }

        public virtual Stream ExecuteRequest(string resource, string method, object body,
                                             IDictionary<string, object> parameters)
        {
            return ExecuteRequest(resource, method, ObjectToJson(body), parameters);
        }

        public virtual string ObjectToJson(object obj)
        {
            TextWriter tw = new StringWriter();
            NewtonJsonSerilizer.Serialize(tw, obj);
            return tw.ToString();
        }

        public virtual TOutput JsonToObject<TOutput>(Stream stream)
        {
            var streamReader = new StreamReader(stream);
            string str = streamReader.ReadToEnd();
            try
            {
                var response = JsonConvert.DeserializeObject<StandardResponse<TOutput>>(str);
                if ((response.Data == null))
                {
                    throw new ApplicationException(string.Format("Failed to get response from stream, error was [{0}]",
                                                                 response.Error));
                }
                return response.Data;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                    string.Format("Failed to generate object of type[{0}] from Json[{1}]", typeof (TOutput).Name, str),
                    ex);
            }
        }

        #endregion
    }

    public class Detections
    {
        private const string Resource = "detections";

        private readonly ILog logger = LogManager.GetLogger(typeof (Detections));
        private readonly ISchemaAwareRequestExecutor service;

        public Detections(TranslateService service)
        {
            this.service = service;
        }

        /// <summary>Detect the language of text.</summary>
        /// <param name="q">Required - The text to detect</param>
        public virtual Stream List(string q)
        {
            string body = null;
            var parameters = new Dictionary<string, object>();
            parameters["q"] = q;
            logger.Debug("Executing detections.list");
            Stream ret = service.ExecuteRequest(Resource, "list", body, parameters);
            logger.Debug("Done Executing detections.list");
            return ret;
        }

        /// <summary>Detect the language of text.</summary>
        /// <param name="q">Required - The text to detect</param>
        public virtual DetectionsListResponse ListAsObject(string q)
        {
            string body = null;
            var parameters = new Dictionary<string, object>();
            parameters["q"] = q;
            logger.Debug("Executing detections.list");
            var ret =
                service.JsonToObject<DetectionsListResponse>(service.ExecuteRequest(Resource, "list", body, parameters));
            logger.Debug("Done Executing detections.list");
            return ret;
        }
    }

    public class Languages
    {
        private const string Resource = "languages";

        private readonly ILog logger = LogManager.GetLogger(typeof (Languages));
        private readonly ISchemaAwareRequestExecutor service;

        public Languages(TranslateService service)
        {
            this.service = service;
        }

        /// <summary>List the source/target languages supported by the API</summary>
        /// <param name="target">Optional - the language and collation in which the localized results should be returned</param>
        public virtual Stream List(string target)
        {
            string body = null;
            var parameters = new Dictionary<string, object>();
            parameters["target"] = target;
            logger.Debug("Executing languages.list");
            Stream ret = service.ExecuteRequest(Resource, "list", body, parameters);
            logger.Debug("Done Executing languages.list");
            return ret;
        }

        /// <summary>List the source/target languages supported by the API</summary>
        /// <param name="target">Optional - the language and collation in which the localized results should be returned</param>
        public virtual LanguagesListResponse ListAsObject(string target)
        {
            string body = null;
            var parameters = new Dictionary<string, object>();
            parameters["target"] = target;
            logger.Debug("Executing languages.list");
            var ret =
                service.JsonToObject<LanguagesListResponse>(service.ExecuteRequest(Resource, "list", body, parameters));
            logger.Debug("Done Executing languages.list");
            return ret;
        }

        /// <summary>List the source/target languages supported by the API</summary>
        public virtual Stream List(IDictionary<string, object> parameters)
        {
            string body = null;
            logger.Debug("Executing languages.list");
            Stream ret = service.ExecuteRequest(Resource, "list", body, parameters);
            logger.Debug("Done Executing languages.list");
            return ret;
        }
    }

    public class Translations
    {
        private const string Resource = "translations";

        private readonly ILog logger = LogManager.GetLogger(typeof (Translations));
        private readonly ISchemaAwareRequestExecutor service;

        public Translations(TranslateService service)
        {
            this.service = service;
        }

        /// <summary>Returns text translations from one language to another.</summary>
        /// <param name="q">Required - The text to translate</param>
        /// <param name="target">Required - The target language into which the text should be translated</param>
        /// <param name="format">Optional - Must be one of the following values [html, text] - The format of the text</param>
        /// <param name="source">Optional - The source language of the text</param>
        public virtual Stream List(string q, string target, string format, string source)
        {
            string body = null;
            var parameters = new Dictionary<string, object>();
            parameters["q"] = q;
            parameters["target"] = target;
            parameters["format"] = format;
            parameters["source"] = source;
            logger.Debug("Executing translations.list");
            Stream ret = service.ExecuteRequest(Resource, "list", body, parameters);
            logger.Debug("Done Executing translations.list");
            return ret;
        }

        /// <summary>Returns text translations from one language to another.</summary>
        /// <param name="q">Required - The text to translate</param>
        /// <param name="target">Required - The target language into which the text should be translated</param>
        /// <param name="format">Optional - Must be one of the following values [html, text] - The format of the text</param>
        /// <param name="source">Optional - The source language of the text</param>
        public virtual TranslationsListResponse ListAsObject(string q, string target, string format, string source)
        {
            string body = null;
            var parameters = new Dictionary<string, object>();
            parameters["q"] = q;
            parameters["target"] = target;
            parameters["format"] = format;
            parameters["source"] = source;
            logger.Debug("Executing translations.list");
            var ret =
                service.JsonToObject<TranslationsListResponse>(service.ExecuteRequest(Resource, "list", body, parameters));
            logger.Debug("Done Executing translations.list");
            return ret;
        }

        /// <summary>Returns text translations from one language to another.</summary>
        /// <param name="q">Required - The text to translate</param>
        /// <param name="target">Required - The target language into which the text should be translated</param>
        public virtual Stream List(string q, string target, IDictionary<string, object> parameters)
        {
            string body = null;
            parameters["q"] = q;
            parameters["target"] = target;
            logger.Debug("Executing translations.list");
            Stream ret = service.ExecuteRequest(Resource, "list", body, parameters);
            logger.Debug("Done Executing translations.list");
            return ret;
        }
    }
}