using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListHandler
{
    public static int IndexRandomizer(int length)
    {
        return Random.Range(0, length);
    }

    public static List<T> ListRemoval<T>(List<T> list, int removalIndex)
    {
        list.RemoveAt(removalIndex);
        return list;
    }
}
