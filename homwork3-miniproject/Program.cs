


using System;
using System.Collections;
using System.Diagnostics;

ProductList productList = new ProductList();


while (true)
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("To enter a new Asset - enter: e | To print sortedList  - enter: p | To finish and close - enter: exit");
    string input = Console.ReadLine().ToLower();

    switch (input.ToLower())
    {

        // to add Asset values in method from productlist class
        case "e":
            productList.AddProduct();
            break;

        // to print list of all pruduct 
        case "p":
           /* Console.WriteLine("=============== list sorted by Type ======================================="); 
            Console.WriteLine("                                                                            ");
            productList.Print();*/
            Console.WriteLine("============== List sorted by office and then by Purchase Date =============");
            Console.WriteLine("                                                                            ");
            productList.PrintByDate();
         
            break;

            // to close the program 
        case "exit":
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("                 The End                 ");
            Console.WriteLine("----------------------------------------------");
            return;

        // Error message in case wrong input
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid choice. Please enter a valid option.");
            break;
    }
}


// delcare Asset class
class Asset
{
    public string Type { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal Price { get; set; }
    public string Office { get; set; }
    public string Currency;
    public decimal LocalPrice;
    
    // constractor for Asset class
    public Asset(string type, string brand, string model, string office, DateTime purchaseDate, decimal price, string currency, decimal localPrice)
    {
        Type = type;
        Brand = brand;
        Model = model;
        PurchaseDate = purchaseDate;
        Price = price;
        Office = office;
        Currency = currency;
        LocalPrice = localPrice;
    }

    
}


// declare Product class to add and print the list
class ProductList
{
    public List<Asset> AssetList = new List<Asset>();


    // Method to Add properties to object Asset and add to the list
    public void AddProduct()
    {

        // input values from a user
        Console.Write(" Enter a Type Phone | Computer: ");
        string type = Console.ReadLine();
        if (type != "phone" && type != "computer")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You entered an invalid type");
            return; // Exit the method if the type is invalid
        }

        Console.Write(" Enter a Model: ");
        string brand = Console.ReadLine();
        Console.Write(" Enter a Brand: ");
        string model = Console.ReadLine();
        Console.Write(" Enter a Office: USA | Spain | Sweden:   "); // input choice from three office
        string office = Console.ReadLine().ToUpper();
        if (office != "USA" && office != "SPAIN" && office != "SWEDEN")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You entered an invalid type");
            return; // Exit the method if the office is invalid
        }
        Console.Write(" Enter a Purchase Date (yyyy-MM-dd): ");

        
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime purchaseDate))
        {
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine(" Invalid Date format");// Exit the method if the date is invalid
            return;
        }
        Console.Write(" Price in dollars: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Invalid Price "); // Exit the method if the price is invalid 
            return;
        }


        GetPrice convertPrice = new GetPrice(price,office);  // create object from class GEtPrice

        string currency = convertPrice.GetCurrency();    //Get value of Currency from the method 
        decimal localPrice= convertPrice.PriceInDollar(); // Get value of Local price 

      // add values in the list inside object Asset
        AssetList.Add(new Asset(type, brand, model, office,purchaseDate, price, currency, localPrice));
    }

    public virtual void Print()
    {

        // Sort the list by Type and print
        List<Asset> SortedProducts = AssetList.OrderBy(Asset => Asset.Type).ToList();
        
       Console.WriteLine("Type".PadRight(15) + "Brand".PadRight(15) + "Model".PadRight(15) + "Office".PadRight(15) + "PurchaseDate".PadRight(15) + "Price".PadRight(15) + "Currency".PadRight(15) + "LocalPrice".PadRight(15) );
        Console.WriteLine("--------".PadRight(15) + "------".PadRight(15) + "-----".PadRight(15) + "------".PadRight(15) + "-------------".PadRight(15) + "-------".PadRight(15) + "---------".PadRight(15) + "----------".PadRight(15));

        foreach (Asset ass in SortedProducts)
        {
            Console.WriteLine(ass.Type.PadRight(15)  + ass.Brand.PadRight(15) + ass.Model.PadRight(15) + ass.Office.PadRight(15) + ass.PurchaseDate.ToString("yyyy-MM-dd").PadRight(15) + ass.Price.ToString().PadRight(15) + ass.Currency.PadRight(15) + ass.LocalPrice.ToString().PadRight(15) );
             
        }
    }

    public void PrintByDate()
    {

        // Sort the list by Office first and then by Purchase Date
        List<Asset> SortedProducts1 = AssetList.OrderBy(asset => asset.Office).ThenBy(asset => asset.PurchaseDate).ToList();
        Console.WriteLine("Type".PadRight(15) + "Brand".PadRight(15) + "Model".PadRight(15)  +"Office".PadRight(15) + "PurchaseDate".PadRight(15) + "Price".PadRight(15)  + "Currency".PadRight(15) + "LocalPrice".PadRight(15) );
        Console.WriteLine("--------".PadRight(15) + "------".PadRight(15) + "-----".PadRight(15) + "------".PadRight(15) + "-------------".PadRight(15) + "-------".PadRight(15) + "---------".PadRight(15) + "----------".PadRight(15));

        foreach (Asset ass in SortedProducts1)
        {
            DateTime today = DateTime.Today;

            DateTime oldDate = DateTime.Now.AddYears(-3);
            
            TimeSpan difference = today - ass.PurchaseDate;     // calculate the difference of how many days between today and the input Purchase date 
            if (ass.PurchaseDate > oldDate)
            {
                int differ = (int)difference.TotalDays;
                if (differ <= 90)          // if Purchase date is less than three months => colore Red
                {
                    
                    Console.ForegroundColor = ConsoleColor.Red;

                }
                else if (differ >= 90 && differ <= 180)    // if Purchase Date more than three months and less than 6 months => colore Yellow
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;      // other date with wihte color
                   
                }
            }
            Console.WriteLine(ass.Type.PadRight(15) + ass.Brand.PadRight(15) + ass.Model.PadRight(15) + ass.Office.PadRight(15) + ass.PurchaseDate.ToString("yyyy-MM-dd").PadRight(15) + ass.Price.ToString().PadRight(15) + ass.Currency.PadRight(15) + ass.LocalPrice.ToString().PadRight(15) );
            Console.ForegroundColor = ConsoleColor.White;

        }
    }

    }



// declare a class GetPrice to convert the pric and currency according to country
class GetPrice
{
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public string Office { get; set; }
    public decimal LocalPrice { get; set; }


    public GetPrice(decimal price, string office)
    {
        Price = price;
        Office = office;
    }

    public decimal PriceInDollar()   // method to convert input values in dollars and convert them to each currency according to office
    {
        decimal usdToEurRate = 0.92m;
        decimal usdToSekRate = 10.59m;

        switch (Office.ToUpper())
        {
            case "USA":
                return LocalPrice = Price;

            case "SPAIN":
                return LocalPrice = Price * usdToEurRate;

            case "SWEDEN":
                return LocalPrice = Price * usdToSekRate;

            default:
                Console.WriteLine("Invalid Office");
                return 0;
        }
    }



    // Method to return currency according to office
    public string GetCurrency()
    {
        switch (Office.ToUpper())
        {
            case "USA":
                return Currency = "US";


            case "SPAIN":
                return Currency = "EUR";

            case "SWEDEN":
                return Currency = "SEK";

            default:

                return Currency = "US";
        }
    }
}

