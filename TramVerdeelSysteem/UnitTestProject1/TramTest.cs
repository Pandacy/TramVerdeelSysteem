﻿using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTest
{
    [TestClass]
    public class TramTest
    {
        [TestMethod]
        public void AddTramTest()
        {
            //Arrange
            //Tram tram = new Tram();

            //Act
            //for (int i = 817; i < 842; i++)
            //{
            //    tram.AddTram(4, 3, 1, i);
            //}

            //Assert
        }

        [TestMethod]
        public void AddSectorTest()
        {
            //Arrange
            Tram tram = new Tram();

            //Act
            for (int i = 1; i < 6; i++)
            {
                tram.AddSector(42, i);
            }

            //Assert
        }
    }
}
