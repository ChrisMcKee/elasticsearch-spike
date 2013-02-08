namespace ElasticSearchDemo
{
	using System;
	using System.Collections.Generic;
	using Nest;

	public class Employee
	{
		[ElasticProperty(Index = FieldIndexOption.not_analyzed)]
		public Guid Id { get; set; }

		[ElasticProperty(Index = FieldIndexOption.not_analyzed)]
		public Guid CompanyId { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }

		[ElasticProperty(Type = FieldType.date_type, Index = FieldIndexOption.analyzed, AddSortField = true)]
		public DateTime DateOfBirth { get; set; }

		public List<int> FavoriteNumbers { get; set; }
	}
}