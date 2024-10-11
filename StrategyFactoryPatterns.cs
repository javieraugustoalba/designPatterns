using System;

namespace StrategyWithFactoryPattern
{
    // Definition:
    // The Strategy pattern allows you to define a family of algorithms, encapsulate each one, 
    // and make them interchangeable. It lets the algorithm vary independently from clients that use it.
    // In this example, we define different shipping cost strategies.
    // The Factory pattern is used to create objects without exposing the creation logic to the client.
    // Here, a factory is used to create different strategy instances based on a given input.

    // Strategy interface that defines a common behavior for calculating shipping costs.
    public interface IShippingStrategy
    {
        decimal CalculateShippingCost(decimal weight);
    }

    // Concrete strategy class for ground shipping.
    public class GroundShipping : IShippingStrategy
    {
        public decimal CalculateShippingCost(decimal weight) => weight * 1.5m; // Example calculation.
    }

    // Concrete strategy class for air shipping.
    public class AirShipping : IShippingStrategy
    {
        public decimal CalculateShippingCost(decimal weight) => weight * 3.0m; // Example calculation.
    }

    // Context class that uses the strategy.
    public class ShippingService
    {
        private IShippingStrategy _strategy;

        // Constructor that accepts a strategy to use.
        public ShippingService(IShippingStrategy strategy)
        {
            _strategy = strategy;
        }

        // Method to set a new strategy at runtime.
        public void SetStrategy(IShippingStrategy strategy)
        {
            _strategy = strategy;
        }

        // Method to calculate shipping cost using the current strategy.
        public decimal CalculateCost(decimal weight)
        {
            return _strategy.CalculateShippingCost(weight);
        }
    }

    // Summary:
    // - The Strategy pattern allows switching between different behaviors (strategies) at runtime.
    // - Here, the ShippingService class depends on the IShippingStrategy interface.
    // - We can change the strategy (GroundShipping or AirShipping) at runtime using SetStrategy.
    // - This makes the ShippingService class flexible, as it can work with any class that implements IShippingStrategy.

    // Factory class that creates instances of different strategies.
    public static class ShippingStrategyFactory
    {
        // Factory method that returns an appropriate IShippingStrategy instance based on input.
        public static IShippingStrategy CreateStrategy(string type)
        {
            // Use a switch statement to determine which strategy to create.
            return type switch
            {
                "Ground" => new GroundShipping(),
                "Air" => new AirShipping(),
                _ => throw new ArgumentException("Invalid shipping type")
            };
        }
    }

    // Summary:
    // - The Factory pattern encapsulates the object creation logic.
    // - Here, the ShippingStrategyFactory creates instances of IShippingStrategy based on a given string input.
    // - This separates the strategy creation logic from the client, making the client simpler.
    // - The client (in this case, the Main method) does not need to know the details of how the strategies are created.

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Strategy Pattern without Factory:");
            
            // Create a strategy directly and use it with the ShippingService.
            var groundStrategy = new GroundShipping();
            var shippingService = new ShippingService(groundStrategy);
            Console.WriteLine("Ground shipping cost: " + shippingService.CalculateCost(10)); // Outputs cost for ground shipping.

            // Change the strategy at runtime.
            var airStrategy = new AirShipping();
            shippingService.SetStrategy(airStrategy);
            Console.WriteLine("Air shipping cost: " + shippingService.CalculateCost(10)); // Outputs cost for air shipping.

            Console.WriteLine("\nStrategy Pattern with Factory:");

            // Use the factory to create a strategy instead of directly instantiating it.
            var factoryCreatedStrategy = ShippingStrategyFactory.CreateStrategy("Ground");
            var factoryShippingService = new ShippingService(factoryCreatedStrategy);
            Console.WriteLine("Factory-created Ground shipping cost: " + factoryShippingService.CalculateCost(10));

            // Create another strategy using the factory.
            var factoryCreatedAirStrategy = ShippingStrategyFactory.CreateStrategy("Air");
            factoryShippingService.SetStrategy(factoryCreatedAirStrategy);
            Console.WriteLine("Factory-created Air shipping cost: " + factoryShippingService.CalculateCost(10));
        }
    }

    // Final Summary:
    // - Strategy Pattern:
    //   - Encapsulates different algorithms (shipping strategies) in separate classes (GroundShipping, AirShipping).
    //   - Allows the behavior (strategy) to change at runtime, making the context class (ShippingService) more flexible.
    //   - Does not require a factory but can be used with one for better separation of concerns.
    //
    // - Factory Pattern:
    //   - Provides a way to create objects (strategies) without exposing the instantiation logic to the client.
    //   - In this example, ShippingStrategyFactory creates instances of strategies based on input.
    //   - Useful when the creation logic is complex or needs to be separated from the client code.
    //
    // - Combining Strategy and Factory:
    //   - Using the Factory pattern with Strategy is optional and depends on the complexity of object creation.
    //   - It allows you to centralize the creation of strategy objects, making it easier to manage.
    //   - The Strategy pattern focuses on changing behavior dynamically, while the Factory pattern focuses on object creation.
    //   - They can be combined for scenarios where you need to create different strategies based on runtime conditions.
}
