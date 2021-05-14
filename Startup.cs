using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace net_core_db
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        // создаем два объекта User
                        User user1 = new User { Name = "Tom", Age = 33 };
                        User user2 = new User { Name = "Alice", Age = 26 };

                        // добавляем их в бд
                        db.Users.AddRange(user1, user2);
                        db.SaveChanges();
                    }
                    // получение данных
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        // получаем объекты из бд и выводим на консоль
                        var users = db.Users.ToList();
                        Console.WriteLine("Users list:");
                        foreach (User u in users)
                        {
                            await context.Response.WriteAsync($"{u.Id}.{u.Name} - {u.Age}");
                        }
                    }
                    
                });
            });
        }
    }
}
