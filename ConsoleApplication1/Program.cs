using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Shard;
using Raven.Client.Linq;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			var shards = new Dictionary<string, IDocumentStore>
			{
				{"asia", new DocumentStore{Url = "http://localhost:8080", DefaultDatabase = "Asia"}},
				{"africa", new DocumentStore{Url = "http://localhost:8080", DefaultDatabase = "Africa"}},
				{"europe", new DocumentStore{Url = "http://localhost:8080", DefaultDatabase = "Europe"}},
			};
			var shardStrategy = new ShardStrategy(shards)
				.ShardingOn<Company>(x=>x.City, city =>
				{
					switch (city)
					{
						case "Tokyo":
							return "asia";
						case "Stockholm":
							return "europe";
						default:
							return city;
					}
				})
				.ShardingOn<Invoice>(x=>x.CompanyId);


			using(var store = new ShardedDocumentStore(shardStrategy).Initialize())
			{
				CreateData(store);
				//using (var session = store.OpenSession())
				//{
				//    Console.WriteLine(session.Query<Invoice>()
				//        .Where(x => x.CompanyId == "one/companies/1")
				//        .Count());

				//}
			}
		}

		private static void CreateData(IDocumentStore store)
		{
			using (var session = store.OpenSession())
			{
				var company1 = new Company
				{
					Name = "first",
					City = "Tokyo"
				};
				session.Store(company1);
				var company2 = new Company
				{
					Name = "second",
					City = "Stockholm"
				};
				session.Store(company2);

				session.Store(new Invoice
				{
					Amount = 5,
					CompanyId = company1.Id
				});
				session.Store(new Invoice
				{
					Amount = 4,
					CompanyId = company1.Id
				});
				session.Store(new Invoice
				{
					Amount = 25,
					CompanyId = company2.Id
				});
				session.Store(new Invoice
				{
					Amount = 15,
					CompanyId = company2.Id
				});
				session.SaveChanges();
			}
		}
	}

	public class Company
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string City { get; set; }
	}
	public class Invoice
	{
		public string Id { get; set; }
		public decimal Amount { get; set; }
		public string CompanyId { get; set; }
	}

	public class BiasedShardResolutionStrategy : DefaultShardResolutionStrategy
	{
		public BiasedShardResolutionStrategy(IEnumerable<string> shardIds, ShardStrategy shardStrategy) : base(shardIds, shardStrategy)
		{
		}

		public override string GenerateShardIdFor(object entity)
		{
			if ((DateTime.Today - new DateTime(2012, 5, 22)).TotalDays < 14)
				return "tri";
			return base.GenerateShardIdFor(entity);
		}
	}
}
