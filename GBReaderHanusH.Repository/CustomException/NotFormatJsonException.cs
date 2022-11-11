using Newtonsoft.Json;

namespace GBReaderHanusH.Repository.CustomException;

public class NotFormatJsonException : JsonSerializationException
{
    public NotFormatJsonException() : base("Pas le bon format JSON(invalide)") { }
}