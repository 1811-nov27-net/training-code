using MemoryList.Library;
using System;
using System.Collections.Generic;
using Xunit;

namespace MemoryList.Tests
{
    public class MemoryListTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NewListShouldNotHaveContainedAnything(string item)
        {
            var sut = new MemoryList<string>();

            var contained = sut.HasEverContained(item);

            Assert.False(contained);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("abc")]
        public void OneItemAddedShouldHaveBeenContained(string item)
        {
            var sut = new MemoryList<string>();
            sut.Add(item);

            var contained = sut.HasEverContained(item);

            Assert.True(contained);
        }

        [Theory]
        [InlineData(null, "")]
        [InlineData("a", "b", "a")]
        public void SeveralItemsAddRangedShouldHaveBeenContained(params string[] items)
        {
            var sut = new MemoryList<string>();
            sut.AddRange(items);

            foreach (var item in items)
            {
                Assert.True(sut.HasEverContained(item));
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("abc")]
        public void OneItemAddedAndRemovedShouldHaveBeenContained(string item)
        {
            var sut = new MemoryList<string>();
            sut.Add(item);
            sut.Remove(item);

            var contained = sut.HasEverContained(item);

            Assert.True(contained);
        }

        // it's not possible to make these tests pass - this shows the
        // flaw in trying to override with "new". you can still run
        // the parent class's implementation using upcasting.
        [Theory]
        [InlineData(null)]
        [InlineData("abc")]
        public void OneItemAddedAndRemovedWithUpcastingShouldHaveBeenContained(string item)
        {
            var sut = new MemoryList<string>();
            List<string> sutAsList = sut; // implicit upcasting
            sutAsList.Add(item);
            sutAsList.Remove(item);
            // because of using "new" and not "override", that ran List's "add" and "remove",
            // not MemoryList's.

            var contained = sut.HasEverContained(item);

            Assert.True(contained);
        }

        [Theory]
        [InlineData(null, "")]
        [InlineData("a", "b", "a")]
        public void SeveralItemsAddRangedAndRemoveAlledShouldHaveBeenContained(params string[] items)
        {
            var sut = new MemoryList<string>();
            sut.AddRange(items);
            sut.RemoveAll(x => true);

            foreach (var item in items)
            {
                Assert.True(sut.HasEverContained(item));
            }
        }

        [Theory]
        [InlineData(null, "")]
        [InlineData("a", "b", "a")]
        public void SeveralItemsAddRangedAndRemoveRangedShouldHaveBeenContained(params string[] items)
        {
            var sut = new MemoryList<string>();
            sut.AddRange(items);
            sut.RemoveRange(0, items.Length);

            foreach (var item in items)
            {
                Assert.True(sut.HasEverContained(item));
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("abc")]
        public void OneItemAddedAndRemoveAtedShouldHaveBeenContained(string item)
        {
            var sut = new MemoryList<string>();
            sut.Add(item);
            sut.RemoveAt(0);

            var contained = sut.HasEverContained(item);

            Assert.True(contained);
        }

        [Theory]
        [InlineData("a", null, "")]
        [InlineData(null, "", "a")]
        [InlineData("b", "a")]
        public void ListWithSomeItemsAddedAndRemovedShouldNotHaveContainedOthers(string extra, params string[] items)
        {
            var sut = new MemoryList<string>();
            sut.AddRange(items);
            sut.Remove(items[0]);

            Assert.False(sut.HasEverContained(extra));
        }
    }
}
