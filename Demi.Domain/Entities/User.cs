using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Demi.Domain.Entities
{
	/// <summary>
	/// Represents a user that can submit forms. Admins can approve forms.
	/// </summary>
	//[Table("Users")]
	public class User : IdentityUser
	{
		public string Name { get; set; } = string.Empty;
		public ICollection<Form> RequestedForms { get; set; } = [];
		public ICollection<Form> ApprovedForms { get; set; } = [];
	}
}
