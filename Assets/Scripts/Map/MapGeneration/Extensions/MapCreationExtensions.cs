﻿using System.Collections.Generic;
using System.Linq;

public static class MapCreationExtensions
{
    public static T[,] CornerSmoothPass<T>(this T[,] map, SquareVertex corner)
    {
        IEnumerable<int> rangeX = Enumerable.Range(0, map.GetLength(0) - 1);
        IEnumerable<int> rangeY = Enumerable.Range(0, map.GetLength(1) - 1);

        if (corner == SquareVertex.TopRight || corner == SquareVertex.BottomRight)
        {
            rangeX = rangeX.Reverse();
        }

        if (corner == SquareVertex.BottomLeft || corner == SquareVertex.BottomRight)
        {
            rangeY = rangeY.Reverse();
        }

        foreach (int x in rangeX)
        {
            foreach (int y in rangeY)
            {
                map.SmoothCoordinate(x, y);
            }
        }

        return map;
    }

    public static T[,] RandomFill<T>(this T[,] map, T value, int randomFillPercent)
    {
        return RandomFill(map, value, randomFillPercent, 0);
    }

    public static T[,] RandomFill<T>(this T[,] map, T value, int randomFillPercent, int seed)
    {
        return map.RandomFillSection(0, map.GetLength(0) - 1, 0, map.GetLength(1) - 1, value, randomFillPercent, seed);
    }

    public static T[,] RandomFillSection<T>(this T[,] map, int startX, int endX, int startY, int endY, T value, int randomFillPercent)
    {
        return RandomFillSection(map, startX, endX, startY, endY, value, randomFillPercent, 0);
    }

    public static T[,] RandomFillSection<T>(this T[,] map, int startX, int endX, int startY, int endY, T value, int randomFillPercent, int seed)
    {
        if (startX < 0 || startY < 0 || endX < startX || endY < startY)
            return null;

        System.Random random = new System.Random(seed);

        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                if (random.Next(0, 100) < randomFillPercent)
                {
                    map[x, y] = value;
                }
            }
        }

        return map;
    }

    public static T[,] RandomSmoothPass<T>(this T[,] map)
    {
        return RandomSmoothPass(map, 0);
    }

    public static T[,] RandomSmoothPass<T>(this T[,] map, int seed)
    {
        int[] rangeX = Enumerable.Range(0, map.GetLength(0) - 1).ToArray().Shuffle(seed);
        int[] rangeY = Enumerable.Range(0, map.GetLength(1) - 1).ToArray().Shuffle(seed);

        foreach (int x in rangeX)
        {
            foreach (int y in rangeY)
            {
                map.SmoothCoordinate(x, y);
            }
        }

        return map;
    }

    public static T[,] SetBorder<T>(this T[,] map, T value, int borderThickness)
    {
        int sizeX = map.GetLength(0);
        int sizeY = map.GetLength(1);

        if (borderThickness > 0 && borderThickness * 2 < sizeX && borderThickness * 2 < sizeY)
        {
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    if (x < borderThickness || x >= sizeX - borderThickness || y < borderThickness || y >= sizeY - borderThickness)
                    {
                        map[x, y] = value;
                    }
                }
            }
        }

        return map;
    }

    public static T[,] SmoothCoordinate<T>(this T[,] map, int x, int y)
    {
        int sizeX = map.GetLength(0);
        int sizeY = map.GetLength(1);
        T positiveValue = map[x, y];
        T negativeValue = map.GetNeighbors(x, y).Where(a => !a.Equals(map[x, y])).GroupBy(a => a).OrderByDescending(a => a.Count()).Select(a => a.Key).FirstOrDefault();
        int positiveValueCount = map.GetNeighbors(x, y).Count(a => a.Equals(positiveValue));

        if (x == 0 || x == sizeX - 1 || y == 0 || y == sizeY - 1 || positiveValueCount > 4)
        {
            map[x, y] = positiveValue;
        }
        else if (positiveValueCount < 4)
        {
            map[x, y] = negativeValue;
        }

        return map;
    }
}