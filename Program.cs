// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using Microsoft.EntityFrameworkCore;
using SquirrelTracker.Models;
using SquirrelTracker.Models.Items;
using SquirrelTracker.Models.Squirrels;
using SquirrelTracker.Models.Stashes;
using SquirrelTracker.Models.StashLines;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Dbcontext
builder.Services.AddDbContext<SquirrelDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SquirrelDb")));

// Services
builder.Services.AddScoped<SquirrelService>();
builder.Services.AddScoped<StashService>();
builder.Services.AddScoped<ItemService>();

// Repositories
builder.Services.AddScoped<ItemRepository>();
builder.Services.AddScoped<StashRepository>();
builder.Services.AddScoped<StashLineRepository>();
builder.Services.AddScoped<SquirrelRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// When a 404 (or other error) occurs, display the NotFoundPage
app.UseStatusCodePagesWithReExecute("/Home/NotFoundPage");

// Admin Area Routes
app.MapAreaControllerRoute( // Route to edit, add, remove an item from stash
    name: "editDeleteItem",
    areaName: "Admin",
    pattern: "Admin/Stash/{stashId}/Item/{action}/{itemId?}",
    defaults: new { controller = "Stash" });

app.MapAreaControllerRoute( // Route to remove a stash from squirrel
    name: "deleteStash",
    areaName: "Admin",
    pattern: "Admin/Squirrel/{squirrelId}/Stash/{action}/{stashId?}",
    defaults: new { controller = "Stash" });

app.MapAreaControllerRoute( // Route default for Admin area
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

// Public Routes
app.MapControllerRoute( // Route default for public/no area
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

/* Josiah Stoltzfus, 11/3/2025

======================================= Updates =======================================
=======================================================================================

- Fixed routing and endpoints for all controllers.
	- Removed all attribute routing.
	- Added map controller routes to Program.cs for Admin area.
	- Added map controller routes to Program.cs for default public non area.
	- Routing is much more straight forward and easy to follow.
	- All the buttons and <a> tags in the views have been updated to route correctly.

- Removed ItemController because it was never being used.

- Renamed all method names to Pascal naming instead of camel case to follow C# conventions.

- Double checked all validation for input fields and made sure all inputs are displaying red error messages.

- Handled all of the null checks in every controller.

- Added a NotFoundPage.cshtml to route the user if a null value is returned.

- Refactored GetAllItemsInStash(): Moved from StashService to ItemService.

- Added GetAddableItemsForStash() in ItemService.

- Improved the AddItem page so the user doesn't need to manually enter Item ID.
    - This helps avoid validation errors.
	- This also prevents a database exception being thrown because a stash is not allowed to have the same item twice. A stash ID and item ID are unique in the database.
	- The add items page also only shows items that are available to add for that stash. If the item already exists in the stash, it won't be available to add.

- Added ILogger to all controllers to log warning messages whenever a null is returned.

- Updated button colors for view, edit, delete.

- Created the Admin area and moved create, edit, and delete action methods and views into the Admin area controllers.
	- Action methods moved to Area/Admin/Controllers.
	- Views moved to Area/Admin/Views.
 */


