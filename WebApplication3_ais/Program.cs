using static WebApplication3_ais.Program;

namespace WebApi
{

    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer(); // Za minimal api (brez kontrolerjev)
            builder.Services.AddSwaggerGen(); // Generira Open-Api json dokument za izpostavljene končne točke


            var AllowAny = "_allowAny";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: AllowAny, policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });


           


            var app = builder.Build(); // Zgradi aplikacijo


            app.UseCors(AllowAny);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection(); // Če imamo omogočen HTTPS. Brez tega privzeto HTTP
            // Call the Users method
            Users(app);
            app.Run();


        }

    }


}

   

