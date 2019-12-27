using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace NUnitTestProject1
{
    [TestFixture]
    public class DocsMicrosoftSearchTests
    {
        private Regex _regex;

        [SetUp]
        public void Check()
        {
            _regex = new Regex(@"(L|l)(I|i)(N|n)(Q|q)");
        }

        [Test]
        [TestCase(0)]
        [TestCase(26)]
        public void Search_(int count)
        {
            //Количество совпадений для финальной проверки выборки
            int matches = 0;

            var client = new RestClient($"https://docs.microsoft.com/api/Search?locale=ru-ru&search=linq&$skip={count}&$top=25");
            //Параметры
            //0 -   &search  =   linq; - Поисковой запрос
            //1 -   &$skip   =  count; - Пропуск предыдущих
            //2 -   &$top    =     25; - Вывод 25 элементов

            var request = new RestRequest(Method.GET);

            var response = client.Execute(request);

            //Десереализация ответа, при помощи класса пользовательского класса RootObject
            var obj = JsonConvert.DeserializeObject<TestTask.JsonPattern.RootObject>(response.Content);

            //Проверяем результаты на совпадение с шаблоном 
            foreach (var title in obj.results)
                if (_regex.IsMatch(title.title))
                    matches++;

            //Если все результаты содержат "linq"(регистр букв не учитывается благодаря шаблону),
            //то тест пройдет
            //иначе - нет.
            Assert.That(matches, Is.EqualTo(25));
        }
    }
}
