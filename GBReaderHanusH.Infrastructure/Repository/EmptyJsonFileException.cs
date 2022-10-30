

namespace GBReaderHanusH.Infrastructure.Repository;

public class EmptyJsonFileException :Exception
{
    public EmptyJsonFileException():base("Fichier vide aucun livre de charge"){}
}