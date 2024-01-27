namespace Bank.Domain.DTO
{
    public class CreateNewCustomerInput
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string GivenName { get; set; }
        public string SurName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public DateTime? Birthday { get; set; }
        public string? TelephoneCountryCode { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string Frequency { get; set; }
        public string TypeOWNERorDISPONENT { get; set; }

    }
}
