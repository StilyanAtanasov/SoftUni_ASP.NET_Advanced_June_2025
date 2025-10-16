using System.Collections.Immutable;

namespace CinemaApp.Data.Models.Conversions;

public static class MovieShowtimes 
{
    public static readonly ImmutableArray<int> Hours = [12, 15, 18, 20, 22];
}

