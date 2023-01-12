using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerModels
{
    public class Country
    {
        public int Id { get; set; }

        public required string CountryName { get; set; }
    }
}
