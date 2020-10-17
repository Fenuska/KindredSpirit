using NUnit.Framework;
using System.Collections.Generic;

namespace KindredSpiritCore
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void Primitive_Int_OK()
        {
            var valueA = 5;
            var valueB = 5;


            var result = Kindred.AreItemsEqual(valueA, valueB);

            Assert.IsTrue(result);
        }

        [Test]
        public void Primitive_Int_KO()
        {
            var valueA = 5;
            var valueB = 7;

            var result = Kindred.AreItemsEqual(valueA, valueB);

            Assert.IsTrue(!result);
        }

        [Test]
        public void Primitive_string_OK()
        {
            var valueA = "this is a test";
            var valueB = "this is a test";

            var result = Kindred.AreItemsEqual(valueA, valueB);

            Assert.IsTrue(result);
        }

        [Test]
        public void Primitive_string_KO()
        {
            var valueA = "this is value A";
            var valueB = "this is value B";

            var result = Kindred.AreItemsEqual(valueA, valueB);

            Assert.IsTrue(!result);
        }

        [Test]
        public void List_OK()
        {
            var valueA = new List<int> { 1, 2, 3, 4 };
            var valueB = new List<int> { 1, 2, 3, 4 };

            var result = Kindred.AreItemsEqual(valueA, valueB);

            Assert.IsTrue(result);
        }

        [Test]
        public void List_KO()
        {
            var valueA = new List<int> { 1, 2, 3, 4 };
            var valueB = new List<int> { 1, 2, 3 };

            var result = Kindred.AreItemsEqual(valueA, valueB);

            Assert.IsTrue(!result);
        }

        [Test]
        public void Dictionary_OK()
        {
            var valueA = new Dictionary<string, int> { { "1", 1 }, { "2", 2 }, { "3", 3 }, { "4", 4 } };
            var valueB = new Dictionary<string, int> { { "1", 1 }, { "2", 2 }, { "3", 3 }, { "4", 4 } };

            var result = Kindred.AreItemsEqual(valueA, valueB);

            Assert.IsTrue(result);
        }

        [Test]
        public void Dictionary_KO()
        {
            var valueA = new Dictionary<string, int> { { "1", 1 }, { "2", 2 }, { "3", 3 }, { "4", 4 } };
            var valueB = new Dictionary<string, int> { { "1", 1 }, { "2", 2 }, { "3", 3 } };

            var result = Kindred.AreItemsEqual(valueA, valueB);

            Assert.IsTrue(!result);
        }

        [Test]
        public void ClassSimple_OK()
        {
            var valueA = new Person { Name = "Luca", LastName = "Fenu", Age = 36 };
            var valueB = new Person { Name = "Luca", LastName = "Fenu", Age = 36 };

            var result = Kindred.AreItemsEqual(valueA, valueB);

            Assert.IsTrue(result);
        }

        [Test]
        public void ClassSimple_KO()
        {
            var valueA = new Person { Name = "Luca", LastName = "Fenu", Age = 36 };
            var valueB = new Person { Name = "Luca", LastName = "Fenu", Age = 30 };

            var result = Kindred.AreItemsEqual(valueA, valueB);

            Assert.IsTrue(!result);
        }

        [Test]
        public void ClassAnonymous_OK()
        {
            var valueA = new { Nome = "Luca", Cognome = "Fenu", Eta = 36 };
            var valueB = new { Nome = "Luca", Cognome = "Fenu", Eta = 36 };

            var result = Kindred.AreItemsEqual(valueA, valueB);

            Assert.IsTrue(result);
        }

        [Test]
        public void ClassAnonymous_KO()
        {
            var valueA = new { Nome = "Luca", Cognome = "Fenu", Eta = 36 };
            var valueB = new { Nome = "Luca", Cognome = "Fenu", Eta = 30 };

            var result = Kindred.AreItemsEqual(valueA, valueB);

            Assert.IsTrue(!result);
        }

        [Test]
        public void ClassWithListOfObjects_OK()
        {
            var valueA = new PersonWithChildern
            {
                Name = "Luca",
                LastName = "Fenu",
                Age = 36,
                Sons = new List<Person> {
                    new Person{ Name="FiglioNome1", LastName= "FiglioCognome1"},
                    new Person{ Name="FiglioNome2", LastName= "FiglioCognome2"}
                }
            };
            var valueB = new PersonWithChildern
            {
                Name = "Luca",
                LastName = "Fenu",
                Age = 36,
                Sons = new List<Person> {
                    new Person{ Name="FiglioNome1", LastName= "FiglioCognome1"},
                    new Person{ Name="FiglioNome2", LastName= "FiglioCognome2"}
                }
            };

            var result = Kindred.AreItemsEqual(valueA, valueB);

            Assert.IsTrue(result);
        }

        [Test]
        public void ClassWithListOfObjects_KO()
        {
            var valueA = new PersonWithChildern
            {
                Name = "Luca",
                LastName = "Fenu",
                Age = 36,
                Sons = new List<Person> {
                    new Person{ Name="FiglioNome1", LastName= "FiglioCognome1"},
                    new Person{ Name="FiglioNome2", LastName= "FiglioCognome2"}
                }
            };
            var valueB = new PersonWithChildern
            {
                Name = "Luca",
                LastName = "Fenu",
                Age = 36,
                Sons = new List<Person> {
                    new Person{ Name="FiglioNome1", LastName= "FiglioCognome1"},
                    new Person{ Name="FiglioNome2", LastName= "FiglioCognome1"}
                }
            };

            var result = Kindred.AreItemsEqual(valueA, valueB);

            Assert.IsTrue(!result);
        }

        public class Person
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
        }

        public class PersonWithChildern
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }

            public List<Person> Sons { get; set; }
        }
    }
}