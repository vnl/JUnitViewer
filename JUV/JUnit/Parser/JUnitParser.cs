using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using JUnitViewer;
using JUV.JUnit.Model;

namespace JUV.JUnit.Parser
{
    public class JUnitParser
    {
        private JUnitTestSuite _testSuite;
        private List<JUnitTestCase> _testCases;

        private static readonly string TestSuiteNodeName = "testsuite";
        private static readonly string FailureNodeName = "failure";
        private static readonly string SkippedNodeName = "skipped";
        private static readonly string FailureMessageAttribute = "message";
        private static readonly string TestSuiteNameAttribute = "name";
        private static readonly string TestSuiteTestsAttribute = "tests";
        private static readonly string TestSuiteFailuresAttribute = "failures";
        private static readonly string TestSuiteTimeAttribute = "time";

        private static readonly string TestCaseNodeName = "testcase";
        private static readonly string TestCaseClassnameAttribute = "classname";
        private static readonly string TestCaseNameAttribute = "name";
        private static readonly string TestCaseTimeAttribute = "time";
        private List<JUnitTestSuite> _tesSuitess;

        public JUnitParser()
        {
            _testSuite = new JUnitTestSuite();
            _testCases = new List<JUnitTestCase>();
            _tesSuitess = new List<JUnitTestSuite>();
        }

        public JUnitTestSuite GetTestSuite()
        {
            return _testSuite;
        }

        public IEnumerable<XElement> GetStartNode(string fileLocation)
        {
            XDocument xmlDoc = XDocument.Load(fileLocation);
            XElement doc = XElement.Parse(xmlDoc.ToString());
            IEnumerable<XElement> nodeList = doc.Descendants(TestSuiteNodeName);
            return nodeList;
        }

        public void AddTestCasesToTestSuite()
        {
            foreach (JUnitTestCase tc in _testCases)
            {
                _testSuite.AddTestCase(tc);
            }
        }

        private JUnitTestStep SetFailureMessage(JUnitTestStep testStep, XElement element)
        {
            IEnumerable<XElement> childNodeList = element.Descendants();
            for (int j = 0; j < childNodeList.Count(); j++)
            {
                XElement childNode = childNodeList.ElementAt(j);

                if (childNode.Name == SkippedNodeName)
                {
                    testStep.SetSkipped(true);
                    return testStep;
                }

                if (childNode.NodeType.ToString() == "Element")
                {
                    XElement childElement = childNode;

                    if (childNode.Name == FailureNodeName)
                    {
                        testStep.SetAssertionFailures(childElement.Attribute(FailureMessageAttribute).ToString(), childElement.Value);
                    }
                }
            }
            return testStep;
        }
        public void ParseJUnitResults(IEnumerable<XElement> nodeList)
        {

            for (int i = 0; i < nodeList.Count(); i++)
            {
                XElement node = nodeList.ElementAt(i);

                if (node.NodeType.ToString() == "Element")
                {
                    XElement element = (XElement)node;
                    if (node.Name == TestSuiteNodeName)
                    {
                        _testSuite = new JUnitTestSuite();
                        _tesSuitess.Add(_testSuite);
                        _testSuite.SetName(element.Attribute(TestSuiteNameAttribute).ToString());
                        try
                        {
                            _testSuite.SetTests(Int32.Parse(element.Attribute(TestSuiteTestsAttribute).Value.ToString()));
                            _testSuite.SetFailures(Int32.Parse(element.Attribute(TestSuiteFailuresAttribute).Value.ToString()));
                            _testSuite.SetTime(Double.Parse(element.Attribute(TestSuiteTimeAttribute).Value.ToString()));

                            if (element.Attribute(SkippedNodeName).ToString() == null)
                            {
                                _testSuite.SetSkipped(0);
                            }
                            else
                            {
                                _testSuite.SetSkipped(Int32.Parse(element.Attribute(SkippedNodeName).Value.ToString()));
                            }

                            if (node.Elements().Any())
                            {
                                _testSuite.TestCases = ParseTestCases(node.Elements());
                            }
                        }
                        catch (FormatException e)
                        {
                            throw new Exception("ERROR: Not able to parse attribute: " + e.Message + "! Please check your input file format");
                        }
                    }

                    
                }

                
            }
        }

        private List<JUnitTestCase> ParseTestCases(IEnumerable<XElement> nodes)
        {
            var testCases = new List<JUnitTestCase>();

            for (int i = 0; i < nodes.Count(); i++)
            {
                XElement node = nodes.ElementAt(i);

                if (node.NodeType.ToString() == "Element")
                {
                    XElement element = (XElement)node;

                    if (node.Name == (TestCaseNodeName))
                    {
                        JUnitTestCase testCase = new JUnitTestCase();
                        testCase.SetClassName(element.Attribute(TestCaseClassnameAttribute).ToString());
                        JUnitTestStep testStep = new JUnitTestStep();
                        testStep.SetName(element.Attribute(TestCaseNameAttribute).ToString());
                        testStep.SetTime(element.Attribute(TestCaseTimeAttribute).ToString());

                        if (!_testCases.Any())
                        {
                            testStep = SetFailureMessage(testStep, element);
                            testCase.AddTestStep(testStep);
                            testCases.Add(testCase);
                        }
                        else
                        {
                            bool exists = false;
                            foreach (JUnitTestCase tCase in testCases)
                            {
                                if (tCase.GetClassName() == testCase.GetClassName())
                                {
                                    testStep = SetFailureMessage(testStep, element);
                                    tCase.AddTestStep(testStep);
                                    exists = true;
                                    break;
                                }
                            }
                            if (!exists)
                            {
                                testStep = SetFailureMessage(testStep, element);
                                testCase.AddTestStep(testStep);
                                testCases.Add(testCase);
                            }
                        }
                    }
                }
            }
            return testCases;
        }
    }
}
