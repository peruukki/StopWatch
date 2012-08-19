using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using StopWatch;

namespace StopWatchNUnitTester
{
  [TestFixture]
  public class LRUListTester
  {
    [SetUp]
    public void Init()
    {
    }

    [TearDown]
    public void Uninit()
    {
    }

    [Test]
    public void AddLessItemsThanCapacity()
    {
      AddItems(new LRUList(5), new string[] { "Item 1", "Item 2", "Item 3" });
    }

    [Test]
    public void AddMoreItemsThanCapacity()
    {
      AddItems(new LRUList(2), new string[] { "Item 1", "Item 2", "Item 3" });
    }

    private void AddItems(LRUList lruList, string[] items)
    {
      foreach (string item in items)
      {
        lruList.Add(item);
      }
      int itemCount = (lruList.Capacity < items.Length) ? lruList.Capacity
                                                        : items.Length;
      Assert.That(lruList.Items.Count, Is.EqualTo(itemCount));

      int i = items.Length - 1;
      foreach (string item in lruList.Items)
      {
        Assert.That(item, Is.EqualTo(items[i]));
        i--;
      }
    }
  }
}
