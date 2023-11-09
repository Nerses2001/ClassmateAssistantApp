using DataLayer;
using DataLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using DataLayer.Repository;
using BusinessLayer.IService;
using BusinessLayer.Service;
using HouseBooking.Middleware;
using ClassmateAssistantApp.Extansions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                    ));

builder.Services.AddRouting();


// Add services to the container.

builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IUserCourseRepository, UserCourseRepository>();

builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();


builder.Services.AddAutoMapperService();
builder.Services.AddIdentityServers();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerServices();


var app = builder.Build();

var routeBuilder = new RouteBuilder(app);

routeBuilder.MapRoute("controller", async context =>
{
    await context.Response.WriteAsync("{controller} template");
});

routeBuilder.MapRoute("{controller}/{action}", async context =>
{
    await context.Response.WriteAsync("{controller}/{action} template");
});

app.UseCors("app-cors-policy");
app.UseRouting();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "House Booking");
    });
}


app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<TokenManagerMiddleware>();

app.MapControllers();


app.Run();
