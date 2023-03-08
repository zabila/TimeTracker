namespace Entities.Models;

public class ShapeEntity {
    public ShapeEntity() {
        Entity = new Entity();
    }

    public Guid Id { get; set; }
    public Entity Entity { get; set; }
}