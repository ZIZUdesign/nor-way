using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ITPE3200_1_20H_nor_way.Models
{
    [ExcludeFromCodeCoverage]
    public class Admin
    {
            public int Id { get; set; }
            public string Brukernavn { get; set; }
            public byte[] Passord { get; set; }
            public byte[] Salt { get; set; }
        
    }
}
