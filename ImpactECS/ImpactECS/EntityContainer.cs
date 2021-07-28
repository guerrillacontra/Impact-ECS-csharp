using System.Collections.Generic;
using ImpactECS.Sets;

namespace ImpactECS {
    
    public sealed class EntityContainer {

        public delegate void EntityHandler(EntityContainer container, Entity entity);

        public event EntityHandler Registered = delegate { };
        public event EntityHandler UnRegistered = delegate { };

        public IEnumerable<Entity> Entities => _entities;
        
        
        public bool Contains(Entity entity) {
            return _entities.Contains(entity);
        }
        
        
        public void AddEntity(Entity entity) {

            if (_entities.Contains(entity)) {
                throw new Exceptions.EntityAllReadyRegisteredException(entity);
            }
            
            _entities.Add(entity);
       
            entity.ComponentAdded += EntityStateChangedHandler;
            entity.ComponentRemoved += EntityStateChangedHandler;
            
            foreach (var set in _sets) {

                if (set.Matches(entity)) {
                    set.Register(entity);
                }
                
            }
            
            Registered.Invoke(this, entity);
        }

        
        public void RemoveEntity(Entity entity) {
            
            if (!_entities.Contains(entity)) {
                throw new Exceptions.EntityNotRegisteredException(entity);
            }
            
            _entities.Remove(entity);
            
            foreach (var set in _sets) {

                if (set.Matches(entity)) {
                    set.Unregister(entity);
                }
            }
            
            entity.ComponentAdded -= EntityStateChangedHandler;
            entity.ComponentRemoved -= EntityStateChangedHandler;
            
            
            UnRegistered.Invoke(this, entity);
        }


        private void EntityStateChangedHandler(Entity entity, object component) {
          
            foreach (var set in _sets) {
            
                if (set.HasEntity(entity)) {
                    //remove sets that are not compatible anymore
                    if (!set.Matches(entity)) {
                        set.Unregister(entity);
                    }
                }
                else {
                    //register to sets the entity is compatible with now
                    if (set.Matches(entity)) {
                        set.Register(entity);
                    }
                }
            }
            
        }
        



        public void RegisterSet(IComponentSet set) {
            _sets.Add(set);

            foreach (var entity in _entities) {
                if (set.Matches(entity)) {
                    set.Register(entity);
                }
            }
        }
        
        public void UnRegisterSet(IComponentSet set) {
            _sets.Remove(set);
            
            foreach (var entity in _entities) {
                if (set.Matches(entity)) {
                    set.Unregister(entity);
                }
            }
        }


        private readonly List<Entity> _entities = new List<Entity>();
        private readonly List<IComponentSet> _sets = new List<IComponentSet>();

       
    }
}