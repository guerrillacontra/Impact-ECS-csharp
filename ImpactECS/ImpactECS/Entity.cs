using System;
using System.Collections.Generic;

namespace ImpactECS {

    public sealed class Entity {

        public delegate void EntityComponentHandler(Entity entity, object component);

        public event EntityComponentHandler ComponentAdded = delegate { };
        public event EntityComponentHandler ComponentRemoved = delegate { };


        public int Id => _id;

        public IEnumerable<object> Components => _components;
        
        private readonly int _id;

        public Entity(int id = 0) {
            _id = id;
        }


        public bool HasComponent(Type componentType) {
            return _lookup.ContainsKey(componentType);
        }
        
        public bool HasComponent<T>() {
            return _lookup.ContainsKey(typeof(T));
        }
        
        public void AddComponent(object component) {

            var type = component.GetType();

            if (_lookup.ContainsKey(type)) {
                throw new Exceptions.ComponentExistsException(this, component);
            }

            _lookup.Add(type, component);
            _components.Add(component);
            
            ComponentAdded.Invoke(this, component);
        }

        public object GetComponent(Type componentType) {
            if (!_lookup.TryGetValue(componentType, out var component)) {
                throw new Exceptions.ComponentNotFoundException(this, componentType);
            }

            return component;
        }

        public T GetComponent<T>() {

            var type = typeof(T);

            if (!_lookup.TryGetValue(type, out var component)) {
                throw new Exceptions.ComponentNotFoundException(this, type);
            }

            return (T)component;
        }
        
        public void RemoveComponent<T>() {
            
            var type = typeof(T);

            if (!_lookup.TryGetValue(type, out var component)) {
                throw new Exceptions.ComponentNotFoundException(this, type);
            }

            _lookup.Remove(type);
            _components.Remove(component);
            
            ComponentRemoved.Invoke(this, component);
        }

        private readonly Dictionary<Type, object> _lookup = new Dictionary<Type, object>();
        private readonly List<object> _components = new List<object>();
    }
}