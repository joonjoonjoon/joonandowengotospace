using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace System.Linq
{
    public static class LinqExtension
    {
        public static List<E> ShuffleList<E>(this List<E> inputList)
        {
            var randomList = new List<E>();
            var r = new System.Random();
            while (inputList.Count > 0)
            {
                var randomIndex = r.Next(0, inputList.Count);
                randomList.Add(inputList [randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }

        public static IEnumerable<T> Traverse<T>(this T item, Func<T,T> childSelector)
        {
            var stack = new Stack<T>(new T[]{ item });

            while (stack.Any())
            {
                var next = stack.Pop();
                if (next != null)
                {
                    yield return next;
                    stack.Push(childSelector(next));
                }
            }
        }

        public static IEnumerable<T> Traverse<T>(this T item, Func<T,IEnumerable<T>> childSelector)
        {
            var stack = new Stack<T>(new T[]{ item });

            while (stack.Any())
            {
                var next = stack.Pop();
                //if(next != null)
                //{
                yield return next;
                foreach (var child in childSelector(next))
                {
                    stack.Push(child);
                }
                //}
            }
        }

        public static IEnumerable<T> Traverse<T>(this IEnumerable<T> items, Func<T, IEnumerable<T>> childSelector)
        {
            var stack = new Stack<T>(items);
            while (stack.Any())
            {
                var next = stack.Pop();
                yield return next;
                foreach (var child in childSelector(next))
                    stack.Push(child);
            }
        }

        public static IEnumerable<IEnumerable<T>> Traverse<T>(this IEnumerable<T> items, Func<IEnumerable<T>, T, IEnumerable<T>> childSelector)
        {
            var stack = new Stack<IEnumerable<T>>(new IEnumerable<T>[]{ items });
            while (stack.Any())
            {
                var next = stack.Pop();
                if (next != null)
                {
                    yield return next;

                    foreach (var item in next)
                    {
                        stack.Push(childSelector(next, item));
                    }    
                }
            }
        }

        public static TSource RandomOrDefault<TSource>(this IEnumerable<TSource> source, System.Func<TSource, bool> predicate)
        {
            var result = source.Where(predicate);
            var count = result.Count();
            if (count != 0)
            {
                var index = UnityEngine.Random.Range(0, count);
                return result.ElementAt(index);
            }
            else
            {
                return default(TSource);
            }
        }


        public static TSource RandomOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            var count = source.Count();
            if (count != 0)
            {
                var index = UnityEngine.Random.Range(0, count);
                return source.ElementAt(index);
            }
            else
            {
                return default(TSource);
            }
        }
    }
}
