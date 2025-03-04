﻿using FluentValidation;
using Demi.Domain.Entities;

namespace Demi.Domain
{
	public abstract class ValidatorBase<T> : AbstractValidator<T> where T : class
	{
		public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
		{
			var result = await ValidateAsync(ValidationContext<T>.CreateWithOptions((T)model, x => x.IncludeProperties(propertyName)));
			if (result.IsValid)
				return Array.Empty<string>();
			return result.Errors.Select(e => e.ErrorMessage);
		};
	}
}
