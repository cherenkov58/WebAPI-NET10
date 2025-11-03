using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPIDemo.Models.Repositories
{
	public static class ShirtRepository
	{
		private static List<Shirt> shirts = new List<Shirt>()
		{
			new Shirt { ShirtId = 1, Brand = "My Brand", Color = "Blue", Gender = "Men", Price = 30, Size = 10 },
			new Shirt { ShirtId = 2, Brand = "My Brand", Color = "Black", Gender = "Men", Price = 35, Size = 12 },
			new Shirt { ShirtId = 3, Brand = "Your Brand", Color = "Pink", Gender = "Women", Price = 28, Size = 8 },
			new Shirt { ShirtId = 4, Brand = "Your Brand", Color = "Yellow", Gender = "Women", Price = 30, Size = 9 }
		};

		public static List<Shirt> GetShirts()
		{
			return shirts;
		}

		public static bool ShirtExists(int id)
		{
			return shirts.Any(x => x.ShirtId == id);
		}

		public static Shirt? GetShirtById(int id)
		{
			return shirts.FirstOrDefault(x => x.ShirtId == id);
		}
		public static void AddShirt(Shirt shirt)
		{
			int maxId = shirts.Count > 0 ? shirts.Max(s => s.ShirtId) : 0;
			shirt.ShirtId = maxId++;
			shirts.Add(shirt);
		}

		public static Shirt? GetShirtByProperties(string? brand, string? gender, string? color, int? size)
		{
			return shirts.FirstOrDefault(x =>
				!string.IsNullOrWhiteSpace(brand) &&
				!string.IsNullOrWhiteSpace(x.Brand) &&
				x.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase) &&
				!string.IsNullOrWhiteSpace(gender) &&
				!string.IsNullOrWhiteSpace(x.Gender) &&
				x.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase) &&
				!string.IsNullOrWhiteSpace(color) &&
				!string.IsNullOrWhiteSpace(x.Color) &&
				x.Color.Equals(color, StringComparison.OrdinalIgnoreCase) &&
				size.HasValue &&
				x.Size.HasValue &&
				size.Value == x.Size.Value);
		}

		public static void UpdateShirt(Shirt shirt)
		{
			var existingShirt = shirts.First(x => x.ShirtId == shirt.ShirtId);
			if (existingShirt != null)
			{
				existingShirt.Brand = shirt.Brand;
				existingShirt.Color = shirt.Color;
				existingShirt.Price = shirt.Price;
				existingShirt.Size = shirt.Size;
				existingShirt.Gender = shirt.Gender;

				{
					// return shirts.FirstOrDefault(x => x.Brand == brand && x.Color == color && x.Size == size);
				}
			}
		}

		public static void DeleteShirt(int shirtId)
		{
			var shirtToDelete = GetShirtById(shirtId);
			if (shirtToDelete != null)
			{
				shirts.Remove(shirtToDelete);
			}
		}
	}
}
