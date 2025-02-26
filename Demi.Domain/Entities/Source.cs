

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using Demi.Domain.Enums;

namespace Demi.Domain.Entities
{
	[Table("Sources")]
	public class Source
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }
		public string SourceUri { get; set; }
		public string GitBranch { get; set; }
		public string SubDomainRequested { get; set; }
		[NotMapped]
		public string FullSubDomainRequested => $"{SubDomainRequested}.sxb-platform.eu";

		public ESubDomainTypes SubDomainRequestedType { get; set; } = ESubDomainTypes.None;

		// Foreign Keys
		public string FormId { get; set; }
		public Form Form { get; set; } = null!;
	}


	public class SourceFluentValidator : ValidatorBase<Source>
	{
		public SourceFluentValidator()
		{
			RuleFor(x => x.SourceUri).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Source URI is required").Must(x => Uri.TryCreate(x, UriKind.Absolute, out _)).WithMessage("Source URI is not a valid URI");
			RuleFor(x => x.GitBranch).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Git Branch is required");
			RuleFor(x => x.SubDomainRequested).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Sub Domain Requested is required");
			RuleFor(x => x.SubDomainRequestedType).Cascade(CascadeMode.Stop).NotEqual(ESubDomainTypes.None).WithMessage("Sub Domain Requested Type is required");
		}
	}
}