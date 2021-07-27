using System.Linq;
using ImpactECS;
using ImpactECS.Sets;
using NUnit.Framework;
using UnitTests.Mocks;

namespace UnitTests {
    [TestFixture]
    public class EntityContainerTest {


        [Test]
        public void TestAddRemove() {

            var entity = new Entity();
            entity.AddComponent(new TestComponent());
            entity.AddComponent(new TestComponent2());
    
            var container = new EntityContainer();
            container.AddEntity(entity);
            
            Assert.IsTrue(container.Contains(entity));

            var sets = new IComponentSet[] {
                new ComponentSet<TestComponent>(),
                new ComponentSet<TestComponent, TestComponent2>()
            };

            foreach (var set in sets) {

                var registered = false;
                
                set.Registered += (IComponentSet componentSet, ref SetItem item) => {
                    registered = true;
                };
                
                set.Unregistered += (IComponentSet componentSet, ref SetItem item) => {
                    registered = false;
                };

                
                container.RegisterSet(set);
                
                Assert.IsTrue(registered);
                
                Assert.IsTrue(set.RegisteredItems.ToArray()[0].Entity == entity);
                
                container.UnRegisterSet(set);
            }
        }
        
        [Test]
        public void TestEntityAddAndRemove() {

            var entity = new Entity();
            entity.AddComponent(new TestComponent());
            entity.AddComponent(new TestComponent2());

            var container = new EntityContainer();
            container.AddEntity(entity);
            
            Assert.IsTrue(container.Contains(entity));

            var set = new ComponentSet<TestComponent, TestComponent2>();
            
            Assert.IsEmpty(set.RegisteredItems);
            
            container.RegisterSet(set);
            
            Assert.IsNotEmpty(set.RegisteredItems);
            
            Assert.IsTrue(set.HasEntity(entity));
            
            entity.RemoveComponent<TestComponent>();
            
            Assert.IsEmpty(set.RegisteredItems);
        }
        
    }
}