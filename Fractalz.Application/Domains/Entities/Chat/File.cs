using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Entities.Chat
{
    public class File
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public string FileName { get; set; }
        public long ByteLength { get; set; }
    }
}
