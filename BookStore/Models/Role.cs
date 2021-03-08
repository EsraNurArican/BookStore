using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    /// <summary>
    /// Represents user role such as admin,editor etc
    /// </summary>
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
