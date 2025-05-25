using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom;

record TextFileDto(string Text, IEnumerable<Header> Headers)
{
}
