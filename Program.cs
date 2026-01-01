using AdidataDbContext.Models.Mysql.PTPDev;
using Microsoft.EntityFrameworkCore;
using PTP;

namespace PTP
{
  public class Program
  {

    public static void Main(string[] args)
    {

      var host = CreateHostBuilder(args).Build();

      // Jalankan migrasi otomatis
      using (var scope = host.Services.CreateScope())
      {
        var db = scope.ServiceProvider.GetRequiredService<PTPDevContext>();
        try
        {
             // Fix for "Data too long" error: Change column to LONGTEXT
             db.Database.ExecuteSqlRaw("ALTER TABLE projects MODIFY cover_image_url LONGTEXT;");
        }
        catch (Exception ex)
        {
             Console.WriteLine("Warning: Could not alter table (might already be correct): " + ex.Message);
        }
        //db.Database.Migrate();
      }

      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
