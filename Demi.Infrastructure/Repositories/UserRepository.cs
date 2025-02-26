using Microsoft.EntityFrameworkCore;
using Demi.Application.Interfaces;
using Demi.Domain.Entities;
using Demi.Infrastructure.Context;

namespace Demi.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly DemiDbContext _context;
		public UserRepository(IDbContextFactory<DemiDbContext> factory)
		{
			_context = factory.CreateDbContext();
		}

		public User Create(User user)
		{
			var @new = _context.Users.Add(user);
			_context.SaveChanges();
			return @new.Entity;
		}

		public IQueryable<User> GetAll()
		{
			return _context.Users.AsQueryable();
		}

		public User GetById(int id)
		{
			return _context.Users.Find(id) ?? throw new KeyNotFoundException();
		}

		public string Test()
		{
			//generate new GUID
			var guid = Guid.NewGuid();
			return guid.ToString();

		}
	}
}
