using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBReaderHanusH.Repository.CustomException
{
    public class NotAbleSaveException : DirectoryNotFoundException
    {
        public NotAbleSaveException() : base("Erreur lors de écriture du fichier") { }
    }
}
