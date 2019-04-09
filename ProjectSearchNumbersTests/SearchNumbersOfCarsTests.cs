using System.Collections.Generic;
using NUnit.Framework;
using ProjectSearchNumbers;


namespace ProjectSearchNumbersTests
{
    [TestFixture]
    public class SearchNumbersOfCarsTests
    {
        [Test]
        public void ReadFromFile_NotEmptyFileWithCorrectName_NotNull()
        {
            //проверка на содержание данных в файле с корректным именем
            string nameOfFile = "NumbersOfCars.txt";
            string[] numbersFromFile = SearchNumbersOfCars.ReadFromFile(nameOfFile);
            Assert.NotNull(numbersFromFile);
        }

        [Test]
        public void ReadFromFile_FileWithIncorrectName_Null()
        {
            //проверка поведения программы с указанием некорректного имени файла
            string nameOfFile = "NumbersOfCar.txt";
            string[] numbersFromFile = SearchNumbersOfCars.ReadFromFile(nameOfFile);
            Assert.IsNull(numbersFromFile);
        }

        [TestCase("")]
        [TestCase("   ")]
        public void InputNumberIsEmptyOrHasOnlyWhiteSpaces_True(string value)
        {
            //проверка вводимого номера, состоящего из пустой строки или только из пробелов
            bool result = SearchNumbersOfCars.InputNumberIsEmpty(value);
            Assert.IsTrue(result);
        }

        [TestCase("а423  пг47")]
        [TestCase("а423 по47")]
        public void InputNumberContainsSpace_True(string value)
        {
            //проверка на содержание в строке пробелов
            bool result = SearchNumbersOfCars.InputNumberContainsSpace(value);
            Assert.IsTrue(result);
        }

        [Test]
        public void InputNumberHasBigLength_True()
        {
            //проверка ввода номера с длиной больше допустимой(10 символов)
            string number = "аааааааааа";
            bool result = SearchNumbersOfCars.InputNumberHasBigLength(number);
            Assert.IsTrue(result);
        }

        //проверка формата строки 
        [TestCaseSource("GetTestFormats")]
        public bool InputStringHasCorrectFormat(string value)
        {

            bool numberinput = SearchNumbersOfCars.InputNumberHasCorrectFormat(value);
            return (numberinput);
        }

        public static IEnumerable<TestCaseData> GetTestFormats()
        {
            yield return new TestCaseData("п").Returns(true);
            yield return new TestCaseData("п0").Returns(true);
            yield return new TestCaseData("а012").Returns(true);
            yield return new TestCaseData("а012в").Returns(true);
            yield return new TestCaseData("а012во").Returns(true);
            yield return new TestCaseData("а012во10").Returns(true);
            yield return new TestCaseData("а012во124").Returns(true);
            yield return new TestCaseData("f").Returns(false);

        }

        //проверка ввода номера с корректным форматом(с номером региона из 3 цифр), но ненайденным в хранилище 
        [TestCase("а423пц124")]
        public void InputNumberWithCorrectFormatLength9Symb(string value)
        {

            List<string> results = SearchNumbersOfCars.GetResultsOfSearch(value, new List<string> { "л698ке124" });
            Assert.AreEqual(0, results.Count);
        }


    }
}
