namespace Investigacion1.Features.WeatherForecast.GetWeatherForecast;

public static class GetWeatherForecastEndpoint
{
    public static void MapGetWeatherForecastEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/weatherforecast", () => GetWeatherForecastQuery.Handle())
           .WithName("GetWeatherForecast");
    }
}