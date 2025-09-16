public class ProductEuroDto
{
    public string Name { get; set; }

    // Price in British Pounds
    public decimal PriceInPounds { get; set; }

    // Price converted to Euros
    public decimal PriceInEuros { get; set; }
}
