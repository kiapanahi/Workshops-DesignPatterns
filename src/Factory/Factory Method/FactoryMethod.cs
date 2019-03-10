using System;

namespace Factory.Factory_Method
{
    internal abstract class ProductStore
    {
        public abstract Product CreateProduct();
    }

    internal class InsuranceStore : ProductStore
    {
        public override Product CreateProduct() => new InsuranceProduct();
    }

    internal class MessengerStore : ProductStore
    {
        public override Product CreateProduct() => new GhasedakProduct();

    }

    internal class IaaSStore : ProductStore
    {
        public override Product CreateProduct() => new ArvanCloudProduct();

    }

    internal class CreditCalculationStore : ProductStore
    {
        public override Product CreateProduct() => new AbaciProduct();

    }

    internal class SecurityStore : ProductStore
    {
        public override Product CreateProduct() => new SejelProduct();

    }
}