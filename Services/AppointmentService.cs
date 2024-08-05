namespace AppointmentAPI.Services
{
    using AppointmentAPI.Data;

    public class AppointmentService
    {
        private readonly HaircutSalonDbContext dbContext;

        public AppointmentService(HaircutSalonDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


    }
}
