using System.ComponentModel.DataAnnotations;

namespace DataAPI.Models
{
    public class Currency
    {
        [Key]
        public String Code { get; set; }
        public String Name{ get; set; }
    }
}
