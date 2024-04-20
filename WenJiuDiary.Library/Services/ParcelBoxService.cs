namespace WenJiuDiary.Library.Services;

public class ParcelBoxService : IParcelBoxService
{
    private readonly Dictionary<string, object> _parcelBox = new();

    public object Get(string ticket) =>
    
        _parcelBox.TryGetValue(ticket, out object o) ? o : null;
    

    public string Put(object o)
    {
        var token = Guid.NewGuid().ToString();
        _parcelBox[token] = o;


        return token;
    }
}