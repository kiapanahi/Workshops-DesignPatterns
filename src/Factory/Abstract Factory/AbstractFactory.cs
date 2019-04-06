using System.Dynamic;
using Factory.Factory_Method;

namespace Factory.Abstract_Factory
{
    internal abstract class AbstractProductFactory
    {
        private readonly ProductFactory _factory;

        protected AbstractProductFactory(ProductFactory factory)
        {
            _factory = factory;
        }

        public Product CreateProductInstance()
        {
            var p = _factory.CreateProduct();
            return p;
        }

    }

    internal class AbstractProductFactory2
    {
        private readonly ProductFactory _factory;

        public AbstractProductFactory2(ProductFactory factory)
        {
            _factory = factory;
        }

        public Product CreateProductInstance()
        {
            var p = _factory.CreateProduct();
            return p;
        }

    }

    internal class InsuranceAbstractProductFactory : AbstractProductFactory
    {
        public InsuranceAbstractProductFactory() : base(new InsuranceFactory()) { }
    }
    internal class MessengerAbstractProductFactory : AbstractProductFactory
    {
        public MessengerAbstractProductFactory() : base(new MessengerFactory()) { }
    }
    internal class IaaSAbstractProductFactory : AbstractProductFactory
    {
        public IaaSAbstractProductFactory() : base(new IaaSFactory()) { }
    }
    internal class CreditCalculationAbstractProductFactory : AbstractProductFactory
    {
        public CreditCalculationAbstractProductFactory() : base(new CreditCalculationFactory()) { }
    }
    internal class SecurityAbstractProductFactory : AbstractProductFactory
    {
        public SecurityAbstractProductFactory() : base(new SecurityFactory()) { }
    }
}