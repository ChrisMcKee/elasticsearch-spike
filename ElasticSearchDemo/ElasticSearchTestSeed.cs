namespace ElasticSearchDemo
{
	using System;

	public static class ElasticSearchTestSeed
	{
		public static readonly string[] TestNames = new[]
			                                            {
				                                            "Ken", "Tina", "margaret", "Tracy", "Kelly", "Margaret", "Amy", "Amanda"
				                                            , "Jason", "Danielle", "Elizabeth", "Marc", "Hayley", "Colin", "Helen",
				                                            "Catherine", "Catherine", "Dawn", "Antonia", "Vicky", "Jacqueline",
				                                            "Doug", "Jane", "Annabelle", "Katy Lousie", "Esther", "Maureen", "Fiona"
				                                            , "Kate", "Eleanor", "Stephen", "Mark", "Philip", "Marianne", "Belinda",
				                                            "Michelle", "Paul", "Alexandra", "NULL", "Jessamy", "Jeanette",
				                                            "mererid", "Maggie", "Richard", "Philip", "Katherine", "Peter", "",
				                                            "Ruth", "Sue", "James", "Margaret", "John", "Penelope M.L.", "Juno",
				                                            "David", "Deborah", "Ann", "Gautam", "Anne", "Carolyn", "Marion",
				                                            "Janine", "Mick", "Kirsty", "Jonny", "Claire", "June", "Jeremy", "Celia"
				                                            , "Susan", "Fiona", "Anne", "Mark & Diane", "Trisha", "Sara", "Sophie ",
				                                            "Vernon", "Timothy", "Graham", "Moira", "Neil", "Nigel", "Asadour",
				                                            "Loraine", "Guy", "Julie", "Jill", "Robert", "Adrian", "Jim", "Sharon",
				                                            "Anne", "Marguerite", "Stephen", "Alex", "Elizabeth", "Maggie", "Yuen",
				                                            "Deborah", "Ann", "Patricia", "Kath", "Angela", "Andrea", "Clarissa",
				                                            "Sarah", "Kirsteen", "Oliver", "Katherine", "Dawn", "Robert", "Sally",
				                                            "Patricia", "Lorraine", "Mary", "David", "Adam", "Alessandro", "Judith",
				                                            "Chris", "Anthony", "Craig", "Eve", "Andrew", "Sarah", "Judith",
				                                            "Middlemiss", "Stephen", "Stephen", "Jonathan", "Mary", "Lisa", "Conrad"
				                                            , "Kirstie", "Kathryn", "Donald", "Alison", "Lisa-Dionne", "Tone", "Rob"
				                                            , "Joy", "Louise", "Hannah", "Anna", "Lynda", "Jane", "Abigail",
				                                            "Samantha", "Jason", "Allia", "Anita", "Janis", "Tim", "Jane", "Helen",
				                                            "Nick", "Sharon", "Samson", "Jane", "Anthony"
			                                            };

		public static readonly string[] TestLastNames = new[]
			                                                {
				                                                "Fackrell", "Ball", "field", "Hetherington", "Gardner", "Brampton",
				                                                "Owen", "Bond", "Haines", "Treanor", "McDermott", "Fabri", "Harding"
				                                                , "Hughes", "Breach", "Coultas", "Mason", "McPherson", "Palmer",
				                                                "Miles", "Mackenzie", "Sandle", "Aston", "Mottram", "Wimbush",
				                                                "Rolinson", "Stephenson", "Zobole", "Staines", "MacFarlane",
				                                                "Charnock", "Flannigan", "Hardaker", "Cook", "Holden", "Cotton",
				                                                "Swales", "Olding", "NULL", "Kelly", "Thomas", "Velios", "Danquah",
				                                                "Ellis", "Woodhouse", "Harvey", "Stiles", "Pascale", "Lyne", "Fridd"
				                                                , "Horrobin", "Bonsey", "Angus", "Hooper", "Doran", "Noonan",
				                                                "Hutchinson", "Rapstoff", "Narang", "Postlethwaite", "Burnett",
				                                                "Dunn", "Parrish", "Thacker", "Hall", "Muirhead", "Looney",
				                                                "Emerson", "Hunt", "Moss", "Royce", "Venables", "Brown", "Clements",
				                                                "Woodcock", "Trentham", "Smith", "Williams", "Giles", "Roberts",
				                                                "Vincentelli", "Lamont", "Walsh", "Guzelian", "Leeson", "Eades",
				                                                "Seddon Jones", "Kelly", "Martin", "Friedli", "McQueen", "Tate",
				                                                "McNeill", "Nugent", "Beddoe", "Hodby", "Hawley", "Bolt", "Fong",
				                                                "Dean", "Sproat", "Singh", "Libbert", "Horn", "Hawkins", "Corfe",
				                                                "Brown", "Macdonald", "Buckley", "Macleod", "Henderby", "Livingston"
				                                                , "Shaw", "Fleming", "Grant", "Horlock", "Wright", "Sutherland",
				                                                "Vincentelli", "King", "Coppock", "Shapland", "Wood", "Ropek",
				                                                "Patrizio", "Lowndes", "Winter", "&", "Turner", "Palmer", "Harris",
				                                                "Dale", "Bowers", "Atkinson", "Skinner", "Bone", "Hawthorn", "Bell",
				                                                "Morris", "von", "van Meeuwen", "Bosworth", "Short", "Fuller",
				                                                "Wilkinson", "Cash", "Frazer", "Brown", "Twomey", "Mulligan", "Ali",
				                                                "Klein", "Goodman", "Andrews", "Timshle", "Gilbart", "James",
				                                                "Haward", "Kambalu", "Needham", "Elliott"
			                                                };


		public static readonly string[] TestDescriptionSeedTags = new[]
			                                                   {
				                                                   "Alcoholic", "Overworked", "Underpaid", "Driver", "Admin", "Pale"
			                                                   };

		public static readonly Guid[] CompanyIds = new[]
			                                           {
				                                           Guid.Parse("ef6eacbd-1a8d-4847-a6b4-b6ea38b02d0c"),
				                                           Guid.Parse("bfbd4924-8a72-4312-842d-6ab1215f12b1"),
				                                           Guid.Parse("ad195d5c-bc91-4afb-bd09-6aaa9940197e")
			                                           };
	}
}