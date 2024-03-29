﻿namespace Shared.RequestFeatures;

public class PagedList<T> : List<T> {
    public MetaData MetaData { get; set; }

    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize) {
        MetaData = new MetaData {
            TotalCount = count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };

        AddRange(items);
    }

    public static PagedList<T> ToPageList(IEnumerable<T> source, int pageNumber, int pageSize) {
        var enumerable = source.ToList();
        var count = enumerable.Count;
        var items = enumerable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}