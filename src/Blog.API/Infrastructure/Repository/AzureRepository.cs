using Application.Interface;
using Application.Response;
using AutoMapper;
using Azure.Data.Tables;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzTbale = Domain.Entities.Employe;
namespace Infrastructure.Repository
{
    public class AzureRepository<T> : IAzureRepository<T> where T : class
    {
        private readonly TableClient _tableClient;
        private readonly IMapper _mapper;
        private const String partionKey = "Employe";
        public AzureRepository(TableServiceClient tableServiceClient, IMapper mapper)
        {
            _tableClient = tableServiceClient.GetTableClient("Employee");
            _mapper = mapper;
        }

        public async Task<List<T>> GetAll()
        {
            var EmpResult = new List<T>();
            var employeelist = _tableClient.QueryAsync<AzTbale>(filter: "");
            await foreach (var employee in employeelist)
            {
                var mappedResult = _mapper.Map<T>(employee);
                EmpResult.Add(mappedResult);
            }
            return EmpResult;
        }

        public async Task<T> GetById(string id)
        {
            var empEntity = await _tableClient.GetEntityAsync<AzTbale>(partionKey, id);
            var result = _mapper.Map<T>(empEntity.Value);
            return result;
        }

        public async Task<bool> Add(T emp)
        {
            var EmpEntity = new Employe
            {
                RowKey = Guid.NewGuid().ToString(),
                PartitionKey = partionKey

            };
            var respoance = await _tableClient.AddEntityAsync<AzTbale>(EmpEntity);
            return true;
        }

        public async Task<bool> Update(T emp, string id)
        {
            return true;
        }

        public Task<bool> DeleteById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
