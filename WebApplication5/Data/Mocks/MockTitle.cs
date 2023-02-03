using WebApplication5.Data.Interfaces;

namespace WebApplication5.Data.Mocks
{
    public class MockTitle : ITitle
    {
        public string GetTitle(int Id)
        {
            switch (Id)
            {
                case 0:
                    return "Новости";
                case 1:
                    return "История";
                case 2:
                    return "Теория";
                case 3:
                    return "Примеры практического использования";
                case 4:
                    return "Использованные источники";
                default:
                    return null;
            }
        }
    }
}
