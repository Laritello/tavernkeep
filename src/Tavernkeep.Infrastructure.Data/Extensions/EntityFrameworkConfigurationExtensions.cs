using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace Tavernkeep.Infrastructure.Data.Extensions
{
	public static class EntityFrameworkConfigurationExtensions
	{
		public static EntityTypeBuilder<T> OwnsJson<T, U>(this EntityTypeBuilder<T> builder, Expression<Func<T, U?>> navigationExpression, Action<OwnedNavigationBuilder<T, U>>? buildAction = null)
			where T : class, new()
			where U : class, new()
		{
			if (buildAction == null)
			{
				builder.OwnsOne(navigationExpression, b => b.ToJson());
			}
			else
			{
				builder.OwnsOne(navigationExpression, buildAction);
			}

			return builder;
		}
	}
}
