using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace ParserLDB
{
    class Article
    {
        public List<string> order = new List<string>();
        public List<string> values = new List<string>();
    }

    class ArticleNew
    {
        public string udk = "";
        public string artName = "";
        public string magName = "";
        public string year = "";
        public string annotation = "";
        public string keyword = "";
        public string pages = "";
        public string magNumber = "";
        public string library = "";
        public string authors = "";
    }

    class Program
    {
        static void Main(string[] args)
        {
            File.Delete("out.csv");

            string[] numeration = new string[] { "001Контрольный номер",
            "005Дата корректировки", "008Кодиpуемые данные", "015Номер национальной б-ии", "017Офиц.депозитный номер",
            "020Индекс ISBN", "028Издательские номера для муз.", "040Создатель записи", "041Код языка издания",
            "080Индекс УДК", "086Номер правит. публикации", "100Автор", "110Коллективный автор", "245Название",
            "250Сведения об издании", "257Страна публикации/производства", "260Выходные данные", "300Колич. характеристики",
            "440Серия", "500Пpимечания", "504Библиография", "520Аннотация", "650Тематические рубрики",
            "653Ключевые слова", "700Другие авторы", "710Другие кол. авторы", "773Источник информации",
            "990Дата внесения в базу", "998Список литературы"};

            List<Article> articles = new List<Article>();
            List<ArticleNew> newArticles = new List<ArticleNew>();
                
            string path = "1.LDB";
            double counter = 0;
            using (StreamReader sr1 = new StreamReader(path, Encoding.GetEncoding(866))) //Подсчет общего количества символов в файле
            {
                while (sr1.Peek() >= 0)
                {
                    sr1.Read();
                    counter++;
                }
                sr1.Close();
            }
            Console.WriteLine("Подсчет окончен, {0} символов в файле", counter);
            

            double counterCurr = 0;
            StreamReader sr = new StreamReader(path, Encoding.GetEncoding(866));
            
            
            int c0 = 0;
            int c1 = sr.Read();
            counterCurr = 4;

            while (sr.Peek() >= 0)
            {
                int symb = sr.Read();
                counterCurr++;

                c0 = c1;
                c1 = symb;

                if (c0 == 30 && c1 == 29 && sr.Peek() >= 0) //если обнаружены в тексте два последовательных символа RS и GS, начинаем заполнять данные о новой статье
                {
                    //Console.WriteLine("Новая статья!");

                    Article art = new Article();
                    string firstStr = "";

                    while (sr.Peek() != 30) //считываем все символы до симвода RS
                    {
                        symb = sr.Read();
                        counterCurr++;
                        firstStr += (char)symb;
                    }

                    firstStr = firstStr.Remove(0, 24);//убираем лишнее из считанной строки
                    for (int i = 0; i < (firstStr.Length / 12); i++)//извлекаем только нужную инфу(группы по 3 важных символа через каждые 9 мусорных символа)
                    {
                        string cutStr = firstStr.Substring(i * 12, 3);
                        art.order.Add(cutStr);
                        //Console.WriteLine("Блок: " + cutStr);
                    }
                    //Console.ReadKey();

                    //считываем данные о статье, используя в качетве разделителей последовательность символов RS??US? (первая строка только RS)
                    firstStr = "";
                    symb = sr.Read();//считываем символ RS
                    counterCurr++;
                    while (sr.Peek() != 30) //считываем все символы до появления RS
                    {
                        symb = sr.Read();
                        counterCurr++;
                        firstStr += (char)symb;
                    }
                    art.values.Add(firstStr);
                    //Console.WriteLine("Первая строка: " + firstStr);
                    //Console.ReadKey();

                    symb = sr.Read();//считываем символ RS
                    counterCurr++;
                    while (symb != 30 || sr.Peek() != 29)
                    {
                        firstStr = "";
                        while (sr.Peek() != 30) //считываем все символы до появления RS
                        {
                            symb = sr.Read();
                            counterCurr++;

                            if (symb != 31) //если встречается символ US, заменяем их на ", "
                                firstStr += (char)symb;
                            else
                            {
                                symb = sr.Read();
                                counterCurr++;
                                firstStr += ", ";
                            }
                        }
                        firstStr = firstStr.Remove(0, 4);
                        art.values.Add(firstStr);
                        //Console.WriteLine("Значение считано: " + firstStr);
                        Console.WriteLine("1 step " + Math.Round(counterCurr/counter, 3)*100 + "%");


                        symb = sr.Read();//считываем символ RS
                        counterCurr++;
                    }

                    articles.Add(art);
                    //Console.WriteLine("Статья добавлена!");
                    //Console.ReadKey();
                    c1 = symb;
                }         
            }
            sr.Close();

            Console.Write("Анализ завершен, формируем файл...");


            counter = articles.Count;
            counterCurr = 1;
            //запись всех полученных данных
            foreach (Article art in articles)
            {
                ArticleNew newart = new ArticleNew();                
                int i = 0;
                foreach (string ord in art.order)
                {
                    switch (ord)
                    {
                        case "080":
                            newart.udk = art.values[i];
                            break;
                        case "245":
                            newart.artName = art.values[i];
                            break;
                        case "773":
                            string sourceInfo = art.values[i];
                            string[] sourceInfos = sourceInfo.Split(',');
                            newart.magName = sourceInfos[0];
                            string magNumberAndPages = sourceInfos[sourceInfos.Length - 1].Trim();//последний элемент
                            try{
                                newart.magNumber = magNumberAndPages.Trim().Substring(magNumberAndPages.IndexOf('N') + 1, magNumberAndPages.IndexOf('.') - magNumberAndPages.IndexOf('N') - 1).Trim();
                            }
                            catch{
                                newart.magNumber = " ";
                            }
                                
                            newart.pages = magNumberAndPages.Substring(magNumberAndPages.LastIndexOf('.') + 1);

                            // год издания
                            foreach (string sourceInfosStr in sourceInfos)
                                if (sourceInfosStr.Trim().Length == 4)
                                {
                                    newart.year = sourceInfosStr.Trim();
                                    break;
                                }     
                            break;
                        case "520":
                            newart.annotation = art.values[i];
                            break;
                        case "653":
                            newart.keyword = art.values[i];
                            break;
                        case "998":
                            if (art.values[i].ToUpper().Contains("ЛИТЕРАТУРА"))
                            {
                                //используем регулярные выражения, чтобы вытащить из строки только цифры
                                string pattern = @"\d+";
                                Regex r = new Regex(pattern);
                                Match m = r.Match(art.values[i]);                                
                                while (m.Success)
                                {
                                    newart.library = m.ToString();
                                    m = m.NextMatch();
                                }
                            }
                            break;
                        case "100":
                            if (newart.authors != "")
                                newart.authors += ", ";
                            newart.authors += art.values[i];
                            break;
                        case "110":
                            if (newart.authors != "")
                                newart.authors += ", ";
                            newart.authors += art.values[i];
                            break;
                        case "700":
                            if (newart.authors != "")
                                newart.authors += ", ";
                            newart.authors += art.values[i];
                            break;
                        case "710":
                            if (newart.authors != "")
                                newart.authors += ", ";
                            newart.authors += art.values[i];
                            break;
                    }
                    

                    i++;
                }
                if (newart.annotation == "")
                    newart.annotation = " ";
                if (newart.artName == "")
                    newart.artName = " ";
                if (newart.authors == "")
                    newart.authors = " ";
                if (newart.keyword == "")
                    newart.keyword = " ";
                if (newart.library == "")
                    newart.library = " ";
                if (newart.magName == "")
                    newart.magName = " ";
                if (newart.magNumber == "")
                    newart.magNumber = " ";
                if (newart.pages == "")
                    newart.pages = " ";
                if (newart.udk == "")
                    newart.udk = " ";
                if (newart.year == "")
                    newart.year = " ";

                newArticles.Add(newart);
                Console.WriteLine("2 step " + Math.Round(counterCurr / counter, 3)*100 + "%");
                counterCurr++;
            }

            using (StreamWriter sw = new StreamWriter("out.csv"))
            {
                counter = newArticles.Count;
                counterCurr = 1;
                foreach (ArticleNew newart in newArticles)
                {
                    Console.WriteLine("3 step " + Math.Round(counterCurr / counter, 3) * 100 + "%");
                    sw.Write('"' + "article_outside" + '"' + ';' + '"' + "pref_");
                    string pref1 = "00000000000000";
                    string pref2 = "00000000";
                    string pref = pref1.Remove(0, counterCurr.ToString().Length) + counterCurr + '.' + pref2.Remove(0, counterCurr.ToString().Length) + counterCurr;
                    sw.Write(pref + '"' + ';' + '"' + newart.udk + '"' + ';' + '"');
                    sw.Write(newart.artName + '"' + ';' + '"');
                    sw.Write(newart.magName + '"' + ';' + '"');
                    sw.Write(newart.year + '"' + ';' + '"' + '1' + '"' + ';' + '"');
                    sw.Write(newart.annotation + '"' + ';' + '"');
                    sw.Write(newart.keyword + '"' + ';' + '"');
                    sw.Write(newart.pages + '"' + ';' + '"');
                    sw.Write(newart.magNumber + '"' + ';' + '"');
                    sw.Write(newart.library + '"' + ';' + '"');
                    sw.WriteLine(newart.authors + '"');

                    counterCurr++;
                }
            }

            Console.Write("DONE!");

            Console.ReadKey();
        }
    }
}
