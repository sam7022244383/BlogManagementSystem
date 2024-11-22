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
    public class AzureTable : IAzureTable
    {
        private readonly TableClient _tableClient;
        private readonly IMapper _mapper;
        private const String partionKey = "Employe";
        public AzureTable(TableServiceClient tableServiceClient, IMapper mapper)
        {
            _tableClient = tableServiceClient.GetTableClient("Employee");
            _mapper = mapper;
        }

        public async Task<List<AzTbale>> GetAll()
        {
            var EmpResult = new List<EmployeRespoance>();
            var employeelist = _tableClient.QueryAsync<AzTbale>(filter: "");
            await foreach (var employee in employeelist)
            {
                EmpResult.Add(new EmployeRespoance
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                });
            }
            return _mapper.Map<List<Employe>>(EmpResult);
        }

        public async Task<AzTbale> GetById(string id)
        {
            var empEntity = await _tableClient.GetEntityAsync<Employe>(partionKey, id);
            return empEntity;
        }
        public async Task<bool> Add(AzTbale emp)
        {
            var EmpEntity = new Employe
            {
                RowKey = Guid.NewGuid().ToString(),
                Name = emp.Name,
                Email = emp.Email,
                PartitionKey = partionKey

            };
            var respoance = await _tableClient.AddEntityAsync<Employe>(EmpEntity);
            return true;

        }

        public async Task<bool> Update(AzTbale emp, string id)
        {
            var empEntity = await _tableClient.GetEntityAsync<Employe>(partionKey, id);
            if (empEntity != null)
            {
                empEntity.Value.Name = emp.Name;
                empEntity.Value.Email = emp.Email;
            }
            await _tableClient.UpdateEntityAsync<Employe>(empEntity, empEntity.Value.ETag);
            return true;

        }

        public async Task<bool> DeleteById(string id)
        {
            var empEntity = await _tableClient.GetEntityAsync<Employe>(partionKey, id);
            if (empEntity != null)
            {
                await _tableClient.DeleteEntityAsync(empEntity.Value.PartitionKey, empEntity.Value.RowKey);
            }
            return true;

        }

    }
}
