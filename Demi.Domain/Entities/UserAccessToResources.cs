using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demi.Domain.Enums;
using FluentValidation;

namespace Demi.Domain.Entities
{
	[Table("UserAccessToResources")]
	public class UserAccessToResources
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }

		public string Email { get; set; }
		public EResourceTypes ResourceType { get; set; } = EResourceTypes.Source;
		public EUserAccessLevels AccessLevel { get; set; } = EUserAccessLevels.Read;

		//Foreign Keys
		public string FormId { get; set; }
		public Form Form { get; set; } = null!;
	}

	public class UserAccessToResourcesFluentValidator : ValidatorBase<UserAccessToResources>
	{
		public UserAccessToResourcesFluentValidator()
		{
			RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Email field is required.").EmailAddress().WithMessage("Email is not a valid email address.");
		}
	}
}
