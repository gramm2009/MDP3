using MDP3.AppDbContext;
using MDP3.RequestModels;
using MDP3.ResponseModels;
using MDP3.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MDP3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BdProviderController(
        ILogger<BdProviderController> logger,
        ApplicationDbContext context,
        IGetTestListProduct testService) : ControllerBase
    {

        private readonly ILogger<BdProviderController> _logger = logger;
        private readonly ApplicationDbContext _context = context;
        private readonly IGetTestListProduct _testService = testService;


        [HttpGet("GetListFromTestService")]
        public List<Product> Get()
        {
            return _testService.GetTestListProducts();
        }


        [HttpPost("AddProduct")]
        public async Task<Product> AddProduct(AddProductRequest request)
        {

            Product? product = await _context.Products
                .FirstOrDefaultAsync(p => p.Name == request.ProductName);

            Producer? producer = await _context.Producers
                .FirstOrDefaultAsync(p => p.Title == request.ProducerName);



            if (product == null)
            {


                if (producer == null)
                {
                    producer = new Producer() { Title = request.ProducerName };
                    await _context.Producers.AddAsync(producer);
                    await _context.SaveChangesAsync();
                }

                product = new Product()
                {
                    Name = request.ProductName,
                    ProducerId = producer.Id,

                };

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

            }
            else
            {
                product = await _context.Products
                     .FirstOrDefaultAsync(p => p.Name == request.ProducerName);
            }

            return product!;
        }

        [HttpGet("GetAllProduct")]
        public async Task<List<Product>> GetAllProduct()
        {

            List<Product> products = await _context.Products
                .AsNoTracking()
                .Include(p => p.Producer)
                .ToListAsync();

            return products;
        }


        [HttpPost("AddProducer")]
        public async Task<Producer> AddProducer(AddProducerRequest request)
        {

            Producer producer = new() { Title = request.Title };

            bool isProducerEnabled = await _context.Producers.AnyAsync(p => p.Title == request.Title);

            if (!isProducerEnabled)
            {
                await _context.Producers.AddAsync(producer);
                await _context.SaveChangesAsync();
            }


            return producer;
        }

        [HttpGet("GetAllProducer")]
        public IEnumerable<Producer> GetAllProducer()
        {
            List<Producer> producers = [.. _context.Producers.AsNoTracking()];

            return producers;
        }


        [HttpGet("GroupByProducer")]
        //public IQueryable<ProducerGroupBy> GroupByProducer()
        public List<IGrouping<int, Product>> GroupByProducer()
        {
            var group = _context.Products
                .AsNoTracking()
                .GroupBy(p => p.Producer!.Id)
                .ToList();
            //.Select(g => new ProducerGroupBy
            //{
            //    ProducerTitle = g.Key,
            //    Count = g.Count()
            //});

            return group;
        }


        [HttpGet("JoitById")]
        public List<JoinById> JoitById()
        {
            var group = _context.Products
                .Join(_context.Producers,
                product => product.Id,
                producer => producer.Id,
                (product, producer) => new JoinById()
                {
                    ProductName = product.Name,
                    Producer = producer.Title,
                    ProducerId = producer.Id,
                }).ToList();

            return group;
        }
    }

}