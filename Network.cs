using System.Diagnostics;

namespace WorkWithJSON
{
    internal class SetIpAddressWmi()
    {
        string interfaceName = "Ethernet 2"; // Укажите имя сетевого интерфейса
        string ipAddress = "192.168.88.20";
        string subnetMask = "255.255.255.0";
        string gateway = "192.168.88.1";
        string dns = "8.8.8.8";

        // Команда для настройки IP-адреса, маски и шлюза
        string setIpCommand = $"netsh interface ip set address \"{interfaceName}\" static {ipAddress} {subnetMask} {gateway} 1";

        // Команда для настройки DNS-сервера
        string setDnsCommand = $"netsh interface ip set dns \"{interfaceName}\" static {dns}";

        ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c " + setIpCommand + " && " + setDnsCommand);
        psi.CreateNoWindow = true;
        psi.UseShellExecute = false;
        psi.Verb = "runas"; // Запуск от имени администратора
        Process.Start(psi);
        
    }
}


public class NetworkInterfaceManager
{
    public void RestartNetworkAdapter(string interfaceName)
    {
        // Команда для отключения сетевого адаптера
        string disableCommand = $"netsh interface set interface \"{interfaceName}\" admin=disable";
        // Команда для включения сетевого адаптера
        string enableCommand = $"netsh interface set interface \"{interfaceName}\" admin=enable";

        // Отключаем адаптер
        ExecuteCommand(disableCommand);
        System.Threading.Thread.Sleep(2000); // Небольшая задержка перед включением

        // Включаем адаптер
        ExecuteCommand(enableCommand);
    }

    private void ExecuteCommand(string command)
    {
        ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c " + command)
        {
            CreateNoWindow = true,
            UseShellExecute = false,
            Verb = "runas" // Запуск от имени администратора
        };
        Process.Start(psi);
    }
}

// class Program
// {
//     static void Main()
//     {
//         NetworkInterfaceManager manager = new NetworkInterfaceManager();
//         manager.RestartNetworkAdapter("Ethernet"); // Укажите нужное имя интерфейса
//     }
}