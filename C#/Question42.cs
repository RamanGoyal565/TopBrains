using System;
using System.Collections.Generic;
public class Program
{
	public static SortedDictionary<int, Bike> bikeDetals=new SortedDictionary<int, Bike>();
	public static void Main()
	{
		int choice = 0;
		BikeUtility obj = new BikeUtility();
		do
		{
			Console.WriteLine("1. Add Bike Details");
 			Console.WriteLine("2. Group Bikes By Brand");
			Console.WriteLine("3. Exit");
			Console.WriteLine("Enter your choice");
			choice=Convert.ToInt32(Console.ReadLine());	
			switch (choice)
			{
				case 1:
					Console.Write("Enter the model : ");
					string model = Console.ReadLine();
					Console.Write("Enter the brand : ");
					string brand = Console.ReadLine();
					Console.Write("Enter the price per day : ");
					int price = Convert.ToInt32(Console.ReadLine());
					obj.AddBikeDetails(model, brand, price);
					Console.WriteLine("Bike details added successfully");
					break;
				case 2:
					SortedDictionary<string, List<Bike>> result = obj.GroupBikesByBrand();
					foreach (var item in result.Keys)
					{
						Console.Write(item + " ");
						foreach (var bike in result[item])
						{
							Console.Write(bike.Model + " ");
						}
						Console.WriteLine();
					}
					break;
				case 3:
					break;
				default:
					Console.WriteLine("Wrong Choice");
					break;
			}
		}
		while (choice != 3);
	}
}
public class Bike
{
	public string Model { get; set; }
	public int PricePerDay { get; set; }
	public string Brand { get; set; }
}
public class BikeUtility 
{ 
	public void AddBikeDetails(string model,string brand,int pricePerDay)
	{
		Bike bike = new Bike
		{
			Model = model,
			PricePerDay = pricePerDay,
			Brand = brand,
		};
		int key=Program.bikeDetals.Count+1;
		Program.bikeDetals.Add(key, bike);
	}
    public SortedDictionary<string, List<Bike>> GroupBikesByBrand()
	{
		SortedDictionary<string,List<Bike>> dict=new SortedDictionary<string, List<Bike>>();
		foreach (var entry in Program.bikeDetals.Values)
		{
			if (!dict.ContainsKey(entry.Brand))
				dict[entry.Brand] = new List<Bike>();
			dict[entry.Brand].Add(entry);
		}
		return dict;
	}
}
