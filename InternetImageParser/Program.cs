using System.Diagnostics;
using HtmlAgilityPack;
using System.Net;

namespace InternetImageParser
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //string searchQuery = "Собака";
            Console.WriteLine($"Привет {Environment.UserName}! Напиши что будем искать. Поисковый запрос");
            string searchQuery = Console.ReadLine();

            Console.WriteLine("Сколько картинок скачать? Максимально 40");
            int imagesToDownload = Convert.ToInt32(Console.ReadLine());
            imagesToDownload = Math.Min(imagesToDownload, 40);

            Console.WriteLine("Куда будем сохранять? Если директории нет то она будет создана автоматом\r\nПример D:\\MyDownload");
            string saveDirectory = Console.ReadLine();


            Console.WriteLine($"Принято. Прмерное время ожидания ~{2 * 1.2d * imagesToDownload}\r\nМы начинаем!");
            string url = $"https://yandex.ru/images/search?text={searchQuery}&from=tabbar";

            string html = GetHtml(url);

            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            int count = 0;
            foreach (string imageUrl in await ExtractImageUrls(html))
            {
                DownloadImage(imageUrl, Path.Combine(saveDirectory, $"image{count}.jpg"));
                count++;

                if (count >= imagesToDownload)
                {
                    break;
                }

                await Task.Delay(600);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Операция успешно завершена. Все картинки скачены");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();

        }

        static string GetHtml(string url)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }

        static async Task<string[]> ExtractImageUrls(string html)
        {
            var imageUrls = new List<string>();

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            // Найдите элементы HTML, содержащие изображения
            // Например, используйте XPath или CSS-селекторы для найти теги <img> с атрибутом src

            // Пример с использованием XPath:
            var imgNodes = htmlDoc.DocumentNode.SelectNodes("//img[@src]");
            if (imgNodes != null)
            {
                foreach (var imgNode in imgNodes)
                {
                    await Task.Delay(600);
                    string imageUrl = imgNode.GetAttributeValue("src", "");
                    if (!string.IsNullOrWhiteSpace(imageUrl))
                    {
                        if (!imageUrl.StartsWith("http:") && !imageUrl.StartsWith("https:"))
                        {
                            imageUrl = "https:" + imageUrl;
                        }

                        imageUrls.Add(imageUrl);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(imageUrl);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }

            // Пример с использованием CSS-селектора:
            // var imgNodes = htmlDoc.DocumentNode.SelectNodes("img[src]");
            // ...


            return imageUrls.ToArray();
        }

        static void DownloadImage(string imageUrl, string savePath)
        {
            var time = Stopwatch.StartNew();
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(imageUrl, savePath);
            }
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"На скачивание и сохранение {savePath} файла было затрачено {time.Elapsed.TotalSeconds}сек.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}