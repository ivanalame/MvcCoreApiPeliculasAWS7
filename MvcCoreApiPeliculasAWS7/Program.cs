using Amazon.S3;
using MvcCoreApiPeliculasAWS7.Helpers;
using MvcCoreApiPeliculasAWS7.Models;
using MvcCoreApiPeliculasAWS7.Services;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ServiceApiPeliculas>();
builder.Services.AddTransient<ServicesStorageAWS>();
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddControllersWithViews();

//SECRETOS MANAGER
string jsonSecrets = await HelperSecretManager.GetSecretsAsync();
KeysModel keysModel = JsonConvert.DeserializeObject<KeysModel>(jsonSecrets);
builder.Services.AddSingleton<KeysModel>(x=>keysModel);
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
