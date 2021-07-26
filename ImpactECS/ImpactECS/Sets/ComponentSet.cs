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
    
    public sealed class ComponentSet<T1, T2, T3> : BaseComponentSet
        where T1 : IComponent
        where T2 : IComponent 
        where T3 : IComponent 
    {
        public ComponentSet() :
            base(new[] {typeof(T1), typeof(T2), typeof(T3)}) {
        }

        public void ForEach(Action<Entity, T1, T2, T3> callback) {
            for (int i = _tsItems.Count - 1; i >= 0; i--) {
                var item = _tsItems[i];

                callback.Invoke(item.Item.Entity, item.Com1, item.Com2, item.Com3);
            }
        }

        protected override void OnItemRegistered(SetItem item) {
            _tsItems.Add(
                new TypesafeItemWrapper(
                item,
                item.Entity.GetComponent<T1>(),
                item.Entity.GetComponent<T2>(),
                item.Entity.GetComponent<T3>()
                ));
        }

        protected override void OnItemUnregistered(int index, SetItem value) {
            _tsItems.RemoveAt(index);
        }


        private readonly struct TypesafeItemWrapper {
            public readonly SetItem Item;
            public readonly T1 Com1;
            public readonly T2 Com2;
            public readonly T3 Com3;

            public TypesafeItemWrapper(SetItem item, T1 com1, T2 com2, T3 com3) {
                Item = item;
                Com1 = com1;
                Com2 = com2;
                Com3 = com3;
            }
        }

        private readonly List<TypesafeItemWrapper> _tsItems = new List<TypesafeItemWrapper>();
    }
    
    public sealed class ComponentSet<T1, T2, T3, T4> : BaseComponentSet
        where T1 : IComponent
        where T2 : IComponent 
        where T3 : IComponent 
        where T4 : IComponent 
    {
        public ComponentSet() :
            base(new[] {typeof(T1), typeof(T2), typeof(T3), typeof(T4)}) {
        }


        public delegate void LongAction(Entity entity, T1 com1, T2 com2, T3 com3, T4 com4);

        public void ForEach(LongAction callback) {
            for (int i = _tsItems.Count - 1; i >= 0; i--) {
                var item = _tsItems[i];

                callback.Invoke(item.Item.Entity, item.Com1, item.Com2, item.Com3, item.Com4);
            }
        }

        protected override void OnItemRegistered(SetItem item) {
            _tsItems.Add(
                new TypesafeItemWrapper(
                    item,
                    item.Entity.GetComponent<T1>(),
                    item.Entity.GetComponent<T2>(),
                    item.Entity.GetComponent<T3>(),
                    item.Entity.GetComponent<T4>()
                ));
        }

        protected override void OnItemUnregistered(int index, SetItem value) {
            _tsItems.RemoveAt(index);
        }


        private readonly struct TypesafeItemWrapper {
            public readonly SetItem Item;
            public readonly T1 Com1;
            public readonly T2 Com2;
            public readonly T3 Com3;
            public readonly T4 Com4;

            public TypesafeItemWrapper(SetItem item, T1 com1, T2 com2, T3 com3, T4 com4) {
                Item = item;
                Com1 = com1;
                Com2 = com2;
                Com3 = com3;
                Com4 = com4;
            }
        }

        private readonly List<TypesafeItemWrapper> _tsItems = new List<TypesafeItemWrapper>();
    }
    
    public sealed class ComponentSet<T1, T2, T3, T4, T5> : BaseComponentSet
        where T1 : IComponent
        where T2 : IComponent 
        where T3 : IComponent 
        where T4 : IComponent 
        where T5 : IComponent 
    {
        public ComponentSet() :
            base(new[] {typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5)}) {
        }


        public delegate void LongAction(Entity entity, T1 com1, T2 com2, T3 com3, T4 com4, T5 com5);

        public void ForEach(LongAction callback) {
            for (int i = _tsItems.Count - 1; i >= 0; i--) {
                var item = _tsItems[i];

                callback.Invoke(item.Item.Entity, item.Com1, item.Com2, item.Com3, item.Com4, item.Com5);
            }
        }

        protected override void OnItemRegistered(SetItem item) {
            _tsItems.Add(
                new TypesafeItemWrapper(
                    item,
                    item.Entity.GetComponent<T1>(),
                    item.Entity.GetComponent<T2>(),
                    item.Entity.GetComponent<T3>(),
                    item.Entity.GetComponent<T4>(),
                    item.Entity.GetComponent<T5>()
                ));
        }

        protected override void OnItemUnregistered(int index, SetItem value) {
            _tsItems.RemoveAt(index);
        }


        private readonly struct TypesafeItemWrapper {
            public readonly SetItem Item;
            public readonly T1 Com1;
            public readonly T2 Com2;
            public readonly T3 Com3;
            public readonly T4 Com4;
            public readonly T5 Com5;
            public TypesafeItemWrapper(SetItem item, T1 com1, T2 com2, T3 com3, T4 com4, T5 com5) {
                Item = item;
                Com1 = com1;
                Com2 = com2;
                Com3 = com3;
                Com4 = com4;
                Com5 = com5;
            }
        }

        private readonly List<TypesafeItemWrapper> _tsItems = new List<TypesafeItemWrapper>();
    }
    
    public sealed class ComponentSet<T1, T2, T3, T4, T5, T6> : BaseComponentSet
        where T1 : IComponent
        where T2 : IComponent 
        where T3 : IComponent 
        where T4 : IComponent 
        where T5 : IComponent 
        where T6 : IComponent 
    {
        public ComponentSet() :
            base(new[] {typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6)}) {
        }


        public delegate void LongAction(Entity entity, T1 com1, T2 com2, T3 com3, T4 com4, T5 com5, T6 com6);

        public void ForEach(LongAction callback) {
            for (int i = _tsItems.Count - 1; i >= 0; i--) {
                var item = _tsItems[i];

                callback.Invoke(item.Item.Entity, item.Com1, item.Com2, item.Com3, item.Com4, item.Com5, item.Com6);
            }
        }

        protected override void OnItemRegistered(SetItem item) {
            _tsItems.Add(
                new TypesafeItemWrapper(
                    item,
                    item.Entity.GetComponent<T1>(),
                    item.Entity.GetComponent<T2>(),
                    item.Entity.GetComponent<T3>(),
                    item.Entity.GetComponent<T4>(),
                    item.Entity.GetComponent<T5>(),
                    item.Entity.GetComponent<T6>()
                ));
        }

        protected override void OnItemUnregistered(int index, SetItem value) {
            _tsItems.RemoveAt(index);
        }


        private readonly struct TypesafeItemWrapper {
            public readonly SetItem Item;
            public readonly T1 Com1;
            public readonly T2 Com2;
            public readonly T3 Com3;
            public readonly T4 Com4;
            public readonly T5 Com5;
            public readonly T6 Com6;
            
            public TypesafeItemWrapper(SetItem item, T1 com1, T2 com2, T3 com3, T4 com4, T5 com5, T6 com6) {
                Item = item;
                Com1 = com1;
                Com2 = com2;
                Com3 = com3;
                Com4 = com4;
                Com5 = com5;
                Com6 = com6;
            }
        }

        private readonly List<TypesafeItemWrapper> _tsItems = new List<TypesafeItemWrapper>();
    }
}