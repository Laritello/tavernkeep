﻿using System.Linq.Expressions;

namespace Tavernkeep.Core.Specifications
{
	public abstract class Specification<T> : ISpecification<T> where T : class
	{
		protected Specification(Expression<Func<T, bool>> criteria)
		{
			Criteria = criteria;
			Includes = [];
			IncludeStrings = [];
		}

		public Expression<Func<T, bool>> Criteria { get; protected set; }
		public List<Expression<Func<T, object>>> Includes { get; protected set; }

		public List<string> IncludeStrings { get; protected set; }

		public bool IsSatisfiedBy(T @object)
		{
			return Criteria.Compile().Invoke(@object);
		}

		public void AddInclude(Expression<Func<T, object>> expression) => Includes.Add(expression);

		public void AddInclude(string expression) => IncludeStrings.Add(expression);
	}
}
