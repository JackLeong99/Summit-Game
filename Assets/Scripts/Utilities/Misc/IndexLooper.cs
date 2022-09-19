using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IndexLooper
{
    public static int Increment(int index, int count)
    {
        switch (true)
        {
            case bool x when index == count - 1:
                return 0;
            default:
                return index + 1;
        }
    }

    public static int Decrement(int index, int count)
    {
        switch (true)
        {
            case bool x when index == 0:
                return count - 1;
            default:
                return index - 1;
        }
    }
}