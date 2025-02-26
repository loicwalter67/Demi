using Demi.Domain.Entities;

namespace Demi.Application.Interfaces
{
	public interface IUserRepository
	{
		public IQueryable<User> GetAll();
		public User GetById(int id);
		public User Create(User user);

		public string Test();
	}
}
