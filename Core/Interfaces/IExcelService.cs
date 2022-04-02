using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IExcelService<T> where T : class
    {
        public string GetExcelFromObject(IReadOnlyList<T> objects);
    }
}
