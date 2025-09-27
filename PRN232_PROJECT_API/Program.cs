using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PRN232_PROJECT_API.Model;
using PRN232_PROJECT_API.Repository;
using PRN232_PROJECT_API.Repository.IRepository;
using PRN232_PROJECT_API.Service;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ========== 1. Add DbContext ==========
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
          .AddEntityFrameworkStores<AppDbContext>()
          .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = builder.Configuration["Jwt:Issuer"],
                       ValidAudience = builder.Configuration["Jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                   };
               });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("ADMIN"));
    options.AddPolicy("AuthorOnly", policy => policy.RequireRole("AUTHOR"));
    options.AddPolicy("ModeratorOnly", policy => policy.RequireRole("MODERATOR"));
    options.AddPolicy("ViewOnly", policy => policy.RequireRole("VIEWER"));
});
// ========== 3. Add Controller + Swagger ==========
builder.Services.AddControllers().AddOData(options =>
{
    options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IArticlesRepository, ArticlesRepository>();
builder.Services.AddAutoMapper(typeof(PRN232_PROJECT_API.Mapper.AutoMapperProfile));
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IUserArticleRepository, UserArticleRepository>();
builder.Services.AddScoped<IRegisterRepository, RegisterRepository>();

//builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PROJECT_HE172177", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "INPUT token here:"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
});

var app = builder.Build();

// ========== 4. Seed Roles + Users ==========
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedRolesAndUsersAsync(services);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PROJECT_HE172177 v1");
    });
}

// ========== 5. Middleware ==========
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;
    if (response.StatusCode == 403)
    {
        response.ContentType = "application/json";
        await response.WriteAsync("{\"error\": \"Access Denied\"}");
    }
});


app.MapControllers();

app.Run();

// ========== 6. SEED DATA ==========
static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    


    string[] roles = { "ADMIN", "AUTHOR", "MODERATOR", "VIEWER" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // 1. ADMIN
    var admin = new ApplicationUser
    {
        UserName = "admin@mail.com",
        Email = "admin@mail.com",
        FullName = "Admin User",
        EmailConfirmed = true
    };
    await CreateUserWithRoleAsync(userManager, admin, "Admin@123", "ADMIN");

    // 2. AUTHOR
    var author = new ApplicationUser
    {
        UserName = "author@mail.com",
        Email = "author@mail.com",
        FullName = "Author User",
        EmailConfirmed = true
    };
    await CreateUserWithRoleAsync(userManager, author, "Author@123", "AUTHOR");

    // 3. MODERATOR
    var moderator = new ApplicationUser
    {
        UserName = "moderator@mail.com",
        Email = "moderator@mail.com",
        FullName = "Moderator User",
        EmailConfirmed = true
    };
    await CreateUserWithRoleAsync(userManager, moderator, "Moderator@123", "MODERATOR");

    // 4. VIEWER
    var viewer = new ApplicationUser
    {
        UserName = "viewer@mail.com",
        Email = "viewer@mail.com",
        FullName = "Viewer User",
        EmailConfirmed = true
    };
    await CreateUserWithRoleAsync(userManager, viewer, "Viewer@123", "VIEWER");
}

static async Task CreateUserWithRoleAsync(UserManager<ApplicationUser> userManager, ApplicationUser user, string password, string role)
{
    var existing = await userManager.FindByEmailAsync(user.Email);
    if (existing == null)
    {
        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }
}
