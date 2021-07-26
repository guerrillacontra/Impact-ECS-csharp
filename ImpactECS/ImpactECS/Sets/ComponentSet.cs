
using System;
using System.Collections.Generic;

namespace ImpactECS.Sets {
    public sealed  class ComponentSet<T1> : IComponentSet where T1:IComponent {

        public event ComponentSetHandler Registered = delegate { };
        public event ComponentSetHandler Unregistered = delegate { };
        

        public IEnumerable<SetItem> RegisteredItems => _registered;
        

        public bool Matches(Entity entity) {

            return entity.HasComponent<T1>();

        }

        public void Register(Entity entity) {

            if (_entityLookup.ContainsKey(entity)) return;

            IComponent[] components = {
                entity.GetComponent<T1>()
            };

            var item = new SetItem(entity, components);
            
            _registered.Add(item);
            _entityLookup.Add(entity, item);
            
            _tsItems.Add(new TypesafeItem(entity.GetComponent<T1>()));
            
            Registered.Invoke(this, ref item);
        }


        public void Unregister(Entity entity) {

            if (!_entityLookup.TryGetValue(entity, out var item)) {
                return;
            }

            var index = _registered.IndexOf(item);

            _registered.RemoveAt(index);
            _entityLookup.Remove(entity);
            _tsItems.RemoveAt(index);

            Unregistered.Invoke(this, ref item);
        }


        public void ForEach<T>(Action<Entity, T1> callback) where T:IComponent {

            for (int i = _tsItems.Count - 1; i >= 0; i--) {

                var item = _registered[i];
                var coms = _tsItems[i];

                callback.Invoke(item.Entity, coms.Com1);
            }
            
        }

        private readonly struct TypesafeItem {
            public readonly T1 Com1;

            public TypesafeItem( T1 com1) {
                Com1 = com1;
            }
        }
        
         
        private readonly List<SetItem> _registered = new List<SetItem>();
        private readonly List<TypesafeItem> _tsItems = new List<TypesafeItem>();
        private readonly Dictionary<Entity, SetItem> _entityLookup = new Dictionary<Entity, SetItem>();
    }
}