using System.Linq;
using ImpactECS;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnitTests.Mocks;

namespace UnitTests {
    [TestFixture]
    public class EntityTest {
        
        [Test]
        public void TestInstantiation() {
            const int id = 1;

            var entity = new Entity(id);

            Assert.NotNull(entity);
            Assert.AreEqual(entity.Id, id);
        }


        [Test]
        public void TestAddRemoveComponent() {
            const int id = 1;

            //increments per event
            int eventId = 0;
            
            const int eventIdComponentAdded = 1;
            const int eventIdComponentRemoved = 2;

            var entity = new Entity(id);
            entity.ComponentAdded += (entity1, component) => eventId++;
            entity.ComponentRemoved += (entity1, component) => eventId++;

            var testCom = new TestComponent() {Text = "Hello world"};

            Assert.DoesNotThrow(() => entity.AddComponent(testCom));
            Assert.AreEqual(eventId, eventIdComponentAdded);

            Assert.IsTrue(entity.HasComponent<TestComponent>());

            Assert.AreEqual(entity.Components.Count(), 1);

            var com = entity.GetComponent<TestComponent>();

            Assert.AreEqual(testCom, com);

            Assert.DoesNotThrow(() => entity.RemoveComponent<TestComponent>());

            Assert.AreEqual(eventId, eventIdComponentRemoved);

            Assert.IsFalse(entity.HasComponent<TestComponent>());

            Assert.AreEqual(entity.Components.Count(), 0);
        }

    }
}