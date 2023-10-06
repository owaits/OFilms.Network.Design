using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OFilms.Network.Project;
using OFilms.Network.Project.Devices;
using OFilms.Network.Project.Principals;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OFilms.Network.Design
{
    /// <summary>
    /// Input control to enter an IPAddress on a form.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.Forms.InputBase&lt;System.Net.IPAddress&gt;" />
    public partial class InputIPAddress: InputBase<IPAddress?>
    {
        /// <summary>
        /// Gets or sets the ip address as string.
        /// </summary>
        /// <value>
        /// The ip address as string.
        /// </value>
        public string? IPAddressAsString 
        {
            get { return CurrentValue?.ToString(); }
            set
            {
                CurrentValue = (value != null ? IPAddress.Parse(value) : null);
                ValueChanged.InvokeAsync(CurrentValue);
            }
        }

        /// <summary>
        /// Parses a string to create an instance of <typeparamref name="TValue" />. Derived classes can override this to change how
        /// <see cref="P:Microsoft.AspNetCore.Components.Forms.InputBase`1.CurrentValueAsString" /> interprets incoming values.
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <param name="result">An instance of <typeparamref name="TValue" />.</param>
        /// <param name="validationErrorMessage">If the value could not be parsed, provides a validation error message.</param>
        /// <returns>
        /// True if the value could be parsed; otherwise false.
        /// </returns>
        protected override bool TryParseValueFromString(string? value, out IPAddress? result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            result = (value != null ? IPAddress.Parse(value) : null);
            validationErrorMessage = null;
            return true;
        }

    }
}
