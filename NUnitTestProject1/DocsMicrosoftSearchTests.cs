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
            //���������� ���������� ��� ��������� �������� �������
            int matches = 0;

            var client = new RestClient($"https://docs.microsoft.com/api/Search?locale=ru-ru&search=linq&$skip={count}&$top=25");
            //���������
            //0 -   &search  =   linq; - ��������� ������
            //1 -   &$skip   =  count; - ������� ����������
            //2 -   &$top    =     25; - ����� 25 ���������

            var request = new RestRequest(Method.GET);

            var response = client.Execute(request);

            //�������������� ������, ��� ������ ������ ����������������� ������ RootObject
            var obj = JsonConvert.DeserializeObject<TestTask.JsonPattern.RootObject>(response.Content);

            //��������� ���������� �� ���������� � �������� 
            foreach (var title in obj.results)
                if (_regex.IsMatch(title.title))
                    matches++;

            //���� ��� ���������� �������� "linq"(������� ���� �� ����������� ��������� �������),
            //�� ���� �������
            //����� - ���.
            Assert.That(matches, Is.EqualTo(25));
        }
    }
}
