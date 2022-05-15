using AppCore.Business.Services.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services.Bases
{
    public interface IKategoriService : IService<KategoriModel, Kategori, ETicaretContext>
    {

    }
}
