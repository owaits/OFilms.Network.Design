using OFilms.Network.Project.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFilms.Network.Project.Formats
{
    /// <summary>
    /// Implemented by any formatter that can take network project data and transform it to a file stream.
    /// </summary>
    /// <remarks>
    /// A formatter might take network switch configuration and allow it to be written to a device specific configuration.
    /// </remarks>
    internal interface IFormatter
    {
        /// <summary>
        /// Gets the name used to identify this formatter.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description of this formatter.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Writes to stream formatted contents to a stream.
        /// </summary>
        /// <param name="project">The project to be written to the stream.</param>
        /// <param name="device">The device information to write to the stream.</param>
        /// <param name="stream">The stream to write that formatted content to.</param>
        void WriteToStream(NetworkProject project, IDevice device, Stream stream);

        /// <summary>
        /// Reads the formatted content from a stream.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <returns>The device that was read from the formatted stream.</returns>
        IDevice ReadFromStream(Stream stream);
    }
}
