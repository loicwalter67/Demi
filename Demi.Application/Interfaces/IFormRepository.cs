using System.Security.Claims;
using Demi.Domain.Entities;
using Demi.Domain.Enums;

namespace Demi.Application.Interfaces
{
	/// <summary>
	/// Interface for <see cref="FormRepository"/>
	/// </summary>
	public interface IFormRepository
	{
		public Task<List<Form>> GetAllFor(ClaimsPrincipal user);
		public Task<int> Count();
		public Task<Form> GetById(string id);
		public Task<Form> Create(Form form);
		public Task<Form> Update(Form form);
		public Task Delete(Form form);
		public Task DeleteById(string id);

		public Task ChangeStatus(string id, EStates state, string reason, string userId);
	}
}
