using System.Net;

namespace E_cart.Models
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? StatusMessage { get; set; }
        public string? Error { get; set; }
        public bool ? Success { get; set; }
        public object Result { get; set; }
        
       
        
      
    }
}
