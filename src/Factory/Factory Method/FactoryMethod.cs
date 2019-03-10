using System;

namespace Factory.Factory_Method
{
    internal abstract class ProductFactory
    {
        public abstract Product CreateProduct();
    }

    internal class InsuranceFactory : ProductFactory
    {
        public override Product CreateProduct() => new InsuranceProduct();
    }

    internal class MessengerFactory : ProductFactory
    {
        public override Product CreateProduct() => new GhasedakProduct();

    }

    internal class IaaSFactory : ProductFactory
    {
        public override Product CreateProduct() => new ArvanCloudProduct();

    }

    internal class CreditCalculationFactory : ProductFactory
    {
        public override Product CreateProduct() => new AbaciProduct();

    }

    internal class SecurityFactory : ProductFactory
    {
        public override Product CreateProduct() => new SejelProduct();

    }
}