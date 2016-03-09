﻿namespace Sequin.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Core.Infrastructure;
    using Microsoft.Owin;
    using Newtonsoft.Json;

    public class JsonDeserializerCommandFactory : ICommandFactory
    {
        private readonly JsonSerializerSettings serializerSettings;

        public JsonDeserializerCommandFactory() : this(new JsonSerializerSettings()) { }

        public JsonDeserializerCommandFactory(JsonSerializerSettings serializerSettings)
        {
            this.serializerSettings = serializerSettings;
        }

        public object Create(Type commandType, IDictionary<string, object> environment)
        {
            var request = new OwinRequest(environment);
            using (var streamReader = new StreamReader(request.Body))
            {
                var requestBody = streamReader.ReadToEnd();
                try
                {
                    var command = Convert.ChangeType(JsonConvert.DeserializeObject(requestBody, commandType, serializerSettings), commandType);
                    return command;
                }
                catch (JsonReaderException ex)
                {
                    throw new CommandConstructionException(commandType, requestBody, ex);
                }
            }
        }
    }
}