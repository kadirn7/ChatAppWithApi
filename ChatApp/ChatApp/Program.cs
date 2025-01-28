using System.Text;
using ChatApp;
using ChatApp.Data;
using ChatApp.Data.Entities.Db;
using ChatApp.Helpers;
using ChatApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
 // Add this using directive
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chat App Web API", Version = "V1" });
    //JWT Authentication(yetkilendirme)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = "Bearer"}
        },
        Array.Empty<string>()    
        
    }
    });
      
});

builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddRepository(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddSingleton<ISignalrConnection, SignalrConnection>();

//Cors
var corsPolicyName = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });

});


//Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options=>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"]))
        };
    });





var app = builder.Build();

app.UseCors(corsPolicyName);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();  ---->>>>   bunu neden yorum satýrýna aldýk ??

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chathub");
});


app.UseAuthorization();

//app.MapControllers();

app.Run();
