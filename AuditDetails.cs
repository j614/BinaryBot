using System;
using System.Collections.Generic;
using System.Text;

namespace TestBinaryBot
{
    class AuditDetails
    {
        public List<ContractEnd> contract_end { get; set; }
        public List<ContractStart> contract_start { get; set; }
    }
}
