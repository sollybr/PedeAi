using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddRazorPages();

builder.Services.AddReverseProxy()
    .LoadFromMemory(new[]
    {
        new Yarp.ReverseProxy.Configuration.RouteConfig
        {
            RouteId = "api",
            Match = new() { Path = "/api/{**catch-all}" },
            ClusterId = "apiCluster"
        }
    },
    new[]
    {
        new Yarp.ReverseProxy.Configuration.ClusterConfig
        {
            ClusterId = "apiCluster",
            Destinations = new Dictionary<string, Yarp.ReverseProxy.Configuration.DestinationConfig>
            {
                { "api", new() { Address = "https://localhost:7081/" } }
            }
        }
    });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Error";
    });

builder.Services.AddHsts(options =>
{
    options.MaxAge = TimeSpan.FromDays(1); // https://aka.ms/aspnetcore-hsts
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapReverseProxy();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();