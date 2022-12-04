using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using BloodCore.Domain;
using System.Buffers.Text;
using System.Buffers;
using BloodCore.Extensions;

namespace BloodLoop.WebApi.Middlewares
{
    public class IdentityJsonConverter<TIdentity> : JsonConverter<TIdentity> where TIdentity : Identity<TIdentity>
    {
        public override TIdentity Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                // try to parse number directly from bytes
                var token = reader.GetString();
                var decodedToken = GuidExtensions.ParseShort(token);
                var identity = (TIdentity)typeof(TIdentity).GetMethod(nameof(Identity<TIdentity>.Of)).Invoke(null, new object[]{ decodedToken });
                return identity;
            }

            // fallback to default handling
            return null;
        }

        public override void Write(Utf8JsonWriter writer, TIdentity value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Id.ToShort());
        }
    }
}
