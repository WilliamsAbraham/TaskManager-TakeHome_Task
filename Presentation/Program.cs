
using Application.Interfaces;
using Application.Services;
using Application.Services.NotificationService;
using Application.Services.ProjectService;
using Application.Services.TaskService;
using Application.Services.UserService;
using infrastructure;
using infrastructure.Externals;
using infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi.Models;
using Presentation.BackGrounndServices;
using SendGrid;

var builder = WebApplication.CreateBuilder(args);

var sendgridkey = builder.Configuration["SendGridSettings:ApiKey"];

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddSingleton(new SendGridClient(sendgridkey));
builder.Services.AddSingleton<IEmailSender,SenGridEmailService>();

builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationRepo, NotificationRepository>();
builder.Services.AddScoped<NotificationRepository>();


builder.Services.AddScoped<IProjectRepo, ProjectRepository>();
builder.Services.AddScoped<ProjectRepository>();

builder.Services.AddScoped<IUSerRepo,UserRepository>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskRepo,TaskRepository>();
builder.Services.AddScoped<TaskRepository>();

builder.Services.AddScoped<NoticeBackGroundService>();

builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnect")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Task Management API",
        Description = "An ASP.NET Core Web API for managing Tasks",
        Contact = new OpenApiContact
        {
            Name = "Abraham WIlliams",
            Url = new Uri("https://www.linkedin.com/in/abraham-williams-aa7bb0146")
        },
        
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
