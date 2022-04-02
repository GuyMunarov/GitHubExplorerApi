using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IHashingService
    {
        public void HashPassword(string password, out byte[] hash, out byte[] salt);
        public bool CheckHashEquality(string password, byte[] userHash, byte[] salt);
    }
}
