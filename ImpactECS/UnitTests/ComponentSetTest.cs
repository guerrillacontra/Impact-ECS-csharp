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
        
        [Test]
        public void TestForEach2Param() {
            
            const int id = 1;
            
            var entity = new Entity(id);

            var com1 = new TestComponent() {Text = "Hello world"};
            entity.AddComponent(com1);
            
            var com2 = new TestComponent2() {Text = "Bye world"};
            entity.AddComponent(com2);

            var set = new ComponentSet<TestComponent, TestComponent2>();
            set.Register(entity);

            set.ForEach((e, test1, test2) => {
                
                Assert.AreEqual(e, entity);
                Assert.AreEqual(test1, com1);
                Assert.AreEqual(test2, com2);
            });
        }
        
        [Test]
        public void TestForEach3Param() {
            
            const int id = 1;
            
            var entity = new Entity(id);

            var components = new object[] {new TestComponent(), new TestComponent2(), new TestComponent3()};

            foreach (var com in components) {
                entity.AddComponent(com);
            }

            var set = new ComponentSet<TestComponent, TestComponent2, TestComponent3>();
            set.Register(entity);

            set.ForEach((e, test1, test2, test3) => {

                Assert.AreEqual(e, entity);
                
                var tests = new object[] {test1, test2, test3};

                for (int i = 0; i < tests.Length; i++) {
                    Assert.AreEqual(tests[i], components[i]);
                }
            });
        }
        
        [Test]
        public void TestForEach4Param() {
            
            const int id = 1;
            
            var entity = new Entity(id);

            var components = new object[] {new TestComponent(), new TestComponent2(), new TestComponent3(), new TestComponent4()};

            foreach (var com in components) {
                entity.AddComponent(com);
            }

            var set = new ComponentSet<TestComponent, TestComponent2, TestComponent3, TestComponent4>();
            set.Register(entity);

            set.ForEach((e, test1, test2, test3, test4) => {

                Assert.AreEqual(e, entity);
                
                var tests = new object[] {test1, test2, test3, test4};

                for (int i = 0; i < tests.Length; i++) {
                    Assert.AreEqual(tests[i], components[i]);
                }
            });
        }
        
        [Test]
        public void TestForEach5Param() {
            
            const int id = 1;
            
            var entity = new Entity(id);

            var components = new object[] {new TestComponent(), new TestComponent2(), new TestComponent3(), new TestComponent4(), new TestComponent5()};

            foreach (var com in components) {
                entity.AddComponent(com);
            }

            var set = new ComponentSet<TestComponent, TestComponent2, TestComponent3, TestComponent4, TestComponent5>();
            set.Register(entity);

            set.ForEach((e, test1, test2, test3, test4, test5) => {

                Assert.AreEqual(e, entity);
                
                var tests = new object[] {test1, test2, test3, test4, test5};

                for (int i = 0; i < tests.Length; i++) {
                    Assert.AreEqual(tests[i], components[i]);
                }
            });
        }
        
        [Test]
        public void TestForEach6Param() {
            
            const int id = 1;
            
            var entity = new Entity(id);

            var components = new object[] {new TestComponent(), new TestComponent2(), new TestComponent3(), new TestComponent4(), new TestComponent5(), new TestComponent6()};

            foreach (var com in components) {
                entity.AddComponent(com);
            }

            var set = new ComponentSet<TestComponent, TestComponent2, TestComponent3, TestComponent4, TestComponent5, TestComponent6>();
            set.Register(entity);

            set.ForEach((e, test1, test2, test3, test4, test5, test6) => {

                Assert.AreEqual(e, entity);
                
                var tests = new object[] {test1, test2, test3, test4, test5, test6};

                for (int i = 0; i < tests.Length; i++) {
                    Assert.AreEqual(tests[i], components[i]);
                }
            });
        }
    }
}