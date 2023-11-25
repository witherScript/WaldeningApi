namespace WaldeningApi.Models;

public class Park
{
  public int ParkId{ get; set; }
  public string State{ get; set; }
  public string Name { get; set; }
  public string Website { get; set; }
  public bool IsNational { get; set; }
  public List<string> Activities { get; set; }
  public Park()
    {
        Activities = new List<string>();
    }

}