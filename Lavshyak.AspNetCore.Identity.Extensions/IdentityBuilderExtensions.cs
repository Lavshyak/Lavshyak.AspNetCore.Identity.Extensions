using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lavshyak.AspNetCore.Identity.Extensions;

public static class IdentityBuilderExtensions
{
    public static IdentityBuilder AddSignInManagerNormal<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        TSignInManager,
        TUser, TUserKey>(this IdentityBuilder builder)
        where TUserKey : IEquatable<TUserKey>
        where TUser : IdentityUser<TUserKey>
        where TSignInManager : SignInManager<TUser>
    {
        builder.AddSignInManager<TSignInManager>();

        builder.Services.TryAddScoped<TSignInManager>();
        builder.Services.TryAddScoped<SignInManager<TUser>, TSignInManager>();

        return builder;
    }
}