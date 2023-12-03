using System.ComponentModel.DataAnnotations;
namespace WaldeningApi.Models;

public class Park
{
  public int ParkId{ get; set; }
  [Required]
  public string State{ get; set; }
  [Required]
  public string Name { get; set; }
  [Required]
  public string Website { get; set; }
  [Required]
  public bool IsNational { get; set; }
  [Required]
  public List<string> Activities { get; set; }
  public Park()
    {
        Activities = new List<string>();
    }

}