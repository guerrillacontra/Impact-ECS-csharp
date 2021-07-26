using System;
using System.Collections.Generic;

namespace ImpactECS.Sets {
    public abstract class BaseComponentSet : IComponentSet {

        public event ComponentSetHandler Registered = delegate { };
        public event ComponentSetHandler Unregistered = delegate { };


        public IEnumerable<SetItem> RegisteredItems => _items;


        protected BaseComponentSet(Type[] supportedTypes) {
            _supportedTypes = supportedTypes;
        }
        
        public bool Matches(Entity entity) {
            
            for (var i = 0; i < _supportedTypes.Length; i++) {
                var type = _supportedTypes[i];
                if (!entity.HasComponent(type)) return false;
            }

            return true;
        }
        
        
        public void Register(Entity entity) {
            if (_entityLookup.ContainsKey(entity)) return;

            IComponent[] components = new IComponent[_supportedTypes.Length];

            for (int i = 0; i < components.Length; i++) {
                components[i] = entity.GetComponent(_supportedTypes[i]);
            }

            var item = new SetItem(entity, components);
            
            _items.Add(item);
            _entityLookup.Add(entity, item);

            OnItemRegistered(item);
            
            Registered.Invoke(this, ref item);
        }

        protected abstract void OnItemRegistered(SetItem item);

        
        public void Unregister(Entity entity) {
            if (!_entityLookup.TryGetValue(entity, out var item)) {
                return;
            }

            var index = _items.IndexOf(item);

            _items.RemoveAt(index);
            _entityLookup.Remove(entity);
            
            OnItemUnregistered(index, item);

            Unregistered.Invoke(this, ref item);
        }

        protected abstract void OnItemUnregistered(int index, SetItem value);

  
        
        
        private readonly Type[] _supportedTypes;
        private readonly List<SetItem> _items = new List<SetItem>();
        private readonly Dictionary<Entity, SetItem> _entityLookup = new Dictionary<Entity, SetItem>();
    }
}