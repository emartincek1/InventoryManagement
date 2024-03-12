using InventoryManagement.ProductManagement;

namespace InventoryManagement.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_Contructor_Initalizes_Correctly()
        {
            // Arrange & Act
            Product product = new Product(1, "Sugar", "Lorem ipsum", new Price(10, Currency.Euro), UnitType.PerKg, 100);
            Price price = new Price(10, Currency.Euro);

            // Assert
            Assert.Equal(1, product.Id);
            Assert.Equal("Sugar", product.Name);
            Assert.Equal("Lorem ipsum", product.Description);
            Assert.Equal(price.Currency, product.Price.Currency);
            Assert.Equal(price.ItemPrice, product.Price.ItemPrice);
            Assert.Equal(UnitType.PerKg, product.UnitType);
        }

        [Fact]
        public void ChangeStockThreshold_Changes_Threshold()
        { 
            // Act
            Product.ChangeStockThreshold(25);

            // Assert
            Assert.Equal(25, Product.StockThreshold);
        }

        [Fact]
        public void UseProduct_Reduces_AmountInStock()
        {
            // Arrange
            Product product = new Product(1, "Sugar", "Lorem ipsum", new Price(10, Currency.Euro), UnitType.PerKg, 100);

            product.IncreaseStock(100);

            // Act
            product.UseProduct(20);

            // Assert
            Assert.Equal(80, product.AmountInStock);    
        }

        [Fact]
        public void UseProduct_ItemsHigherThanStock_NoChangetoStock()
        {
            // Arrange
            Product product = new Product(1, "Sugar", "Lorem ipsum", new Price(10, Currency.Euro), UnitType.PerKg, 100);

            product.IncreaseStock(10);

            // Act
            product.UseProduct(100);

            // Assert
            Assert.Equal(10, product.AmountInStock);
        }

        [Fact]
        public void IncreaseStock_Increases_AmountInStock()
        {
            // Arrange
            Product product = new Product(1, "Sugar", "Lorem ipsum", new Price(10, Currency.Euro), UnitType.PerKg, 100);

            // Act
            product.IncreaseStock(10);

            // Assert
            Assert.Equal(10, product.AmountInStock);
        }

        [Fact]
        public void IncreaseStock_Caps_AmountInStock_IncreaseHigherThanMax()
        {
            // Arrange
            Product product = new Product(1, "Sugar", "Lorem ipsum", new Price(10, Currency.Euro), UnitType.PerKg, 100);

            // Act
            product.IncreaseStock(200);

            // Assert
            Assert.Equal(100, product.AmountInStock);
        }

        [Fact]
        public void DecreaseStock_Decreases_AmountInStock()
        {
            // Arrange
            Product product = new Product(1, "Sugar", "Lorem ipsum", new Price(10, Currency.Euro), UnitType.PerKg, 100);
            product.IncreaseStock(20);

            // Act
            product.DecreaseStock(10, "Natural Disaster");

            // Assert
            Assert.Equal(10, product.AmountInStock);
        }

        [Fact]
        public void DisplayDetailsShort_Displays_Correctly()
        {
            // Arrange
            Product product = new Product(1, "Sugar", "Lorem ipsum", new Price(10, Currency.Euro), UnitType.PerKg, 100);

            // Act & Assert
            Assert.Equal("1 Sugar \n0 item(s) in stock", product.DisplayDetailsShort());

        }

        [Fact]
        public void DisplayDetailsFull_Displays_Correctly()
        {
            // Arrange
            Product product = new Product(1, "Sugar", "Lorem ipsum", new Price(10, Currency.Euro), UnitType.PerKg, 100);

            // Act & Assert
            Assert.Equal("1 Sugar \nLorem ipsum\n10 Euro\n0 item(s) in stock\n!!STOCK LOW!!", product.DisplayDetailsFull());

        }

        [Fact]
        public void UpdateLowStock_UpdatesCorrectly_StockBelowThreshold()
        {
            // Arrange
            Product product = new Product(1, "Sugar", "Lorem ipsum", new Price(10, Currency.Euro), UnitType.PerKg, 100);
            product.IncreaseStock(10);

            // Act
            product.UpdateLowStock();

            // Assert
            Assert.True(product.IsBelowStockThreshold);
        }

        [Fact]
        public void UpdateLowStock_UpdatesCorrectly_StockAboveThreshold()
        {
            // Arrange
            Product product = new Product(1, "Sugar", "Lorem ipsum", new Price(10, Currency.Euro), UnitType.PerKg, 100);
            product.IncreaseStock(30);

            // Act
            product.UpdateLowStock();

            // Assert
            Assert.False(product.IsBelowStockThreshold);
        }
    }
}