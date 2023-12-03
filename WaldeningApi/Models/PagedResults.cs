namespace WaldeningApi.Models;
public class PagedResults<T>
{
  public int PageNumber { get; set; }
  public int PageSize{ get; set; }
  public int NumberOfPages{ get; set; }
  public int NumberOfResults{ get; set; }
  public string NextPageUrl{ get; set; }
  public IEnumerable<T> Results{ get; set; }
}