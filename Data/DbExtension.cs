namespace UnitessTestApp.Api.Data
{
    public static class DbExtension
    {
        public static void RunInitializeDbContext(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var dbContext = scope.ServiceProvider.GetService<DapperContext>();

            DbInitializer.Initialize(dbContext);
        }
    }
}
