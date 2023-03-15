using Innowise.Clinic.Documents.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCrossServiceCommunication(builder.Configuration.GetSection("RabbitConfigurations"));
builder.Services.ConfigureBlobStorage(builder.Configuration.GetConnectionString("Azurite"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();