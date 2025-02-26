using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Demi.Application.Interfaces;
using Demi.Domain.Entities;
using Demi.Domain.Enums;
using Demi.Infrastructure.Context;

namespace Demi.Infrastructure.Repositories
{
	public class FormRepository(IDbContextFactory<DemiDbContext> factory) : IFormRepository
	{
		private readonly DemiDbContext _context = factory.CreateDbContext();

		public async Task<Form> Create(Form form)
		{
			var @new = _context.Forms.Add(form);
			await _context.SaveChangesAsync();
			return @new.Entity;
		}

		private IQueryable<Form> GetAllAsQueryable()
		{
			return _context.Forms
				.Include(f => f.RequesterUser)
				.Include(f => f.ApprovalUser)
				.Include(f => f.Sources)
				.Include(f => f.UserPermissions)
				.AsQueryable();
		}

		public async Task<List<Form>> GetAllFor(ClaimsPrincipal user)
		{
			var isAdmin = user.IsInRole("Admin");
			var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

			var queryable = GetAllAsQueryable();

			var result = isAdmin
				? await queryable.ToListAsync()
				: await queryable.Where(f => f.RequesterId == userId).ToListAsync();

			return result;
		}

		public async Task<int> Count()
		{
			return await _context.Forms.CountAsync();
		}

		public async Task<Form> GetById(string id)
		{
			var queryable = GetAllAsQueryable();
			var result = await queryable.FirstOrDefaultAsync(i => i.Id == id);
			return result ?? throw new KeyNotFoundException();
		}

		public async Task<Form> Update(Form form)
		{
			var updated = _context.Forms.Update(form);
			await _context.SaveChangesAsync();
			return updated.Entity;
		}

		public async Task Delete(Form form)
		{
			_context.Forms.Remove(form);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteById(string id)
		{
			await Delete(await GetById(id));
		}

		public async Task ChangeStatus(string id, EStates state, string reason, string userId)
		{
			var form = await GetById(id);
			form.State = state;
			form.Reason = reason;
			form.ApprovalDate = DateTime.Now;
			form.ApprovalUserId = userId;
			await Update(form);
		}
	}
}
