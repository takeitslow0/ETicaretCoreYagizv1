using DataAccess.Contexts;
using DataAccess.Repoistories.Bases;

namespace DataAccess.Repoistories
{
    public class KategoriRepo : KategoriRepoBase
    {
        public KategoriRepo() : base()
        {

        }

        public KategoriRepo(ETicaretContext eTicaretContext) : base(eTicaretContext)
        {

        }
    }
}
