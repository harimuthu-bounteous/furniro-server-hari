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
// builder.Services.AddControllers().AddJsonOptions(options =>
//     {
//         options.JsonSerializerOptions.PropertyNamingPolicy = null; // Adjust if needed
//         options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull; // Ignore nulls
//     });


string url = builder.Configuration["Supabase:Url"] ?? throw new ArgumentNullException("Supabase:Url", "Supabase URL is missing from configuration.");
string key = builder.Configuration["Supabase:ApiKey"] ?? throw new ArgumentNullException("Supabase:ApiKey", "Supabase ApiKey is missing from configuration.");

builder.Services.AddSingleton(new SupabaseService(url, key));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAllOrigins");

app.Run();
