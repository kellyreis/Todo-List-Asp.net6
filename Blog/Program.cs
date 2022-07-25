
using Blog.Data;
using Microsoft.AspNetCore.Mvc;
using Blog.Services;

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

builder.Services.AddTransient<TokenService>(); // Sempre cria uma nova instancia

var app = builder.Build();

app.MapControllers();


app.Run();
