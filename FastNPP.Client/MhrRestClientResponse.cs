using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastNPP.Client
{
  public class MhrRestClientResponse
  {
    public System.Net.HttpStatusCode HttpStatus { get; set; }
    public string Message { get; set; }
    public string Severity { get; set; }
    public string Code { get; set; }
    public string Content { get; set; }
  }
}
