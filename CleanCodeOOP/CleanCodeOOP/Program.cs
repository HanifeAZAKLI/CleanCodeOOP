using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CleanCodeOOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IBankService bankService = new CentralBankServiceAdapter();
            IProductService productService = new ProductManager(bankService);
            productService.Sell(new Product() { Id = 1, Name = "Laptop", Price = 1000 },
                new Customer() { Id = 1, Name = "Hanife", IsOfficer = true });
        }
    }

       class Customer : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsStudent { get; set; }
        public bool IsOfficer { get; set; }
    }
    interface IEntity
    {

    }
    class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    class ProductManager : IProductService
    {
        private IBankService _bankService;

        public ProductManager(IBankService bankService)
        {
            _bankService = bankService;
        }
        public void Sell(Product product, Customer customer)
        {
            decimal price = product.Price;
            if (customer.IsStudent)
            {
                price = product.Price * 0.90m;
            }

            else if (customer.IsOfficer)
            {
                price = product.Price * 0.80m;
            }
            
            price = _bankService.ConvertRate(new CurrencyRate { Price = price });
            Console.WriteLine($"İndirim ve kur dönüşümü sonrası fiyat: {price}");
            Console.ReadLine();
        }
    }

    internal interface IProductService
    {
        void Sell(Product product, Customer customer);
    }

    class FakeBankService : IBankService
    {
        public decimal ConvertRate(CurrencyRate currencyRate)
        {
            return currencyRate.Price / 30.60m;

        }
    }

    internal interface IBankService
    {
        decimal ConvertRate(CurrencyRate currencyRate);
    }
    class CurrencyRate
    {
        public decimal Price { get; set; }
        public int Currency { get; set; }
    }

    class CentralBankServiceAdapter : IBankService
    {
        public decimal ConvertRate(CurrencyRate currencyRate)
        {
            CentralBankService centralBankService = new CentralBankService();
            return centralBankService.ConvertCurrecy(currencyRate);
        }
    }



    //Merkez bankasının kodu gibi düşün.
    class CentralBankService
    {
        public decimal ConvertCurrecy(CurrencyRate currencyRate)
        {
            return currencyRate.Price / 30.53m;
        }


        //İŞ bankasının kodu gibi düşün.
        class IsBankService
        {
            public decimal ConvertCurrecy(CurrencyRate currencyRate)
            {
                return currencyRate.Price / 30.70m;
            }
        }

        class IsBankServiceAdapter : IBankService
        {
            public decimal ConvertRate(CurrencyRate currencyRate)
            {
                IsBankService centralBankService = new IsBankService();
                return centralBankService.ConvertCurrecy(currencyRate);
            }
        }
    }


}  
