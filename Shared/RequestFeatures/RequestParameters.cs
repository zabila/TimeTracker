namespace Shared.RequestFeatures;

public abstract class RequestParameters {
    private const int MaxPageSize = 50;
    private int _pageSize = 10;

    public string OrderBy { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize {
        get {
            return _pageSize;
        }
        set {
            _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }

}