using System;

namespace Factory.Simple_Factory
{
    internal class SimpleFactory
    {
        public Product CreateProduct(ProductCategory productCategory)
        {
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

            return product;
        }
    }
}