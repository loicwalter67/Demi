using Microsoft.EntityFrameworkCore;
using Demi.Application.Interfaces;
using Demi.Components;
using Demi.Infrastructure.Context;
using Demi.Infrastructure.Repositories;
using MudBlazor.Services;
using Demi.Domain.Entities;
using Demi.Domain.Enums;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Demi.Components.Account;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("DemiDbConnection") ?? throw new InvalidOperationException("Connection string not found");
builder.Services.AddDbContextFactory<DemiDbContext>(options =>
	options.UseSqlServer(
		connectionString,
		o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
	)
);

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = IdentityConstants.ApplicationScheme;
	options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
	.AddIdentityCookies();

builder.Services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<DemiDbContext>()
	.AddSignInManager()
	.AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<User>, IdentityNoOpEmailSender>();

builder.Services.AddScoped<IFormRepository, FormRepository>();

builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();
