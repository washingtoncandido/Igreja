using FICR.Cooperation.Humanism.Data.Context;
using FICR.Cooperation.Humanism.Data.Contracts.Base;
using FICR.Cooperation.Humanism.Data.Repository;
using FICR.Cooperation.Humanism.Services;
using FICR.Cooperation.Humanism.Services.Contracts;
using FICR.Cooperation.Humanism.Services.Twilio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CooperationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CooperationSqlServer")));

// Configure a injeção de dependência do UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IEventService, EventServices>();
builder.Services.AddTransient<IContactService, ContactServices>();

// Adiciona o MenssagemService com as credenciais do Twilio
builder.Services.AddTransient<IMenssagemService>(provider =>
{
    var unitOfWork = provider.GetRequiredService<IUnitOfWork>();
    var accountSid = builder.Configuration["accountSid"];
    var authToken = builder.Configuration["authToken"];
    var twilioWhatsAppNumber = "+14155238886";

    return new MenssagemService(unitOfWork, accountSid, authToken, twilioWhatsAppNumber);
});

// Configura CORS
builder.Services.AddCors(c =>
{
    c.AddPolicy("EventoIgreja", policyBuild =>
    {
        policyBuild.WithOrigins("http://localhost:3000");
        policyBuild.AllowAnyHeader();
        policyBuild.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("EventoIgreja");

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});

app.Run();