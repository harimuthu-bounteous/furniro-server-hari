using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using Supabase;

using furniro_server_hari.Interfaces;
using furniro_server_hari.Repository;
using furniro_server_hari.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});
builder.Services.AddControllers();

// builder.Services.AddControllers().AddJsonOptions(options => {
//     options.JsonSerializerOptions.PropertyNamingPolicy = null; // Adjust if needed
//     options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull; // Ignore nulls
// });

// builder.Services.AddSingleton(new SupabaseService(url, key));

string url = builder.Configuration["Supabase:Url"] ?? throw new ArgumentNullException("Supabase:Url", "Supabase URL is missing from configuration.");
string key = builder.Configuration["Supabase:ApiKey"] ?? throw new ArgumentNullException("Supabase:ApiKey", "Supabase ApiKey is missing from configuration.");

var supabaseClient = new Client(url, key);
supabaseClient.InitializeAsync().Wait();


builder.Services.AddSingleton(supabaseClient);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AuthService>();

var bytes = Encoding.UTF8.GetBytes(builder.Configuration["Authentication:JwtSecret"] ?? throw new ArgumentNullException("Authentication:JwtSecret", "Authentication JwtSecret is missing from configuration."));

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(bytes),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Authentication:ValidAudience"],
            ValidIssuer = builder.Configuration["Authentication:ValidIssuer"],
            ValidateLifetime = true,
        };



        // options.Events = new JwtBearerEvents
        // {
        //     OnAuthenticationFailed = context =>
        //     {
        //         Console.WriteLine("Authentication failed: " + context.Exception.Message);
        //         return Task.CompletedTask;
        //     },
        //     OnTokenValidated = context =>
        //     {
        //         Console.WriteLine("Token validated.");
        //         return Task.CompletedTask;
        //     },
        //     OnMessageReceived = context =>
        //     {
        //         Console.WriteLine("Message received.");
        //         return Task.CompletedTask;
        //     }
        // };

    });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
