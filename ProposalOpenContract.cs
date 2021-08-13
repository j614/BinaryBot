using System;
using System.Collections.Generic;
using System.Text;

namespace TestBinaryBot
{
    class ProposalOpenContract
    {
        public string account_id { get; set; }
        public AuditDetails audit_details { get; set; }
        public string barrier { get; set; }
        public int barrier_count { get; set; }
        public int bid_price { get; set; }
        public double buy_price { get; set; }
        public long contract_id { get; set; }
        public string contract_type { get; set; }
        public string currency { get; set; }
        public double current_spot { get; set; }
        public string current_spot_display_value { get; set; }
        public int current_spot_time { get; set; }
        public int date_expiry { get; set; }
        public int date_settlement { get; set; }
        public int date_start { get; set; }
        public string display_name { get; set; }
        public double entry_spot { get; set; }
        public string entry_spot_display_value { get; set; }
        public double entry_tick { get; set; }
        public string entry_tick_display_value { get; set; }
        public int entry_tick_time { get; set; }
        public double exit_tick { get; set; }
        public string exit_tick_display_value { get; set; }
        public int exit_tick_time { get; set; }
        public int expiry_time { get; set; }
        public int is_expired { get; set; }
        public int is_forward_starting { get; set; }
        public int is_intraday { get; set; }
        public int is_path_dependent { get; set; }
        public int is_settleable { get; set; }
        public int is_sold { get; set; }
        public int is_valid_to_cancel { get; set; }
        public int is_valid_to_sell { get; set; }
        public string longcode { get; set; }
        public int payout { get; set; }
        public double profit { get; set; }
        public double profit_percentage { get; set; }
        public int purchase_time { get; set; }
        public int sell_price { get; set; }
        public double sell_spot { get; set; }
        public string sell_spot_display_value { get; set; }
        public int sell_spot_time { get; set; }
        public int sell_time { get; set; }
        public string shortcode { get; set; }
        public string status { get; set; }
        public TransactionIds transaction_ids { get; set; }
        public string underlying { get; set; }
        public string validation_error { get; set; }
    }
}
