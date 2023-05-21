using System.ComponentModel.DataAnnotations;
using System.Reflection;
using UniversityScheduler.Api.Core.Models.Attributes;
using UniversityScheduler.Api.Core.Models.University_Entities;

namespace UniversityScheduler.Api.Core.Extensions;

public static class EntityListExtension
{
    private static readonly Dictionary<Type, PropertyInfo[]> ValidateAttributePropertiesCache = new();

    public static void ValidateList<TEntity>(this List<TEntity> entities)
        where TEntity : IEntity
    {
        ValidateListElementsProperties(entities);
    }

    public static void Validate<TEntity>(this TEntity entity)
        where TEntity : IEntity
    {
        ValidateElementProperties(entity);
    }

    [Obsolete]
    private static void CheckForDuplicates<TEntity>(this TEntity entity, List<TEntity> cachedEntities)
        where TEntity : IEntity
    {
        if (cachedEntities.Any(entityItem => entityItem.Id == entity.Id))
            throw new ValidationException($"Invalid entity - an entity with the id {entity.Id} already exists!");
    }

    private static void ValidateElementProperties<TEntity>(this TEntity entity)
    {
        var entityType = entity!.GetType();

        if (!ValidateAttributePropertiesCache.TryGetValue(entityType, out var propertiesWithValidateAttribute))
        {
            propertiesWithValidateAttribute = entityType
                .GetProperties()
                .Where(property => Attribute
                    .IsDefined(property, typeof(ValidateAttribute)))
                .ToArray();

            ValidateAttributePropertiesCache[entityType] = propertiesWithValidateAttribute;
        }

        foreach (var propertyInfo in propertiesWithValidateAttribute)
        {
            var validateAttribute =
                propertyInfo.GetCustomAttribute(typeof(ValidateAttribute)) as ValidateAttribute;

            var propertyValue = propertyInfo.GetValue(entity);

            if (propertyValue != null)
            {
                if ((propertyValue is int && (int)propertyValue == default) ||
                    (propertyValue is string && (string)propertyValue == string.Empty))
                    continue;

                if (validateAttribute != null && validateAttribute.Validate(propertyValue) == false)
                    throw new ValidationException(
                        "Invalid property for an entity - cannot use the entity!");
            }
        }
    }

    [Obsolete]
    private static void CheckForDuplicatesInsideTheNewList<TEntity>(this List<TEntity> entities) where TEntity : IEntity
    {
        if (entities.Count != entities.Select(entity => entity.Id).Distinct().Count())
            throw new ValidationException(
                "Invalid list of entities - possibly id duplications inside the new list of entities!");
    }

    private static void ValidateListElementsProperties<TEntity>(this List<TEntity> entities) where TEntity : IEntity
    {
        foreach (var entity in entities)
        {
            var entityType = entity!.GetType();

            if (!ValidateAttributePropertiesCache.TryGetValue(entityType,
                    out var propertiesWithValidateAttribute))
            {
                propertiesWithValidateAttribute = entityType
                    .GetProperties()
                    .Where(property => Attribute
                        .IsDefined(property, typeof(ValidateAttribute)))
                    .ToArray();

                ValidateAttributePropertiesCache[entityType] = propertiesWithValidateAttribute;
            }


            foreach (var propertyInfo in propertiesWithValidateAttribute)
            {
                var validateAttribute =
                    propertyInfo.GetCustomAttribute(typeof(ValidateAttribute)) as ValidateAttribute;

                var propertyValue = propertyInfo.GetValue(entity);

                if (propertyValue != null)
                {
                    if ((propertyValue is int && (int)propertyValue == default) ||
                        (propertyValue is string && (string)propertyValue == string.Empty))
                        continue;

                    if (validateAttribute != null && validateAttribute.Validate(propertyValue) == false)
                        throw new ValidationException(
                            "Invalid property for an entity - cannot use the list of entities!");
                }
            }
        }
    }

    [Obsolete]
    private static void CheckForDuplicates<TEntity>(this List<TEntity> entities, List<TEntity> cachedEntities)
        where TEntity : IEntity
    {
        var tempDictionary = new Dictionary<int, object?>();

        foreach (var entity in
                 cachedEntities) tempDictionary.Add(entity.Id, null); //the cached list does not contain any duplicates

        foreach (var entity in entities)
            if (tempDictionary.ContainsKey(entity.Id))
                throw new ValidationException(
                    "Invalid list of entities - there are entities having certain id's in the cached list!");
    }

    [Obsolete]
    public static bool VerifyExistence<TEntity>(this List<TEntity> entities, int id) where TEntity : IEntity
    {
        return entities.Any(entity => entity.Id == id);
    }

    [Obsolete]
    public static async Task CheckSubclassExistenceAsync<TEntity, TInstanceToVerify>(this TEntity entity,
        int idOfInstanceToVerify, List<TInstanceToVerify> cachedInstances) where TInstanceToVerify : IEntity
    {
        await Task.Run(() =>
        {
            if (cachedInstances.All(instance => instance.Id != idOfInstanceToVerify))
                throw new ValidationException(
                    $"The current entity having the id {idOfInstanceToVerify} not exist in the cached list of entities");
        });
    }
}