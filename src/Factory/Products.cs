namespace Factory
{
    internal abstract class Product
    {
    }

    internal class InsuranceProduct : Product
    {
    }

    internal class GhasedakProduct : Product
    {
    }

    internal class ArvanCloudProduct : Product
    {
    }

    internal class AbaciProduct : Product
    {
    }

    internal class SejelProduct : Product
    {
    }

    internal enum ProductCategory
    {
        Insurance,
        Messenger,
        IaaS,
        CreditCalculation,
        Security
    }
}