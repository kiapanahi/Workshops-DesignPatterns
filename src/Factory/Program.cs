using System;
using System.Linq;
using Factory.Factory_Method;
using Factory.Simple_Factory;

namespace Factory
{
    internal class Program
    {
        private static void Main()
        {
            var productCategory = ShowMenu();

            #region Naive implementation

            Product product;
            switch (productCategory)
            {
                case ProductCategory.Insurance:
                    product = new InsuranceProduct();
                    break;
                case ProductCategory.Messenger:
                    product = new GhasedakProduct();
                    break;
                case ProductCategory.IaaS:
                    product = new ArvanCloudProduct();
                    break;
                case ProductCategory.CreditCalculation:
                    product = new AbaciProduct();
                    break;
                case ProductCategory.Security:
                    product = new SejelProduct();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();

            }
            Report("Naive Method", product);

            #endregion

            #region Using SimpleFactory

            var productViaSimpleFactory = new SimpleFactory().CreateProduct(productCategory);
            Report("Using SimpleFactory", productViaSimpleFactory);

            #endregion

            #region Using factory method

            Product productViaFactoryMethod;
            switch (productCategory)
            {
                case ProductCategory.Insurance:
                    productViaFactoryMethod = new InsuranceStore().CreateProduct();
                    break;
                case ProductCategory.Messenger:
                    productViaFactoryMethod = new MessengerStore().CreateProduct();
                    break;
                case ProductCategory.IaaS:
                    productViaFactoryMethod = new IaaSStore().CreateProduct();
                    break;
                case ProductCategory.CreditCalculation:
                    productViaFactoryMethod = new CreditCalculationStore().CreateProduct();
                    break;
                case ProductCategory.Security:
                    productViaFactoryMethod = new SecurityStore().CreateProduct();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Report("Using FactoryMethod", productViaFactoryMethod);

            #endregion


            CreateAndReportProduct(productCategory);
        }

        private static void Report(string issuer, Product product)
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"\t{issuer}");
            Console.WriteLine($"\tproduct: {product.GetType().Name}");
            Console.WriteLine(new string('=', 50));

        }

        private static void CreateAndReportProduct(ProductCategory productCategory)
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("\tCreating product somewhere in application");

            Product product;
            switch (productCategory)
            {
                case ProductCategory.Insurance:
                    product = new InsuranceProduct();
                    break;
                case ProductCategory.Messenger:
                    product = new GhasedakProduct();
                    break;
                case ProductCategory.IaaS:
                    product = new ArvanCloudProduct();
                    break;
                case ProductCategory.CreditCalculation:
                    product = new AbaciProduct();
                    break;
                case ProductCategory.Security:
                    product = new SejelProduct();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            Console.WriteLine($"\tproduct: {product.GetType().Name}");
            Console.WriteLine(new string('=', 50));
        }

        private static ProductCategory ShowMenu()
        {
            Console.WriteLine("Select product category:");
            var l = Enum.GetNames(typeof(ProductCategory))
                .Select((s, i) => (Code: i + 1, Name: s))
                .ToList();
            foreach (var (code, name) in l)
            {
                Console.WriteLine($"{code}. {name}");
            }

            Console.WriteLine();
            Console.Write("> ");
            var input = Console.ReadLine();
            Console.WriteLine();

            var index = int.Parse(input) - 1;

            var n = l[index].Name;

            var category = Enum.Parse<ProductCategory>(n, true);

            return category;
        }
    }
}