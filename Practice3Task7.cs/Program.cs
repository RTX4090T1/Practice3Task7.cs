using System;
using System.Collections.Generic;
public interface Commands
{
    void AddDel(int value);
    void Cancel(int value);
}
class ProductObject
{
    public List<string> products = new List<string>();
}
public class Product
{
    public string product { get; set; }
    public string manufacturer { get; set; }
    public double price { get; set; }

    public Product(string product, string manufacturer, double price)
    {
        this.product = product;
        this.manufacturer = manufacturer;
        this.price = price < 0 ? 0 : price;
    }
    public override string ToString() => $"{product}, {manufacturer}, {price}";
}

class CommandRealization
{
    public List<Product> productList = new List<Product>();
    public List<Product> actionHistory = new List<Product>();
    public void AddProduct(Product product)
    {
        productList.Add(product);
        actionHistory.Add(product);
    }
    public void DeleteProduct()
    {
        productList.RemoveAt(productList.Count - 1);
    }
    public void CancelLastOperation(int value)
    {
        if (value == 1)
        {
            productList.RemoveAt(productList.Count - 1);
            actionHistory.RemoveAt(actionHistory.Count - 1);
        }
        else
        {
            productList.Add(actionHistory[actionHistory.Count - 1]);
        }
    }
    public void ShowProducts()
    {
        if (productList.Count == 0)
        {
            Console.WriteLine("Basket is empty");
            return;
        }
        foreach (var product in productList)
        {
            Console.WriteLine(string.Join(",", product));
        }
    }
}

class CommandOperator
{
    private CommandRealization commandRealization = new CommandRealization();

    public void ProductOperation(int value)
    {
        if (value == 1)
        {
            Console.WriteLine("Enter product name: ");
            string prodName = Console.ReadLine();
            Console.WriteLine("Enter product manufacturer: ");
            string prodManufact = Console.ReadLine();
            Console.WriteLine("Enter product price: ");
            double prodPrice = Convert.ToDouble(Console.ReadLine());
            Product addDel = new Product(prodName, prodManufact, prodPrice);
            commandRealization.AddProduct(addDel);

        }
        else
        {
            commandRealization.DeleteProduct();
        }
        commandRealization.ShowProducts();
    }
    public void CancelOperation(int value)
    {
        commandRealization.CancelLastOperation(value);
    }
}

class Basket : Commands
{
    CommandOperator commandOperator = new CommandOperator();

    public void AddDel(int value)
    {
        commandOperator.ProductOperation(value);
    }
    public void Cancel(int value)
    {
        commandOperator.CancelOperation(value);
    }
}
class BasketInterface
{
    Basket basket = new Basket();
    int var = 0;
    public void AddDelProduct(int value)
    {
        basket.AddDel(value);
        if (value == 1)
        {
            var = 1;
        }
        else
        {
            var = 2;
        }
    }
    public void CancelLastOperation()
    {
        basket.Cancel(var);
    }
}

namespace practiceA.css
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BasketInterface basket = new BasketInterface();
            int value = 0;
            do
            {
                Console.WriteLine("Enter 1 to add product 2 to delete product 3 to cancel last action program and 0 to finish");
                value = Convert.ToInt32(Console.ReadLine());
                switch (value)
                {
                    case 1:
                        {
                            basket.AddDelProduct(value);
                            break;
                        }
                    case 2:
                        {
                            basket.AddDelProduct(value);
                            break;
                        }
                    case 3:
                        {
                            basket.CancelLastOperation();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Incorrect operation!");
                            break;
                        }
                }

            } while (value != 0);
        }
    }
}