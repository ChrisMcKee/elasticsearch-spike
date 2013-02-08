namespace ElasticSearchDemo
{
	using System;
	using System.Diagnostics;

	public class Program
	{
		private const int NumberOfTestRecords = 1000;

		private static void ConsoleNextHelper(bool delete =false)
		{
			Console.WriteLine("Any Key To Continue");
			Console.ReadKey();
			if(delete)
				ElasticSearchStuff.RemoveEmployeesIndex();
			Console.Clear();
		}

		private static void Main(string[] args)
		{
			Console.WriteLine("1 - Index employees without explicitly creating index");
			var stopwatch = Stopwatch.StartNew();
			ElasticSearchStuff.SetUpElasticSearch();
			ElasticSearchStuff.SeedTheIndexWithNewEmployees(200);
			stopwatch.Stop(); Console.WriteLine("- Setup + Seed took {0} Milliseconds", stopwatch.Elapsed.TotalMilliseconds); stopwatch.Reset();
			stopwatch.Start();
			ElasticSearchStuff.CountTotalEmployees();
			ElasticSearchStuff.FilterEmployeesByCompanyId();
			stopwatch.Stop(); Console.WriteLine("- Count + Filter took {0} Milliseconds", stopwatch.Elapsed.TotalMilliseconds); stopwatch.Reset();
			ConsoleNextHelper(delete:true);


			Console.WriteLine("Explicitly Creating a new index and seeding with 1000 records Please Wait...");
			ElasticSearchStuff.SetUpElasticSearch();
			ElasticSearchStuff.CreateEmployeeIndex();
			ElasticSearchStuff.SeedTheIndexWithNewEmployees(NumberOfTestRecords);

			Console.WriteLine("2 - Index employees after explicitly creating index");
			ElasticSearchStuff.CountTotalEmployees();
			ElasticSearchStuff.FilterEmployeesByCompanyId();
			ConsoleNextHelper();

			Console.WriteLine("3 - Filter employees by favorite numbers");
			ElasticSearchStuff.CountTotalEmployees();
			ElasticSearchStuff.FilterEmployeesByFavoriteNumbers(13);
			ConsoleNextHelper();

			Console.WriteLine("4 - Delete employees by favorite numbers");
			ElasticSearchStuff.CountTotalEmployees();
			ElasticSearchStuff.FilterEmployeesByFavoriteNumbers(13);
			ElasticSearchStuff.DeleteEmployeesByQueryFavoriteNumbers(13);
			ElasticSearchStuff.CountTotalEmployees();
			ConsoleNextHelper();

			Console.WriteLine("5 - Search and score employees by description text");
			ElasticSearchStuff.CountTotalEmployees();
			ElasticSearchStuff.SearchEmployeesByDescriptionText(100);
			ConsoleNextHelper();

			Console.WriteLine("6 - Update one employee");
			ElasticSearchStuff.CountTotalEmployees();
			ElasticSearchStuff.UpdateEmployee();
			ElasticSearchStuff.FilterEmployeesByFavoriteNumbers(22);
			ConsoleNextHelper();

			Console.WriteLine("7 - Get employees with facets");
			ElasticSearchStuff.CountTotalEmployees();
			ElasticSearchStuff.GetEmployeesWithFacets();
			ConsoleNextHelper(delete:true);

			Console.WriteLine("Finished");
			Console.ReadKey();
		}

	}
}