using System;
using System.Threading;
using Renci.SshNet;

public class SshConnectionChecker
{
    private string ip = "192.168.88.20";  // IP-адрес узла
    private string user = "username";
    private string password = "password";
    private int retryInterval = 5000;  // Интервал между попытками (мс)
    private int maxAttempts = 10;      // Максимальное количество попыток

    public void CheckSshConnection()
    {
        int attempts = 0;
        bool isConnected = false;

        while (attempts < maxAttempts && !isConnected)
        {
            using (var sshClient = new SshClient(ip, user, password))
            {
                try
                {
                    attempts++;
                    Console.WriteLine($"Попытка {attempts} подключения...");

                    sshClient.Connect();
                    if (sshClient.IsConnected)
                    {
                        Console.WriteLine("Соединение восстановлено.");
                        isConnected = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Попытка {attempts} не удалась: {ex.Message}");
                }
                finally
                {
                    if (sshClient.IsConnected)
                        sshClient.Disconnect();
                }

                if (!isConnected)
                {
                    Thread.Sleep(retryInterval); // Ожидание перед следующей попыткой
                }
            }
        }

        if (!isConnected)
        {
            Console.WriteLine("Не удалось восстановить SSH-подключение после нескольких попыток.");
        }
    }
}
