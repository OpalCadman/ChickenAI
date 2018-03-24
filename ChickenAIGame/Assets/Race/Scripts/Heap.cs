using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T> where T : Iheap_Item<T>
{
    T[] items;
    int cur_ItemCount;

    public Heap(int maxSize)
    {
        items = new T[maxSize];
    }

    public void Add(T item)
    {
        item.Index_Heap = cur_ItemCount;
        items[cur_ItemCount] = item;
        Sorting_Up(item);
        cur_ItemCount++;
    }

    public T Remove_First_Heap()
    {
        T first_Item = items[0];
        cur_ItemCount--;
        items[0] = items[cur_ItemCount];
        items[0].Index_Heap = 0;
        Sorting_Down(items[0]);
        return first_Item;
    }

    public void Item_Update(T item)
    {
        Sorting_Up(item);
    }

    public int Heap_Count
    {
        get {
            return cur_ItemCount;
        }
    }

    public bool contain(T item)
    {
        return Equals(items[item.Index_Heap], item);
    }

    void Sorting_Down(T item)
    {
        while (true)
        {
            int Index_Child_L = item.Index_Heap * 2 + 1;
            int Index_Child_R = item.Index_Heap * 2 + 2;
            int Index_Swap;

            if (Index_Child_L < cur_ItemCount)
            {
                Index_Swap = Index_Child_L;

                if (Index_Child_R < cur_ItemCount)
                {
                    if (items[Index_Child_L].CompareTo(items[Index_Child_R]) < 0)
                    {
                        Index_Swap = Index_Child_R;
                    }
                }

                if (item.CompareTo(items[Index_Swap]) < 0)
                {
                    Swapping(item, items[Index_Swap]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    void Sorting_Up(T item)
    {
        int Index_Parent = (item.Index_Heap - 1) / 2;

        while (true)
        {
            T Item_Parent = items[Index_Parent];
            if (item.CompareTo(Item_Parent) > 0)
            {
                Swapping(item, Item_Parent);
            }
            else
            {
                break;
            }

            Index_Parent = (item.Index_Heap - 1) / 2; 
        }
    }

    void Swapping(T item_A, T item_B)
    {
        items[item_A.Index_Heap] = item_B;
        items[item_B.Index_Heap] = item_A;
        int Index_ItemA = item_A.Index_Heap;
        item_A.Index_Heap = item_B.Index_Heap;
        item_B.Index_Heap = Index_ItemA;
    }

}

public interface Iheap_Item<T> : IComparable<T>
{
    int Index_Heap
    {
        get;
        set;
    }
}