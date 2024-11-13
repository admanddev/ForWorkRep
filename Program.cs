using System.Text.Json;

namespace WorkWithJSON;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string configPath = Path.Combine(AppContext.BaseDirectory, "Config.json");
            // Открываем или создаём файл Config.json
            using (FileStream json = new FileStream(configPath, FileMode.OpenOrCreate))
            {
                // Десериализуем JSON в словарь (Dictionary)
                Dictionary<string, string>? myDict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

                // Проверяем, что десериализация прошла успешно и словарь не null
                if (myDict != null)
                {
                    // Попытка получить значения по ключам
                    myDict.TryGetValue("ipForGetMAC1", out string? host);
                    myDict.TryGetValue("ipTrackerAndCB", out string? ipTrackerCB);
                    myDict.TryGetValue("userName", out string? userName);
                    myDict.TryGetValue("password", out string? password);

                    // Выводим значения в консоль
                    Console.WriteLine(host ?? "ipForGetMAC не найдено");
                    Console.WriteLine(ipTrackerCB ?? "ipTrackerAndCB не найдено");
                    Console.WriteLine(userName ?? "userName не найдено");
                    Console.WriteLine(password ?? "password не найдено");
                }
                else
                {
                    Console.WriteLine("NE удалось прочитать данные из файла Config.json.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }
}