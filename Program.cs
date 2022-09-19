using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Events.Models;
using Microsoft.IdentityModel.Tokens;
using Events.Repositories;
using Events.App_Start;
using Events.App_Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {           
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddDbContext<EventsDbContext>(opts => {
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:EventsConnection"]);
});


builder.Services.AddScoped<IEventsRepository, EFEventsRepository>();
builder.Services.AddScoped<IPersonsRepository, EFPersonsRepository>();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "Events Api",
        Description = "A simulated implementation of a CRUD application for working with events built on .Net Core",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact {
            Name = "Antonov Matvei",
            Email = "matvei.antonov.work@gmail.com"
        },
        License = new OpenApiLicense {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();


SeedData.EnsurePopulated(app);


app.Run();
