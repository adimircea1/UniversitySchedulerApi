namespace UniversityScheduler.Api.Core.Extensions;

public static class UpdateObjectExtension
{
    public static void UpdateByReflection<TEntity>(this TEntity entity, object? updatedObject)
    {
        if (updatedObject == null)
            throw new Exception("Cannot update the old entity because the updated version is null!");

        if (typeof(TEntity) != updatedObject.GetType())
            throw new Exception(
                "Cannot update the old entity - entity type does not match with the updated object type!");

        var updatedProperties = updatedObject.GetType()
            .GetProperties()
            .Where(property => property.GetValue(updatedObject) != null)
            .ToList();

        foreach (var updatedProperty in updatedProperties)
        {
            var value = updatedProperty.GetValue(updatedObject);

            if ((value is int && (int)value == default) || (value is string && (string)value == string.Empty)) continue;

            if (entity is not null)
            {
                var entityPropertyInfo = entity.GetType()
                    .GetProperty(updatedProperty.Name);

                if (entityPropertyInfo is not null)
                    entityPropertyInfo.SetValue(entity, updatedProperty
                        .GetValue(updatedObject));
            }
        }
    }
}