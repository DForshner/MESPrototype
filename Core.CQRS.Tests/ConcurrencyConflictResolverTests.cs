using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.CQRS.Tests
{
    [TestClass]
    public class ConcurrencyConflictResolverTests
    {
        private class EventA : Event { }
        private class EventB : Event { }
        private class EventC : Event { }

        [TestMethod]
        public void WhenNoConflictRegistered_ExpectNoConflict()
        {
            var sut = new ConcurrencyConflictResolver();
            sut.RegisterConflict(typeof(EventA), new Type[] { typeof(EventB) });

            var result = sut.ConflictsWith(typeof(EventA), new Type[] { typeof(EventC), typeof(EventC) });

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenConflictRegistered_ExpectConflict()
        {
            var sut = new ConcurrencyConflictResolver();
            sut.RegisterConflict(typeof(EventA), new Type[] { typeof(EventB) });

            var result = sut.ConflictsWith(typeof(EventA), new Type[] { typeof(EventC), typeof(EventB) });

            Assert.IsTrue(result);
        }
    }
}
