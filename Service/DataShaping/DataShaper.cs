﻿using System.Dynamic;
using System.Reflection;
using Contracts;
using Entities.Models;

namespace Service.DataShaping;

public class DataShaper<T> : IDataShaper<T> where T : class {
    private PropertyInfo[] Properties { get; set; }
    public DataShaper() {
        Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
    }

    public IEnumerable<ShapeEntity> ShapeData(IEnumerable<T> entities, string fieldsString) {
        var requiredProperties = GetRequiredProperties(fieldsString);
        return FetchData(entities, requiredProperties);
    }

    public ShapeEntity ShapeData(T entity, string fieldsString) {
        var requiredProperties = GetRequiredProperties(fieldsString);
        return FetchDataForEntity(entity, requiredProperties);

    }

    private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString) {
        var requiredProperties = new List<PropertyInfo>();
        if (!string.IsNullOrWhiteSpace(fieldsString)) {
            var fields = fieldsString.Split(',',
                StringSplitOptions.RemoveEmptyEntries);
            foreach (var field in fields) {
                var property = Properties
                    .FirstOrDefault(pi => pi.Name.Equals(field.Trim(),
                        StringComparison.InvariantCultureIgnoreCase));
                if (property == null)
                    continue;
                requiredProperties.Add(property);
            }
        } else {
            requiredProperties = Properties.ToList();
        }
        return requiredProperties;
    }

    private IEnumerable<ShapeEntity> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties) {

        return entities.Select(entity => FetchDataForEntity(entity, requiredProperties)).ToList();
    }
    private ShapeEntity FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties) {
        var shapedObject = new ShapeEntity();
        foreach (var property in requiredProperties) {
            var objectPropertyValue = property.GetValue(entity);
            shapedObject.Entity.TryAdd(property.Name, objectPropertyValue);
        }
        
        var objectProperty = entity.GetType().GetProperty("Id");
        shapedObject.Id = (Guid)objectProperty.GetValue(entity);
        return shapedObject;
    }
}