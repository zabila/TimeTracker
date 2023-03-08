﻿using Entities.Models;

namespace Entities.LinkModels;

public class LinkResponse {
    public bool HasLinks { get; set; }
    public List<Entity> ShapeEntities { get; set; } = new List<Entity>();
    public LinkCollectionWrapper<Entity> LinkedEntities { get; set; } = new LinkCollectionWrapper<Entity>();
}