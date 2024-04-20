namespace WenJiuDiary.Library.Services;

//中间存储服务
public interface IParcelBoxService
{
    string Put(object o);

    object Get(string ticket);
}