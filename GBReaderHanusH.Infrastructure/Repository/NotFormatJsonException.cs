namespace GBReaderHanusH.Infrastructure.Repository;

public class NotFormatJsonException:Exception
{
    public NotFormatJsonException() : base("Pas le bon format JSON(invalide)"){}
}