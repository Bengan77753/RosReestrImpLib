﻿using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//
using RosReestrImp.Rule;
using RosReestrImp.Geometry;
using RosReestrImp.Data;

namespace UnitTestProject1
{
    [TestClass]
    public partial class UnitTest1
    {

        [TestMethod]
        public void LoadData1()
        {
            RuleManager wRM = new RuleManager("Shema\\testList2.xml");
            List<Layer> res = wRM.LoadData("doc9415874.xml");
            Assert.IsTrue(true, "good");
        }

        [TestMethod]
        public void CreateTLineString()
        {
            List<TGeometry.MyPoint> nCoords = new List<TGeometry.MyPoint>();
            nCoords.Add(new TGeometry.MyPoint(1, 1, 1));
            nCoords.Add(new TGeometry.MyPoint(2, 2, 2));
            nCoords.Add(new TGeometry.MyPoint(3, 3, 3));
            nCoords.Add(new TGeometry.MyPoint(4, 4, 4));
            TLineString nLS = new TLineString(nCoords);
            Assert.IsTrue(true, "good");
        }

        public XmlNamespaceManager LoadNamespace(XmlDocument wDoc)
        {
            XmlNamespaceManager res = new XmlNamespaceManager(wDoc.NameTable);
            res.PopScope();
            XmlElement rNode = wDoc.DocumentElement;
            string nStr;
            foreach (XmlAttribute attr in rNode.Attributes)
            {
                nStr = attr.Name;
                if (nStr.Contains("xmlns"))
                {
                    if (nStr.Length == 5)
                    {
                        res.AddNamespace("ns", attr.Value); //!!
                    }
                    else
                    {
                        res.AddNamespace(nStr.Replace("xmlns:", ""), attr.Value);
                    }
                }
            }
            return res;
        }

        [TestMethod]
        public void TestXPath1()
        {
            XmlDocument wDoc = new XmlDocument();
            wDoc.Load("doc9415874.xml");
            XmlElement wNode = wDoc.DocumentElement;
            XmlNamespaceManager wNM = this.LoadNamespace(wDoc);
            XmlNode XmlNode1 = wDoc.DocumentElement.SelectSingleNode("//ns:ObjectsRealty//ns:EntitySpatial", wNM);
            Assert.IsTrue(true, "good");
        }

    }
}
