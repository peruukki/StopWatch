using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace StopWatch
{
  public class LRUList
  {
    private readonly StringCollection mItems;
    public StringCollection Items { get { return mItems; } }

    private readonly int mCapacity;
    public int Capacity { get { return mCapacity; } }

    public LRUList(int capacity)
      : this(capacity, new StringCollection())
    {
    }

    public LRUList(int capacity, StringCollection initialValues)
    {
      mItems = initialValues;
      mCapacity = capacity;
      RemoveItemsExceedingCapacity();
    }

    public void Add(string item)
    {
      mItems.Insert(0, item);
      RemoveItemsExceedingCapacity();
    }

    private void RemoveItemsExceedingCapacity()
    {
      while (mItems.Count > mCapacity)
      {
        mItems.RemoveAt(mItems.Count - 1);
      }
    }
  }
}
