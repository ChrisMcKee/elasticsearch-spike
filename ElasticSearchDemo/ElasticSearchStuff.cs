namespace ElasticSearchDemo
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Nest;
	using Nest.FactoryDsl;

	public static class ElasticSearchStuff
	{
		public const string IndexName = "employees";
		public const string TypeName = "employee";

		public static IElasticClient ElasticSearchClient;

		public static Guid EmployeeId;
		private static readonly ConnectionSettings ElasticSearchConnectionString = new ConnectionSettings("localhost", 9200);

		public static void GetEmployeesWithFacets()
		{
			var searchBuilder = SearchBuilder.Builder();
			searchBuilder.Filter(FilterFactory.TermFilter("companyId", "bfbd4924-8a72-4312-842d-6ab1215f12b1".ToLower()));
			searchBuilder.Facet(FacetFactory.TermsFacet("favoriteNumbers").Field("favoriteNumbers"));

			var result = ElasticSearchClient.Search(searchBuilder, IndexName, TypeName);
			var facetItems = result.FacetItems<TermItem>("favoriteNumbers");

			foreach (var facetItem in facetItems)
			{
				Log(string.Format("Number of matches for facet {0}: {1}", facetItem.Term, facetItem.Count));
			}
		}

		public static void UpdateEmployee()
		{
			var employee = ElasticSearchClient.Get<Employee>(IndexName, TypeName, EmployeeId.ToString());

			employee.FavoriteNumbers.Add(22);

			ElasticSearchClient.Update(u => u
				                            .Object(employee)
				                            .Script("ctx._source = myobj")
				                            .Params(p => p.Add("myobj", employee))
				                            .Index(IndexName)
				                            .Type(TypeName)
				                            .Id(employee.Id.ToString())
				                            .RetriesOnConflict(5)
				                            .Refresh());

			Log("Updated employee with new favorite number");
		}

		/*
		 * If you have a column full of guids it would make sense to strip the hyphen
		 * as there is a well known issue with elastic search and hyphens
		 * searching for a standard stored guid in whole wont produce any results
		 * searching for part of the guid (before a -) will produce results
		 */
		public static void FilterEmployeesByCompanyId()
		{
			var searchBuilder = SearchBuilder.Builder();
			searchBuilder.Filter(FilterFactory.TermFilter("companyId", "ef6eacbd"));

			var result = ElasticSearchClient.Search(searchBuilder, IndexName);

			Log(string.Format("Employees in Company Id Starting 'ef6eacbd' : {0} (Time (ms): {1})", result.Total,
			                  result.ElapsedMilliseconds));
		}

		public static void SearchEmployeesByDescriptionText(int size)
		{
			var searchBuilder = SearchBuilder.Builder();
			searchBuilder.Query(QueryFactory.TextQuery("description", "overworked")).Size(size);

			var result = ElasticSearchClient.Search(searchBuilder, IndexName, TypeName);

			Console.ForegroundColor = ConsoleColor.Green;
			Log("Employees whose Description is closest to 'overworked':");

			result.Hits.Hits.ForEach(h => Log(string.Format("Score: {0}", h.Score.ToString())));
			Console.ForegroundColor = ConsoleColor.White;
		}

		public static void CountTotalEmployees()
		{
			var searchBuilder = SearchBuilder.Builder().Query(QueryFactory.MatchAllQuery());
			var results = ElasticSearchClient.Search(searchBuilder, IndexName, TypeName);

			Log(string.Format("Total employees: {0}", results.Total));
		}

		public static void FilterEmployeesByFavoriteNumbers(int favoriteNumber)
		{
			var searchBuilder = SearchBuilder.Builder();

			var filter = FilterFactory.TermFilter("favoriteNumbers", favoriteNumber);
			searchBuilder.Filter(filter);

			var results = ElasticSearchClient.Search(searchBuilder, IndexName, TypeName);

			Log(string.Format("Employees whose favorite numbers include {0}: {1}", favoriteNumber, results.Total));
		}

		//Think Tags (numbers are easier to spit out on the fly)
		public static void DeleteEmployeesByQueryFavoriteNumbers(int favoriteNumber)
		{
			ElasticSearchClient.DeleteByQuery<Employee>(
				q => q.Index(IndexName).Type(TypeName).Term(e => e.FavoriteNumbers, favoriteNumber.ToString()));

			Log(string.Format("Deleted employees whose favorite numbers include {0}", favoriteNumber));
		}

		//Generator to create new employees for ES
		public static void SeedTheIndexWithNewEmployees(int numberToGenerate)
		{
			var counter = 0;
			var rnd = new Random();

			for (var i = 0; i < numberToGenerate; i++)
			{
				var description = new StringBuilder();

				for (int j = 0; j < 10; j++)
				{
					int index = rnd.Next(ElasticSearchTestSeed.TestDescriptionSeedTags.GetUpperBound(0));
					description.Append(ElasticSearchTestSeed.TestDescriptionSeedTags[index]);
					if (i > 0) description.Append(" ");
				}

				var employee = new Employee
					               {
						               Id = Guid.NewGuid(),
						               Name =
							               ElasticSearchTestSeed.TestNames[rnd.Next(160)] + " " +
							               ElasticSearchTestSeed.TestLastNames[rnd.Next(160)],
						               DateOfBirth = DateTime.Now.AddDays(-(i - 30)),
						               CompanyId = ElasticSearchTestSeed.CompanyIds[counter],
						               Description = description.ToString(),
						               FavoriteNumbers =
							               new List<int>
								               {
									               rnd.Next(1, 20),
									               rnd.Next(1, 20),
									               rnd.Next(1, 20),
									               rnd.Next(1, 20),
									               rnd.Next(1, 20)
								               }
					               };

				ElasticSearchClient.Index(employee, IndexName, TypeName, employee.Id.ToString(),
				                          new IndexParameters {Refresh = true});


				if (counter == 2)
					counter = 0;
				else
					counter++;

				EmployeeId = employee.Id;
			}

			Log(string.Format("Created {0} employees", numberToGenerate));
		}

		public static void SetUpElasticSearch()
		{
			ElasticSearchClient = new ElasticClient(ElasticSearchConnectionString);
			ConnectionStatus status;
			ElasticSearchClient.TryConnect(out status);

			if (!status.Success)
			{
				Log(string.Format("Connection Status: {0}", status.Result));
				Console.WriteLine("Any Key To Continue");
				Console.ReadLine();
			}
			else
			{
				Log("Connection Status : Connected");
			}
		}

		public static void CreateEmployeeIndex()
		{
			var settings = new IndexSettings {NumberOfReplicas = 1, NumberOfShards = 5};

			var result = ElasticSearchClient.CreateIndex(IndexName, settings);
			if (!result.OK)
			{
				Log("Unable to create and configure employees ElasticSearch index");
				return;
			}
			Log("Employees index created");
		}

		public static void RemoveEmployeesIndex()
		{
			try
			{
				ElasticSearchClient.DeleteIndex(IndexName);
				Log("Deleted employees index", true);
			}
			catch
			{
				// for this crappy app we can ignore this; I'm basically being lazy
			}
		}

		public static void Log(string message, bool delete = false)
		{
			Console.ForegroundColor = delete ? ConsoleColor.Red : ConsoleColor.Yellow;
			Console.WriteLine(string.Concat(DateTime.Now.ToLongTimeString(), " -> ", message));
			Console.ForegroundColor = ConsoleColor.White;
		}
	}
}