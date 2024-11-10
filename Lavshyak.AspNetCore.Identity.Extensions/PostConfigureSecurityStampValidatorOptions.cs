using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Lavshyak.AspNetCore.Identity.Extensions;

internal sealed class DefaultPostConfigureSecurityStampValidatorOptions : IPostConfigureOptions<SecurityStampValidatorOptions>
{
    public DefaultPostConfigureSecurityStampValidatorOptions(TimeProvider timeProvider)
    {
        TimeProvider = timeProvider;
    }

    private TimeProvider TimeProvider { get; }

    public void PostConfigure(string? name, SecurityStampValidatorOptions options)
    {
        options.TimeProvider ??= TimeProvider;
    }
}