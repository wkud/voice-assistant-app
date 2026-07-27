namespace VoiceAssistant.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityName) : base($"Entity {entityName} could not be found")
    {
    }

    public EntityNotFoundException(string entityName, Guid id) : base($"Entity {entityName} with ID: '{id}' could not be found")
    {
    }

    public EntityNotFoundException(string entityName, string propertyName, string propertyValue) : base($"Entity {entityName} with {propertyName}: '{propertyValue}' could not be found")
    {
    }
}