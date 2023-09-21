using Newtonsoft.Json;
using System;

namespace Pagos
{
    public class PaymentWebhook
    {
        public string id { get; set; }
        public string number { get; set; }
        public string date_operation { get; set; }
        public int transaction_id { get; set; }
        public int order_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string status { get; set; }
    }
    //OPENPAY
    public class PaymentMethod
    {
        public string? type { get; set; }
        public string? url { get; set; }
    }

    public class Root
    {
        public string? type { get; set; }
        public DateTime? event_date { get; set; }
        public Transaction Transaction { get; set; }
        public string? verification_code { get; set; }

        public string? id { get; set; }

    }

    public class Transaction
    {
        public string? id { get; set; }
        public string? authorization { get; set; }
        public string? operation_type { get; set; }
        public string? Transaction_type { get; set; }
        public string? status { get; set; }
        public bool? conciliated { get; set; }
        public DateTime? creation_date { get; set; }
        public DateTime? operation_date { get; set; }
        public string? description { get; set; }
        public object? error_message { get; set; }
        public string? order_id { get; set; }
        public Card Card { get; set; }
        public double? amount { get; set; }
        public Customer customer { get; set; }
        public Fee fee { get; set; }
        public PaymentMethod payment_method { get; set; }
        public Metadata metadata { get; set; }
        public string? currency { get; set; }
        public string? method { get; set; }
    }
    public class Card
    {
        public string? type { get; set; }
        public string? brand { get; set; }
        public object? address { get; set; }
        public string? Card_number { get; set; }
        public string? holder_name { get; set; }
        public string? expiration_year { get; set; }
        public string? expiration_month { get; set; }
        public bool? allows_charges { get; set; }
        public bool? allows_payouts { get; set; }
        public string? bank_name { get; set; }
        public string? points_type { get; set; }
        public bool? points_Card { get; set; }
        public string? bank_code { get; set; }
    }

    public class Customer
    {
        public string? name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? phone_number { get; set; }
        public object? address { get; set; }
        public DateTime? creation_date { get; set; }
        public object? external_id { get; set; }
        public object? clabe { get; set; }
    }

    public class Fee
    {
        public double? amount { get; set; }
        public double? tax { get; set; }
        public string? currency { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("Aceptación de politica de privacidad de  comprador")]
        public string? Aceptacindepoliticadeprivacidaddecomprador { get; set; }

        [JsonProperty("Aceptación término y condiciones de comprador")]
        public string? Aceptacintrminoycondicionesdecomprador { get; set; }
    }
}

