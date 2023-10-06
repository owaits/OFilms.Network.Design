using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OFilms.GreenGo.Project.JsonConverters
{
    /// <summary>
    /// This converter allows IPAddress JSON serialization. Please add this type converter to any IPAddress properties.
    /// </summary>
    /// <seealso cref="System.Text.Json.Serialization.JsonConverter&lt;System.Net.IPAddress&gt;" />
    internal class IPAddressJsonConverter : JsonConverter<IPAddress?>
    {
        /// <summary>
        /// Reads and converts the JSON to type <typeparamref name="T" />.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="typeToConvert">The type to convert.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        public override IPAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? ipAsString = JsonSerializer.Deserialize<string>(ref reader, options);

            IPAddress.TryParse(ipAsString, out IPAddress? address);
            return address ?? default;
        }

        /// <summary>
        /// Writes a specified value as JSON.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="value">The value to convert to JSON.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        public override void Write(Utf8JsonWriter writer, IPAddress? value, JsonSerializerOptions options)
        {
            if(value != null)
            {
                JsonSerializer.Serialize<string>(writer, value.ToString(), options);
            }            
        }
    }
}
