namespace PruebaTecnica.Models.DTO
{
    public static class EntityExtensions
    {
        public static PersonaDto ToDto(this Persona persona)
        {
            var nombreCompleto = $"{persona.Nombres} {persona.ApellidoPaterno} {persona.ApellidoMaterno}";
            return new PersonaDto(
                persona.Id,
                nombreCompleto,
                persona.FechaNacimiento.ToString("dd/MM/yyyy"),
                persona.FechaCreacion.ToString("dd/MM/yyyy HH:mm:ss")
            );
        }
    }
}
