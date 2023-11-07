var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// app.UseHttpsRedirection();

//app.useSwagger();
//app.useSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
