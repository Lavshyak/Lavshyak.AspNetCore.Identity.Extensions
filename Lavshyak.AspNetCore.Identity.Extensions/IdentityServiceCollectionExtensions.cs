using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Lavshyak.AspNetCore.Identity.Extensions;

public static class IdentityServiceCollectionExtensions
{
    public static void TryAddIdentityDeps<T>(this IServiceCollection services)
        where T : class, IPostConfigureOptions<SecurityStampValidatorOptions>
    {
        services. /*Try*/AddHttpContextAccessor();

        services.TryAddScoped<IdentityErrorDescriber>();
        services.TryAddEnumerable(ServiceDescriptor
            .Singleton<IPostConfigureOptions<SecurityStampValidatorOptions>, T>());
        services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
    }

    /// <summary>
    /// <![CDATA[services.AddIdentityDeps<DefaultPostConfigureSecurityStampValidatorOptions>();]]>
    /// </summary>
    /// <param name="services"></param>
    public static void TryAddIdentityDeps(this IServiceCollection services)
    {
        services.TryAddIdentityDeps<DefaultPostConfigureSecurityStampValidatorOptions>();
    }


    public static IdentityBuilder AddIdentityNormal<TUser,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        TRole>(
        this IServiceCollection services,
        Action<IdentityOptions>? setupAction = null)
        where TUser : class
        where TRole : class
    {
        services.TryAddScoped<IUserValidator<TUser>, UserValidator<TUser>>();
        services.TryAddScoped<IPasswordValidator<TUser>, PasswordValidator<TUser>>();
        services.TryAddScoped<IPasswordHasher<TUser>, PasswordHasher<TUser>>();
        services.TryAddScoped<IRoleValidator<TRole>, RoleValidator<TRole>>();
        services.TryAddScoped<SecurityStampValidator<TUser>>();
        services.TryAddScoped<TwoFactorSecurityStampValidator<TUser>>();
        services.TryAddScoped<IUserClaimsPrincipalFactory<TUser>, UserClaimsPrincipalFactory<TUser, TRole>>();
        services.TryAddScoped<IUserConfirmation<TUser>, DefaultUserConfirmation<TUser>>();
        services.TryAddScoped<UserManager<TUser>>();
        services.TryAddScoped<RoleManager<TRole>>();

        if (setupAction != null)
        {
            services.Configure(setupAction);
        }

        return new IdentityBuilder(typeof(TUser), typeof(TRole), services);
    }
}