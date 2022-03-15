using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using PinkPunther.Backend.Models;
using PinkPunther.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<PinkPuntherDatabaseSettings>(
    builder.Configuration.GetSection("PinkPuntherDatabase"));

builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddSingleton<PunsService>();
builder.Services.AddSingleton<PunSubmissionsService>();

builder.Services.AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = "GitHub";
    })
    .AddCookie(o =>
    {
        o.ExpireTimeSpan = TimeSpan.FromDays(30);
        o.Cookie.MaxAge = o.ExpireTimeSpan;
        o.SlidingExpiration = true;
    })
    .AddOAuth("GitHub", o =>
    {
        o.ClientId = builder.Configuration["GitHub:ClientId"];
        o.ClientSecret = builder.Configuration["GitHub:ClientSecret"];

        o.CallbackPath = new PathString("/api/v1/auth/github-callback");
        o.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
        o.TokenEndpoint = "https://github.com/login/oauth/access_token";
        o.UserInformationEndpoint = "https://api.github.com/user";

        o.SaveTokens = true;

        o.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
        o.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
        o.ClaimActions.MapJsonKey("urn:github:login", "login");
        o.ClaimActions.MapJsonKey("urn:github:url", "html_url");
        o.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");

        o.Events = new OAuthEvents
        {
            OnCreatingTicket = async ctx =>
            {
                var req = new HttpRequestMessage(HttpMethod.Get, ctx.Options.UserInformationEndpoint);
                req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ctx.AccessToken);

                var res = await ctx.Backchannel.SendAsync(req, HttpCompletionOption.ResponseHeadersRead,
                    ctx.HttpContext.RequestAborted);
                res.EnsureSuccessStatusCode();

                var json = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
                ctx.RunClaimActions(json.RootElement);
            }
        };
    });

builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();