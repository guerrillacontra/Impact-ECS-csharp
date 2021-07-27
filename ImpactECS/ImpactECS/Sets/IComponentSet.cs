using System;
using System.Collections.Generic;

namespace ImpactECS.Sets {

    public delegate void ComponentSetHandler(IComponentSet set,ref SetItem item);

    public interface IComponentSet {


        event ComponentSetHandler Registered, Unregistered;

        IEnumerable<SetItem> RegisteredItems { get; }


        void Register(Entity entity);
        void Unregister(Entity entity);
        bool Matches(Entity entity);

        bool HasEntity(Entity entity);

    }
}