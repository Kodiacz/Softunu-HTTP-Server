namespace SoftuniHTTPServer.HTTP
{
    public enum HttpStatusCode
    {
        Ok = 200,
        MovedPermenantly = 301,
        Found = 302,
        TemporaryRedirect = 307,
        NotFound = 404,
        InternalServerError = 500
    }
}
