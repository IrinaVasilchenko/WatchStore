using System.Collections.Generic;
using WatchesShop.Models;
using WatchesShop.Data;

namespace WatchesShop.Services
{
    public interface IWatchService
    {
        IEnumerable<Watch> GetWatches();
        
        Watch GetWatchById(int id);
        IEnumerable<Brand> GetBrands();
        IEnumerable<Gender> GetGenders();
        IEnumerable<WatchColor> GetColors();
        IEnumerable<Material> GetMaterials();
        IEnumerable<Mechanism> GetMechanisms();
        IEnumerable<Style> GetStyles();

        void AddWatch(Watch watch);
        void AddCase(Case watchCase);
        void UpdateWatch(Watch watch);
        void DeleteWatch(int id);
    }
}
