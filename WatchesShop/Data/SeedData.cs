using WatchesShop.Models;
using Microsoft.EntityFrameworkCore;

namespace WatchesShop.Data
{
    public static class SeedData
    {
        public static void Initialize(WatchContext context)
        {
            // ѕроверка, чтобы не дублировать данные
            if (!context.Brands.Any())
            {
                context.Brands.AddRange(
                    new Brand { Name = "Alfex" },
                    new Brand { Name = "Apple" },
                    new Brand { Name = "Atlantic" },
                    new Brand { Name = "Balmain" },
                    new Brand { Name = "Bulova" },
                    new Brand { Name = "Casio" },
                    new Brand { Name = "Christina" },
                    new Brand { Name = "Cluse" },
                    new Brand { Name = "Diesel" },
                    new Brand { Name = "Elite" },
                    new Brand { Name = "Garmin" },
                    new Brand { Name = "Hamilton" },
                    new Brand { Name = "Jaguar" },
                    new Brand { Name = "Luminox" },
                    new Brand { Name = "Oris" },
                    new Brand { Name = "Rado" },
                    new Brand { Name = "Tissot" },
                    new Brand { Name = "Zeppelin" }
                );
                context.SaveChanges();
            }

            if (!context.Styles.Any())
            {
                context.Styles.AddRange(
                    new Style { Name = "Fashion" },
                    new Style { Name = "Classic" },
                    new Style { Name = "Sport" }
                );
                context.SaveChanges();
            }

            if (!context.Colors.Any())
            {
                context.Colors.AddRange(
                    new WatchColor { Name = "Black" },
                    new WatchColor { Name = "Silver" },
                    new WatchColor { Name = "Golden" },
                    new WatchColor { Name = "White" }
                );
                context.SaveChanges();
            }

            if (!context.Materials.Any())
            {
                context.Materials.AddRange(
                    new Material { Name = "Rubber" },
                    new Material { Name = "Ceramics" },
                    new Material { Name = "Leather" },
                    new Material { Name = "Steel" },
                    new Material { Name = "Silicone" }
                );
                context.SaveChanges();
            }

            if (!context.Mechanisms.Any())
            {
                context.Mechanisms.AddRange(
                    new Mechanism { Name = "Quartz" },
                    new Mechanism { Name = "Mechanics with self-winding" },
                    new Mechanism { Name = "Mechanical" }
                );
                context.SaveChanges();
            }

            if (!context.Genders.Any())
            {
                context.Genders.AddRange(
                    new Gender { Name = "Male" },
                    new Gender { Name = "Female" },
                    new Gender { Name = "Unisex" }
                );
                context.SaveChanges();
            }

            if (!context.Cases.Any())
            {
                context.Cases.AddRange(
                    new Case { Diameter = 42.5m, MaterialId = 5, WaterResistance = 30m, ColorId = 1 },
                    new Case { Diameter = 42.5m, MaterialId = 5, WaterResistance = 30m, ColorId = 4 },
                    new Case { Diameter = 54m, MaterialId = 5, WaterResistance = 50m, LengthCase = 41m, Thickness = 10.7m, Weight = 31.9m, ColorId = 4 },
                    new Case { Diameter = 44m, MaterialId = 4, WaterResistance = 50m, Thickness = 13m, ColorId = 1 },
                    new Case { Diameter = 46m, MaterialId = 3, WaterResistance = 100m, ColorId = 1 },
                    new Case { Diameter = 32m, MaterialId = 4, WaterResistance = 30m, Thickness = 6m, Weight = 77m, ColorId = 2 },
                    new Case { Diameter = 29m, MaterialId = 3, WaterResistance = 50m, ColorId = 4 },
                    new Case { Diameter = 42m, MaterialId = 4, WaterResistance = 100m, ColorId = 2 },
                    new Case { Diameter = 42m, MaterialId = 4, WaterResistance = 100m, ColorId = 2 },
                    new Case { Diameter = 43m, MaterialId = 4, WaterResistance = 100m, ColorId = 2 },
                    new Case { Diameter = 40m, MaterialId = 3, WaterResistance = 50m, ColorId = 1 },
                    new Case { Diameter = 45.5m, MaterialId = 4, ColorId = 1 },
                    new Case { Diameter = 48.5m, MaterialId = 5, WaterResistance = 200m, LengthCase = 48.5m, Thickness = 11.8m, Weight = 51m, ColorId = 1 },
                    new Case { Diameter = 38m, MaterialId = 4, WaterResistance = 30m, Thickness = 7m, ColorId = 3 },
                    new Case { Diameter = 45m, MaterialId = 3, WaterResistance = 100m, ColorId = 1 },
                    new Case { Diameter = 44m, MaterialId = 5, WaterResistance = 50m, ColorId = 4 },
                    new Case { Diameter = 46m, MaterialId = 3, WaterResistance = 100m, ColorId = 1 }
                );
                context.SaveChanges();
            }
        }
    }
}