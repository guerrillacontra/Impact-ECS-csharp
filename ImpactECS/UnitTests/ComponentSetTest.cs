using System.Linq;
using ImpactECS;
using NUnit.Framework;
using UnitTests.Mocks;

namespace UnitTests {
    [TestFixture]
    public class ComponentSetTest {
        [Test]
        public void TestAddRemoveFromEntity() {
            const int id = 1;

            var entity = new Entity(id);

            var set = new ComponentSet(new[] {typeof(TestComponent)});

            Assert.IsNotNull(set);

            Assert.IsFalse(set.Matches(entity));
            
            Assert.AreEqual(set.Items.Count(), 0);

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


            ComponentSet.SetItem setItem = default(ComponentSet.SetItem);
            
            set.Registered += delegate(ComponentSet componentSet, ref ComponentSet.SetItem item) {
                eventId++;
                setItem = item;
            };


            set.Unregistered += delegate(ComponentSet componentSet, ref ComponentSet.SetItem item) {
                eventId++;
                setItem = item;
            };

            Assert.DoesNotThrow(() => set.Register(entity));

            Assert.AreEqual(eventId, eventIdAdded);

            Assert.AreEqual(setItem.Entity, entity);
            
            Assert.AreEqual(set.Items.Count(), 1);

            Assert.AreEqual(setItem.Components[0], testCom);

            set.Unregister(entity);

            Assert.AreEqual(eventId, eventIdRemoved);
            
            Assert.AreEqual(set.Items.Count(), 0);
        }

        [Test]
        public void TestForEach() {
            
            const int id = 1;
            
            var entity = new Entity(id);

            var testCom = new TestComponent() {Text = "Hello world"};
            entity.AddComponent(testCom);

            var test2Com = new TestComponent2();
            entity.AddComponent(test2Com);
            
            var set = new ComponentSet(new[] {typeof(TestComponent), typeof(TestComponent2)});
            set.Register(entity);

            set.ForEach<TestComponent, TestComponent2>((e, test1, test2) => {
                
                Assert.AreEqual(e, entity);
                Assert.AreEqual(test1, testCom);
                Assert.AreEqual(test2, test2Com);
            });


        }
    }
}