using System;
using System.Linq;
using Factory.Abstract_Factory;
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

            var productViaSimpleFactory = SimpleFactory.CreateProduct(productCategory);
            Report("Using SimpleFactory", productViaSimpleFactory);

            #endregion

            #region Using factory method

            Product productViaFactoryMethod;
            switch (productCategory)
            {
                case ProductCategory.Insurance:
                    productViaFactoryMethod = new InsuranceFactory().CreateProduct();
                    break;
                case ProductCategory.Messenger:
                    productViaFactoryMethod = new MessengerFactory().CreateProduct();
                    break;
                case ProductCategory.IaaS:
                    productViaFactoryMethod = new IaaSFactory().CreateProduct();
                    break;
                case ProductCategory.CreditCalculation:
                    productViaFactoryMethod = new CreditCalculationFactory().CreateProduct();
                    break;
                case ProductCategory.Security:
                    productViaFactoryMethod = new SecurityFactory().CreateProduct();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Report("Using FactoryMethod", productViaFactoryMethod);

            #endregion

            #region Using abstract factory

            var apf2 = new AbstractProductFactory2(new IaaSFactory());
            var p2= apf2.CreateProductInstance();
            Report("Using apf2:", p2);


            var apf = new MessengerAbstractProductFactory();
            var productViaApf = apf.CreateProductInstance();
            Report("Using Abstract Factory", productViaApf);

            #endregion

            //CreateAndReportProduct(productCategory);
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