using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using WatchesShop.Models;
using WatchesShop.Data;

namespace WatchesShop.Services
{
    public class WatchService : IWatchService
    {
        private readonly WatchContext _context;


        public WatchService(WatchContext context)
        {
            _context = context;
        }

        public IEnumerable<Watch> GetWatches()
        {
            var watches = _context.Watches
                                  .Include(w => w.Case)
                                  .ThenInclude(c => c.Color)
                                  .Include(w => w.Case)
                                  .ThenInclude(c => c.Material)
                                  .Include(w => w.MechanismType)
                                  .ToList();

            if (watches == null || !watches.Any())
            {
                // Логирование или отладочный вывод
                Console.WriteLine("No watches found in the database.");
            }

            return watches;
        }



        public IEnumerable<Brand> GetBrands()
        {
            return _context.Brands.ToList();
        }

        public IEnumerable<Gender> GetGenders()
        {
            return _context.Genders.ToList();
        }

        public IEnumerable<Style> GetStyles()
        {
            return _context.Styles.ToList();
        }

        public IEnumerable<WatchColor> GetColors()
        {
            return _context.Colors.ToList();
        }

        public IEnumerable<Material> GetMaterials()
        {
            return _context.Materials.ToList();
        }

        public IEnumerable<Mechanism> GetMechanisms()
        {
            return _context.Mechanisms.ToList();
        }

        public Watch GetWatchById(int id)
        {
            return _context.Watches
                .Include(w => w.Brand)
                .Include(w => w.Gender)
                .Include(w => w.MechanismType)
                .Include(w => w.Style)
                .Include(w => w.Case)
                    .ThenInclude(c => c.Material)
                .Include(w => w.Case)
                    .ThenInclude(c => c.Color)
                .FirstOrDefault(w => w.WatchId == id);
        }

        public void AddWatch(Watch watch)
        {
            _context.Watches.Add(watch);
            _context.SaveChanges();
        }

        public void AddCase(Case watchCase)
        {
            _context.Cases.Add(watchCase);
            _context.SaveChanges();
        }
        public void UpdateWatch(Watch watch)
        {
            _context.Watches.Update(watch);
            _context.SaveChanges();
        }

        public void DeleteWatch(int id)
        {
            var watch = _context.Watches.Find(id);
            if (watch != null)
            {
                _context.Watches.Remove(watch);
                _context.SaveChanges();
            }
        }
    }
}
