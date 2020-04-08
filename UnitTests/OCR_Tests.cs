using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tinyOCREngine;

namespace UnitTests
{
    [TestClass]
    public class OCR_Tests
    {
        static IEnumerable<object[]> getTestData()
        {
            DirectoryInfo d = new DirectoryInfo(@"..\..\..\data");
            foreach (var file in d.GetFiles("*.png"))
            {
                yield return new object[] 
                {
                    file.FullName,
                    $"{Path.GetFileNameWithoutExtension(file.Name)[0]}" 
                };
            }
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Act
            var actual = OCR.GetTextFromImage(filePath);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
