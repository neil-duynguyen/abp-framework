using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore
{
    public class OtpRecord : Entity<Guid>
    {
        public string Email { get; set; }
        public string OtpCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsUsed { get; set; }
    }

}
