using EventMangamentAPI.Service.Implement;
using EventMangamentAPI.Service.Interface;
using EventMangamentAPI.ViewModel.Validation;
using EventMangamentAPI.ViewModel;
using FluentValidation;
using FluentValidation.AspNetCore;
using EventMangamentAPI.MapperProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(c => c.AddProfile(new EventProfile()));
builder.Services.AddAutoMapper(typeof(EventProfile));


builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IOrganizerService, OrganizerService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IReviewService, ReviewService>();


builder.Services.AddScoped<IValidator<CreateEventVM>, CreateEventValidator>();
builder.Services.AddScoped<IValidator<UpdateEventVM>, UpdateEventValidator>();
builder.Services.AddScoped<IValidator<CreateOrganizerVM>, CreateOrganizerValidator>();
builder.Services.AddScoped<IValidator<UpdateOrganizerVM>, UpdateOrganizerValidator>();
builder.Services.AddScoped<IValidator<CreateParticipantVM>, CreateParticipantValidator>();
builder.Services.AddScoped<IValidator<UpdateParticipantVM>, UpdateParticipantValidator>();
builder.Services.AddScoped<IValidator<CreateRegistrationVM>, CreateRegistrationValidator>();
builder.Services.AddScoped<IValidator<UpdateRegistrationVM>, UpdateRegistrationValidator>();
builder.Services.AddScoped<IValidator<CreateReviewVM>, CreateReviewValidator>();
builder.Services.AddScoped<IValidator<UpdateReviewVM>, UpdateReviewValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateEventValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
