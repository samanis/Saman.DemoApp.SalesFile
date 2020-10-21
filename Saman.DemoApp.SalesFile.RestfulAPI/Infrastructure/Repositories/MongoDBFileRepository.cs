using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure.Interfsaces;
using Saman.DemoApp.SalesFile.RestfulAPI.Model;
using System;
using System.Collections.Generic;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure.Repositories
{
    public class MongoDBFileRepository : IFileRepository<string>
    {
        string _connectionString;
        MongoClient _dbClient;
        IConfiguration _configuration;
        IConfigurationSection _mongoDbConfigsection;
        IMongoDatabase _mongoDatabase;
        IMongoCollection<CSVSalesFile> _mongoCollection;

        public MongoDBFileRepository(string connectionString, IConfiguration configuration)
        {
            try
            {
                _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
                _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
                _dbClient = new MongoClient(_connectionString);
                _mongoDbConfigsection = _configuration.GetSection("MongoDB");
                _mongoDatabase = _dbClient.GetDatabase(_mongoDbConfigsection.GetSection("Database").Value);
                _mongoCollection = _mongoDatabase.GetCollection<CSVSalesFile>(_mongoDbConfigsection.GetSection("Collection").Value);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Could not create MongoDB repository", ex);
            }
        }

        public CSVSalesFile GetById(string id)
        {
            try
            {
                var filter_id = Builders<CSVSalesFile>.Filter.Eq("_id", id);
                CSVSalesFile salesFile = _mongoCollection.Find(filter_id).FirstOrDefault();
                return salesFile;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not find MongoDB document", ex);

            }
        }

        public async void InsertFileContent(CSVSalesFile salesFile)
        {
            try
            {
                await _mongoCollection.InsertOneAsync(salesFile);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not save MongoDB document", ex);
            }
        }
    }
}
