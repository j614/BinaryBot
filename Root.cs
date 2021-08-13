using System;
using System.Collections.Generic;
using System.Text;

namespace TestBinaryBot
{
    class Root
    {
        public EchoReq echo_req { get; set; }
        public string msg_type { get; set; }
        public ProposalOpenContract proposal_open_contract { get; set; }
    }
}
