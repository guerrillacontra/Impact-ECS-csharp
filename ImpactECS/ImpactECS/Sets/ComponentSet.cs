using System;
using System.Collections.Generic;

namespace ImpactECS.Sets {
    public sealed class ComponentSet<T1> : BaseComponentSet where T1 : IComponent {
        public ComponentSet() :
            base(new[] {typeof(T1)}) {
        }

        public void ForEach(Action<Entity, T1> callback) {
            for (int i = _tsItems.Count - 1; i >= 0; i--) {
                var item = _tsItems[i];

                callback.Invoke(item.Item.Entity, item.Com1);
            }
        }


        protected override void OnItemRegistered(SetItem item) {
            _tsItems.Add(new TypesafeItemWrapper(item, item.Entity.GetComponent<T1>()));
        }

        protected override void OnItemUnregistered(int index, SetItem value) {
            _tsItems.RemoveAt(index);
        }

        private readonly struct TypesafeItemWrapper {
            public readonly SetItem Item;
            public readonly T1 Com1;

            public TypesafeItemWrapper(SetItem item, T1 com1) {
                Item = item;
                Com1 = com1;
            }
        }


        private readonly List<TypesafeItemWrapper> _tsItems = new List<TypesafeItemWrapper>();
    }

    public sealed class ComponentSet<T1, T2> : BaseComponentSet
        where T1 : IComponent
        where T2 : IComponent {
        public ComponentSet() :
            base(new[] {typeof(T1), typeof(T2)}) {
        }

        public void ForEach(Action<Entity, T1, T2> callback) {
            for (int i = _tsItems.Count - 1; i >= 0; i--) {
                var item = _tsItems[i];

                callback.Invoke(item.Item.Entity, item.Com1, item.Com2);
            }
        }

        protected override void OnItemRegistered(SetItem item) {
            _tsItems.Add(new TypesafeItemWrapper(item, item.Entity.GetComponent<T1>(), item.Entity.GetComponent<T2>()));
        }

        protected override void OnItemUnregistered(int index, SetItem value) {
            _tsItems.RemoveAt(index);
        }


        private readonly struct TypesafeItemWrapper {
            public readonly SetItem Item;
            public readonly T1 Com1;
            public readonly T2 Com2;

            public TypesafeItemWrapper(SetItem item, T1 com1, T2 com2) {
                Item = item;
                Com1 = com1;
                Com2 = com2;
            }
        }

        private readonly List<TypesafeItemWrapper> _tsItems = new List<TypesafeItemWrapper>();
    }
}