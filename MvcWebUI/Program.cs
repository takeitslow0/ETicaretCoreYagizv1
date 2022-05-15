using Business.Services;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Repoistories;
using DataAccess.Repoistories.Bases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//IoC Container: Inversion of Control Container: Baðýmlýlýklarýn yönetimi: Autofac, Ninject
//builder.Services.AddDbContext<ETicaretContext>();
//builder.Services.AddScoped<KategoriRepoBase, KategoriRepo>();
builder.Services.AddScoped<IKategoriService, KategoriService>();
//builder.Services.AddSingleton<IKategoriService, KategoriService>();
//builder.Services.AddTransient<IKategoriService, KategoriService>();
builder.Services.AddScoped<IUrunService, UrunService>();
builder.Services.AddScoped<IHesapService, HesapService>();
builder.Services.AddScoped<IKullaniciService, KullaniciService>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
