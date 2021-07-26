using System.Linq;
using ImpactECS;
using ImpactECS.Sets;
using NUnit.Framework;
using UnitTests.Mocks;

namespace UnitTests {
    [TestFixture]
    public class ComponentSetTest {
        [Test]
        public void TestAddRemoveFromEntity() {
            const int id = 1;

            var entity = new Entity(id);

            var set = new ComponentSet<TestComponent>();

            Assert.IsNotNull(set);

            Assert.IsFalse(set.Matches(entity));
            
            Assert.AreEqual(set.RegisteredItems.Count(), 0);

            var testCom = new TestComponent() {Text = "Hello world"};
            entity.AddComponent(testCom);

            Assert.IsTrue(set.Matches(entity));

            entity.RemoveComponent<TestComponent>();

            Assert.IsFalse(set.Matches(entity));

            entity.AddComponent(testCom);

            //increments per event
            int eventId = 0;

            const int eventIdAdded = 1;
            const int eventIdRemoved = 2;


            var setItem = default(SetItem);

            set.Registered += (IComponentSet componentSet, ref SetItem item) => {
                eventId++;
                setItem = item;
            };

            set.Unregistered += (IComponentSet componentSet, ref SetItem item) => {
                eventId++;
                setItem = item;
            };

            Assert.DoesNotThrow(() => set.Register(entity));

            Assert.AreEqual(eventId, eventIdAdded);

            Assert.AreEqual(setItem.Entity, entity);
            
            Assert.AreEqual(set.RegisteredItems.Count(), 1);

            Assert.AreEqual(setItem.Components[0], testCom);

            set.Unregister(entity);

            Assert.AreEqual(eventId, eventIdRemoved);
            
            Assert.AreEqual(set.RegisteredItems.Count(), 0);
        }

        [Test]
        public void TestForEach1Param() {
            
            const int id = 1;
            
            var entity = new Entity(id);

            var testCom = new TestComponent() {Text = "Hello world"};
            entity.AddComponent(testCom);

            var test2Com = new TestComponent2();
            entity.AddComponent(test2Com);
            
            var set = new ComponentSet<TestComponent>();
            set.Register(entity);

            set.ForEach((e, test1) => {
                
                Assert.AreEqual(e, entity);
                Assert.AreEqual(test1, testCom);
            });


        }
    }
}