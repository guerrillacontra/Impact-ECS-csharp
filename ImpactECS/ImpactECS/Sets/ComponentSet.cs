
using System;
using System.Collections.Generic;

namespace ImpactECS.Sets {
    public sealed  class ComponentSet<T1> : BaseComponentSet where T1:IComponent {

        public ComponentSet() :
            base(new[] {typeof(T1)}) {
        }

        public void ForEach(Action<Entity, T1> callback) {

            for (int i = _tsItems.Count - 1; i >= 0; i--) {

                var item = _tsItems[i];

                callback.Invoke(item.Item.Entity, item.Com1);
            }
        }

        private readonly struct TypesafeItemWrapper {
            public readonly SetItem Item;
            public readonly T1 Com1;

            public TypesafeItemWrapper( SetItem item, T1 com1) {
                Item = item;
                Com1 = com1;
            }
        }
         
        private readonly List<TypesafeItemWrapper> _tsItems = new List<TypesafeItemWrapper>();

        protected override void OnItemRegistered(SetItem item) {
            _tsItems.Add(new TypesafeItemWrapper(item, item.Entity.GetComponent<T1>()));
        }

        protected override void OnItemUnregistered(int index, SetItem value) {
           _tsItems.RemoveAt(index);
        }

        public override bool Matches(Entity entity) {
            return entity.HasComponent<T1>();
        }
    }
}