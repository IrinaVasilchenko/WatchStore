using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WatchesShop.Controllers;
using WatchesShop.Data;
using WatchesShop.Models;
using WatchesShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавляем MVC с поддержкой Razor Pages
builder.Services.AddControllersWithViews();

// Добавляем DbContext
builder.Services.AddDbContext<WatchContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрируем сервисы и контроллеры
builder.Services.AddScoped<IWatchService, WatchService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<Cart>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddTransient<AdminController>();

// Настраиваем кэширование и сессии
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Настраиваем аутентификацию
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login"; // путь к странице логина
    });

var app = builder.Build();

// Настройка конвейера
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

// Роутинг контроллеров
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Watches}/{action=Index}/{id?}");

// Создаём базу данных при запуске, если она не создана
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<WatchContext>();
    context.Database.Migrate(); // создаёт базу и применяет миграции
}

app.Run();