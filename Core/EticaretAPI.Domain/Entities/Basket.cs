using EticaretAPI.Domain.Entities.Common;
using EticaretAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Domain.Entities
{
    public class Basket:BaseEntity
    {
       public AppUser User { get; set; }
       public  string UserId { get; set; }
       public  ICollection<BasketItem> BasketItems { get; set; }
    }
}
