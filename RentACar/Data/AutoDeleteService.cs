using Microsoft.EntityFrameworkCore;
using RentACar.Data;

public class AutoDeleteService : BackgroundService
{
    private readonly ILogger<AutoDeleteService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AutoDeleteService(ILogger<AutoDeleteService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Auto delete service is running.");

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                
                var expiredReservations = await dbContext.Rezervacije
                    .Where(r => r.DatumPovratka < DateTime.Now).ToListAsync();

                foreach (var reservation in expiredReservations)
                {
                    
                    dbContext.Rezervacije.Remove(reservation);

                    
                    var delivery = await dbContext.Dostave.FirstOrDefaultAsync(d => d.NarudzbaId == reservation.Id);
                    if (delivery != null)
                    {
                        dbContext.Dostave.Remove(delivery);
                    }
                }

                await dbContext.SaveChangesAsync();
            }

            await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken); 
        }
    }
}
