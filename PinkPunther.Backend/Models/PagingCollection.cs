namespace PinkPunther.Backend.Models;

public class PagingCollection<T>
{
    public int Page { get; init; }
    
    public int PageSize { get; set; }
    
    public long TotalPages { get; init; }

    public bool HasNextPage => Page < TotalPages;
    
    public IReadOnlyList<T>? Items { get; set; }
}