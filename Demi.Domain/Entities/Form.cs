using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Demi.Domain.Enums;
using Demi.Domain;
using FluentValidation;

namespace Demi.Domain.Entities
{
	/// <summary>
	/// Represents a form that can be submitted by a user.
	/// </summary>
	[Table("Forms")]
	public class Form
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; } = string.Empty;
		public string ProjectName { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string AdditionalNotes { get; set; } = string.Empty;
		public DateTime RequestDate { get; set; }

		public EStates State { get; set; } = EStates.Pending;
		public string Reason { get; set; } = string.Empty;
		public DateTime? ApprovalDate { get; set; }

		// Foreign Keys
		public string RequesterId { get; set; } = string.Empty;
		public User RequesterUser { get; set; } = null!;

		public string? ApprovalUserId { get; set; }
		public User? ApprovalUser { get; set; }

		public IList<Source> Sources { get; set; } = [];
		public IList<UserAccessToResources> UserPermissions { get; set; } = [];
	}

	public class FormFluentValidator : ValidatorBase<Form>
	{
		public FormFluentValidator()
		{
			RuleFor(x => x.ProjectName).NotEmpty().WithMessage("Project name field is required.");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Description field is required.");
			RuleFor(x => x.RequestDate).NotEmpty();
			RuleFor(x => x.State).NotEmpty();

			RuleFor(x => x.RequesterId).NotEmpty();
			RuleFor(x => x.Sources).NotEmpty();
		}
	}


}
