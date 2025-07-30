using System;

namespace PopsicleFactory.Application.Dtos
{
    public record PopsicleDto(
        Guid Id,
        string Name,
        string Flavor,
        decimal Price
    );
    public record CreatePopsicleDto(
        string Name,
        string Flavor,
        decimal Price
    );
    public record UpdatePopsicleDto(
        string Name,
        string Flavor,
        decimal Price
    );
}
