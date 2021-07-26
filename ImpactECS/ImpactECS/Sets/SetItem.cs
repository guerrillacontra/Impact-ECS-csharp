namespace ImpactECS.Sets {
    public readonly struct SetItem {
            
        public readonly Entity Entity;
        public readonly IComponent[] Components;

        public SetItem(Entity entity, IComponent[] components) {
            Entity = entity;
            Components = components;
        }
    }
}