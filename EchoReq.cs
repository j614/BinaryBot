using System;
using System.Collections.Generic;
using System.Text;

namespace TestBinaryBot
{
    class EchoReq
    {
        public long contract_id { get; set; }
        public int proposal_open_contract { get; set; }
        public int subscribe { get; set; }
    }
}
