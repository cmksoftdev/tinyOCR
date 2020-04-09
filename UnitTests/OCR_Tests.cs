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
        public void _All_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Act
            var actual = OCR.GetTextFromImage(filePath);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        void testBasemethod(string checkedChar, string filePath, string expected)
        {
            // Act
            var actual = OCR.GetTextFromImage(filePath);

            // Assert
            if (expected == checkedChar)
                Assert.AreEqual(expected, actual);
            else if (actual == checkedChar)
                Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void A_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "a";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void B_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "b";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void C_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "c";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void D_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "d";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void E_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "e";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void F_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "f";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void G_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "g";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void H_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "h";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void I_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "i";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void J_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "j";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void K_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "k";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void L_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "l";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void M_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "m";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void N_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "n";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void O_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "o";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void P_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "p";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void Q_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "q";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void R_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "r";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void S_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "s";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void T_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "t";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void U_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "u";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void V_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "v";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void W_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "w";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void X_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "x";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void Y_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "y";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }

        [TestMethod()]
        [DynamicData(nameof(getTestData), DynamicDataSourceType.Method)]
        public void Z_Test_GetTextFromImage_ImagesFromFiles(string filePath, string expected)
        {
            // Arrange
            string checkedChar = "z";

            // Act
            testBasemethod(checkedChar, filePath, expected);
        }
    }
}
