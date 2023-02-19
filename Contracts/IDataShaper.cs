using System.Dynamic;
using Entities.Models;

namespace Contracts; 

public interface IDataShaper<T> {
    IEnumerable<ShapeEntity> ShapeData(IEnumerable<T> entities, string fieldsString);
    ShapeEntity ShapeData(T entity, string fieldsString);
}