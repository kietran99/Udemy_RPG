﻿using System;
using System.Collections.Generic;

public static class CollectionExtension
{
    public static (T item, int idx) LookUp<T>(this T[] arr, Predicate<T> conditions)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (conditions(arr[i]))
            {
                return (arr[i], i);
            }
        }

        return (default(T), Constants.INVALID);
    }

    public static (T item, int idx) LookUp<T>(this T[] arr, Func<T, int, bool> conditions)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (conditions(arr[i], i))
            {
                return (arr[i], i);
            }
        }

        return (default(T), Constants.INVALID);
    }

    public static T[] Filter<T>(this T[] iter, Predicate<T> conditions)
    {
        var result = new List<T>();

        for (int i = 0; i < iter.Length; i++)
        {
            if (conditions(iter[i]))
            {
                result.Add(iter[i]);
            }
        }

        return result.ToArray();
    }

    public static T Reduce<T>(this T[] iter, Func<T, T, T> function)
    {
        if (iter.Length == 0) return default(T);

        T currentVal = iter[0];

        for (int i = 0; i < iter.Length; i++)
        {
            currentVal = function(currentVal, iter[i]);
        }

        return currentVal;
    }

    public static T Reduce<T, U>(this U[] iter, Func<T, U, T> function, T startValue)
    {
        T currentVal = startValue;

        for (int i = 0; i < iter.Length; i++)
        {
            currentVal = function(currentVal, iter[i]);
        }

        return currentVal;
    }

    public static void Map<T>(this T[] iter, Action<T> function)
    {
        for (int i = 0; i < iter.Length; i++)
        {
            function(iter[i]);
        }
    }

    public static void Map<T>(this T[] iter, Action<T, int> function)
    {
        for (int i = 0; i < iter.Length; i++)
        {
            function(iter[i], i);
        }
    }

    public static T2[] Map<T1, T2>(this T1[] iter, Func<T1, T2> function)
    {
        var result = new List<T2>();

        for (int i = 0; i < iter.Length; i++)
        {
            result.Add(function(iter[i]));
        }

        return result.ToArray();
    }
}
