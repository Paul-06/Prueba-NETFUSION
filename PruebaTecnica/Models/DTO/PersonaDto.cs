namespace PruebaTecnica.Models.DTO;

public record PersonaDto(
    int IdPersona,
    string NombreCompleto,
    string FechaNacimiento,
    string FechaCreacion
);