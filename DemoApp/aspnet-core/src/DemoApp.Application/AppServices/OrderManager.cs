using DemoApp.AppEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace DemoApp.AppServices
{
    public class OrderManager : DemoAppAppService
    {
        private readonly IRepository<Order, Guid> _orderRepository;

        public OrderManager(IRepository<Order, Guid> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task TestWithDetails(Guid id)
        {
            //Get a IQueryable<T> by including sub collections
            var queryable = await _orderRepository.WithDetailsAsync(x => x.Lines);

            //Apply additional LINQ extension methods
            var query = queryable.Where(x => x.Id == id);

            //Execute the query and get the result
            var order = await AsyncExecuter.FirstOrDefaultAsync(query);
        }
    }
}
