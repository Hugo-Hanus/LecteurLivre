using Newtonsoft.Json;

namespace GBReaderHanusH.Repository.CustomException;

public class EmptyJsonFileException : JsonSerializationException
{
    public EmptyJsonFileException() : base("Fichier vide aucun livre de charge") { }
}