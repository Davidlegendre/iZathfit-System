using System.Collections.ObjectModel;

namespace Models.DTOS;

public class PaginationModel<T> where T : class
{
    public ObservableCollection<T>? CollectionPaginated { get; set; }
    public int TotalItems { get; set; }
    public int Page { get; set; } = 1;
    public int PageCount { get; set; }
    public int PageSize { get; set; } = 10;

}
