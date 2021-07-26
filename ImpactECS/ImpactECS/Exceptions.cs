using System;

namespace ImpactECS {
    public static class Exceptions {
        
        
        public sealed class ComponentExistsException : Exception {


            public ComponentExistsException(Entity entity, IComponent component) 
                : base($"Component {component.GetType().FullName} all ready exists in entity {entity.Id}")
            {
                
            }
            
        }

        public sealed class ComponentNotFoundException : Exception {
            public ComponentNotFoundException(Entity entity, Type type)
            :base($"Could not find component of type {type.FullName} in entity {entity.Id}")
            {
             
            }
        }
    }
}