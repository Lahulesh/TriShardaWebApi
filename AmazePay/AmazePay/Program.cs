using Microsoft.AspNetCore.Cors;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddControllers();

//// Add services to the container.
//var provider = builder.Services.BuildServiceProvider();
//var configuration = provider.GetRequiredService<IConfiguration>();
////builder.Services.AddCors(options =>
////{
////    var frontEndUrl = configuration.GetValue<string>("FrontEndUrl");
////    options.AddDefaultPolicy(builder =>
////    {
////        builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
////    });
////});
////var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
////builder.Services.AddCors(options =>
////{
////    options.AddPolicy(name: MyAllowSpecificOrigins,
////                      policy =>
////                      {
////                          policy.WithOrigins("http://localhost:3000",
////                                              "http://localhost:3000");
////                      });
////});

//builder.Services.AddCors(p =>
//{
//    p.AddDefaultPolicy(build =>
//                      {
//                          build.WithOrigins("http://localhost:3000","http://localhost:8976");
//                          build.AllowAnyMethod();
//                          build.AllowAnyHeader();
//                      });
//});

//builder.Services.AddControllers();

//EnableCorsAttribute corsAttribute = new EnableCorsAttribute("*");

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//var prodDb = configuration["ConnectionStrings:Database"];

//app.UseHttpsRedirection();
//app.UseCors();

//app.UseRouting();
//app.UseAuthorization();
//app.UseAuthentication();
//app.MapControllers();

//app.Run();


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("http://localhost:8976").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();

}
//app cors
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("corsapp");
app.UseAuthorization();

//app.UseCors(prodCorsPolicy);

app.MapControllers();

app.Run();

