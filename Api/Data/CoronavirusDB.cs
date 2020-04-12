
using System;
using Api.Data.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Api.Data
{
    public class CoronavirusDB
    {
        public IMongoDatabase Database { get; set; }
        public CoronavirusDB(IConfiguration configuration) {
            try
            {
                var mongoSettings = MongoClientSettings.FromUrl(new MongoUrl(configuration["connectionString"]));
                var mongoClient = new MongoClient(mongoSettings);

                this.Database = mongoClient.GetDatabase(configuration["NomeBanco"]);
                
                MapearClasses();
            }
            catch(Exception ex)
            {
                throw new MongoException("Não foi possível se conectar com o MongoDB.", ex);
            }
        }

        private void MapearClasses()
        {
            var conventionPack = new ConventionPack() { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);
            // t => true -> aplica globalmente esta convenção
            // t => t.FullName.StartsWith("Your.Name.Space."));

            if (!BsonClassMap.IsClassMapRegistered(typeof(Infectado)))
            {
                BsonClassMap.RegisterClassMap<Infectado>(classMap => 
                {
                    classMap.AutoMap();
                    classMap.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}