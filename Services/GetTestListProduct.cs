using MDP3.Tables;

namespace MDP3.Services
{
    public class GetTestListProduct:IGetTestListProduct
    {
        public List<Product> GetTestListProducts()
        {
            var list = new List<Product>
            {
                new() {
                    Name = "Pepsi",
                    Producer = new Producer()
                    {
                        Title = "Coce"
                    },
                    ProducerId = 0
                }
            };

            return list;
        }
    }
}
