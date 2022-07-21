
using Blog.Data;
var builder = WebApplication.CreateBuilder(args);

//Adicionar MVC no projeto e retira validação automatica
builder
    .Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddDbContext<BlogDataContext>();

var app = builder.Build();

app.MapControllers();


app.Run();
