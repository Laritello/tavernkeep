using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace Tavernkeep.Infrastructure.Data.Extensions
{
    public static class EntityFrameworkConfigurationExtensions
    {
        public static EntityTypeBuilder<T> OwnsJson<T, U>(this EntityTypeBuilder<T> builder, Expression<Func<T, U?>> navigationExpression)
            where T : class, new()
            where U : class, new()
        {
            builder.OwnsOne(navigationExpression, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            });

            return builder;
        }
    }
}
