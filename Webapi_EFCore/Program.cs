using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Webapi_EFCore.Data;
using Webapi_EFCore.Handlers;
using Webapi_EFCore.Profiles;
using Webapi_EFCore.Repositories;
using Webapi_EFCore.Repositories.Interfaces;
using Webapi_EFCore.Services;
using Webapi_EFCore.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
     .LogTo(Console.WriteLine, LogLevel.Information); //;
});

var config = builder.Configuration;
var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]);

// Register Authentication
//builder.Services.AddAuthentication("BasicAuthentication")
//    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,               // ��Ǩ�ͺ��� Issuer �ç�Ѻ����˹��������
        ValidateAudience = false,            // ����Ǩ�ͺ Audience (�����������������) 㹷����
        ValidateLifetime = true,             // ��Ǩ�ͺ��� Token �ѧ����������
        ValidateIssuerSigningKey = true,     // ��Ǩ�ͺ�����١��ͧ�ͧ Signature ���¡حᨷ���˹�
        ValidIssuer = config["Jwt:Issuer"],  // ��˹���Ңͧ Issuer ���١��ͧ
        IssuerSigningKey = new SymmetricSecurityKey(key), // ��˹��ح�����Ѻ��Ǩ�ͺ Signature
        RoleClaimType = "Role"
    };
});


builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<JwtTokenService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
