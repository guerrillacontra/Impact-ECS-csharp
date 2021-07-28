namespace ImpactECS.Sets {
    public readonly struct SetItem {
            
        public readonly Entity Entity;
        public readonly object[] Components;

        public SetItem(Entity entity, object[] components) {
            Entity = entity;
            Components = components;
        }
    }
}