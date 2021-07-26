using System;
using System.Collections.Generic;

namespace ImpactECS {
    public sealed class ComponentSet {

        public delegate void ComponentHandler(ComponentSet set,ref SetItem item);

        public event ComponentHandler Registered = delegate { };
        public event ComponentHandler Unregistered = delegate { };
        
        private readonly Type[] _supportedComponentTypes;

        public IEnumerable<SetItem> Items => _registered;

        public ComponentSet(Type[] supportedComponentTypes) {
            _supportedComponentTypes = supportedComponentTypes;
        }

        public void ForEach<T1>(Action<Entity, T1> callback) 
            where T1 : class, IComponent 
        {

            for (int i = _registered.Count - 1; i >= 0; i--) {

                var set = _registered[i];
                var components = set.Components;
                
                callback.Invoke(set.Entity, components[0] as T1);
            }
            
        }
        
        public void ForEach<T1, T2>(Action<Entity, T1, T2> callback) 
            where T1 : class, IComponent 
            where T2 : class, IComponent 
        {

            for (int i = _registered.Count - 1; i >= 0; i--) {

                var set = _registered[i];
                var components = set.Components;
                
                callback.Invoke(set.Entity, components[0] as T1, components[1] as T2);
            }
            
        }

        public bool Matches(Entity entity) {

            for (int i = 0; i < _supportedComponentTypes.Length; i++) {
                if (!entity.HasComponent(_supportedComponentTypes[i])) return false;
            }

            return true;

        }

        public void Register(Entity entity) {

            if (_entityLookup.ContainsKey(entity)) return;
            
            IComponent[] components = new IComponent[_supportedComponentTypes.Length];
            
            for (int i = 0; i < _supportedComponentTypes.Length; i++) {

                components[i] = entity.GetComponent(_supportedComponentTypes[i]);
            }

            var item = new SetItem(entity, components);
            
            _registered.Add(item);
            _entityLookup.Add(entity, item);
            Registered.Invoke(this, ref item);
        }

        public void Unregister(Entity entity) {

            if (!_entityLookup.TryGetValue(entity, out var item)) {
                return;
            }

            _registered.Remove(item);
            _entityLookup.Remove(entity);

            Unregistered.Invoke(this, ref item);
        }

        private readonly List<SetItem> _registered = new List<SetItem>();
        private readonly Dictionary<Entity, SetItem> _entityLookup = new Dictionary<Entity, SetItem>();

        public readonly struct SetItem {
            
            public readonly Entity Entity;
            public readonly IComponent[] Components;

            public SetItem(Entity entity, IComponent[] components) {
                Entity = entity;
                Components = components;
            }
        }


    }
}