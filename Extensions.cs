using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

public static class Extensions {

    public static void Shuffle<T>(this IList<T> list) {
        for (var i = 0; i < list.Count; i++)
            list.Swap(i, Random.Range(i, list.Count));
    }

    public static void Swap<T>(this IList<T> list, int i, int j) {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

    public static string SplitCamelCase(this string str) {
        return Regex.Replace(
            Regex.Replace(
                str,
                @"(\P{Ll})(\P{Ll}\p{Ll})",
                "$1 $2"
            ),
            @"(\p{Ll})(\P{Ll})",
            "$1 $2"
        );
    }

    public static int RandomIntExcluding(int min, int max, int excluding) {
        if (excluding < min || excluding >= max)
            return Random.Range(min, max);
        int number = Random.Range(min, max - 1);
        if (number >= excluding)
            number++;
        return number;
    }

    public static void TestRandomIntExcluding(int min, int max, int excluding, int tests = 10000) {
        Dictionary<int, int> numbers = new Dictionary<int, int>();
        for (int i = 0; i < tests; i++) {
            int number = RandomIntExcluding(min, max, excluding);
            if (!numbers.ContainsKey(number))
                numbers.Add(number, 0);
            numbers[number]++;
        }

        string str = "";
        List<int> numbersSorted = numbers.Keys.OrderBy(n => n).ToList();
        foreach (int number in numbersSorted)
            str += "[" + number + "]: " + numbers[number] + "\t";
        Debug.Log(str);
    }

}
