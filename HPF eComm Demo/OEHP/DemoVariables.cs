using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HostPayFunctions
{
    public static class DemoVariables
    {
        public static string AccountToken = "DF62A010CE6050093389EBFA8553A761C3C92A9141B5C4FCE8A9482684DD5FEA9C51BD5D08B32D10CC";
        //testhpf.apphb.com/oehp/success.aspx
        //localhost
        public static string CustomKeyPairs = "&bill_customer_title_visible=false&bill_customer_title_edit=false&bill_first_name_visible=false&bill_first_name_edit=false&bill_middle_name_visible=false&bill_middle_name_edita=false&bill_last_name_visible=false&bill_last_name_edit=false&bill_company_visible=false&bill_company_edit=false&bill_address_one_visible=false&bill_address_one_edit=false&bill_address_two_visible=false&bill_address_two_edit=false&bill_city_visible=false&bill_city_edit=false&bill_state_or_province_visible=false&bill_state_or_province_edit=false&bill_country_code_visible=false&bill_country_code_edit=false&bill_postal_code_visible=false&bill_postal_code_edit=false&invoice_number_edit=false&purchase_order_number_edit=false&credit_card_verification_number_editable=false&order_information_visible=false&card_information_visible=false&customer_information_visible=false&expiry_date_label=Expiration+Date&background-color=#FFFFFF;&charge_type_row_visible=false&order_information_visible=false&return_url=http://testhpf.apphb.com/oehp/success.aspx&return_target=_self&btn-background-color=gray;";
        public static string serviceInvoiceNumber { get; set; }
    }
}