using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using JUnitViewer.JUnit.Model;

namespace JUnitViewer.JUnit.Parser
{
    public class JUnitParser
    {
        private JUnitTestSuite testSuite;
        private List<JUnitTestCase> testCases;

        private static readonly string testSuiteNodeName = "testsuite";
        private static readonly string failureNodeName = "failure";
        private static readonly string skippedNodeName = "skipped";
        private static readonly string failureMessageAttribute = "message";
        private static readonly string testSuiteNameAttribute = "name";
        private static readonly string testSuiteTestsAttribute = "tests";
        private static readonly string testSuiteFailuresAttribute = "failures";
        private static readonly string testSuiteTimeAttribute = "time";

        private static readonly string testCaseNodeName = "testcase";
        private static readonly string testCaseClassnameAttribute = "classname";
        private static readonly string testCaseNameAttribute = "name";
        private static readonly string testCaseTimeAttribute = "time";

        public JUnitParser()
        {
            testSuite = new JUnitTestSuite();
            testCases = new List<JUnitTestCase>();
        }

        public JUnitTestSuite getTestSuite()
        {
            return testSuite;
        }

        public IEnumerable<XElement> getStartNode(string fileLocation)
        {
            XDocument xmlDoc = new XDocument(fileLocation);
            XElement doc = XElement.Parse(xmlDoc.ToString());
            IEnumerable<XElement> nodeList = doc.Descendants(testSuiteNodeName);
            return nodeList;
        }

        public void addTestCasesToTestSuite()
        {
            foreach (JUnitTestCase tc in testCases)
            {
                testSuite.addTestCase(tc);
            }
        }

        private JUnitTestStep setFailureMessage(JUnitTestStep testStep, XElement element)
        {
            IEnumerable<XElement> childNodeList = element.Descendants();
            for (int j = 0; j < childNodeList.Count(); j++)
            {
                XElement childNode = childNodeList.ElementAt(j);

                if (childNode.Name == skippedNodeName)
                {
                    testStep.setSkipped(true);
                    return testStep;
                }

                if (childNode.NodeType.ToString() == "Element")
                {
                    XElement childElement = childNode;

                    if (childNode.Name == failureNodeName)
                    {
                        testStep.setAssertionFailures(childElement.Attribute(failureMessageAttribute).ToString(), childElement.Value);
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
                    if (node.Name == testSuiteNodeName)
                    {
                        testSuite.setName(element.Attribute(testSuiteNameAttribute).ToString());
                        try
                        {
                            testSuite.setTests(Int32.Parse(element.Attribute(testSuiteTestsAttribute).ToString()));
                            testSuite.setFailures(Int32.Parse(element.Attribute(testSuiteFailuresAttribute).ToString()));
                            testSuite.setTime(Double.Parse(element.Attribute(testSuiteTimeAttribute).ToString()));

                            if (element.Attribute(skippedNodeName).ToString() == null)
                            {
                                testSuite.setSkipped(0);
                            }
                            else
                            {
                                testSuite.setSkipped(Int32.Parse(element.Attribute(skippedNodeName).ToString()));
                            }
                        }
                        catch (FormatException e)
                        {
                            throw new Exception("ERROR: Not able to parse attribute: " + e.Message + "! Please check your input file format");
                        }
                    }

                    if (node.Name == (testCaseNodeName))
                    {
                        JUnitTestCase testCase = new JUnitTestCase();
                        testCase.setClassName(element.Attribute(testCaseClassnameAttribute).ToString());
                        JUnitTestStep testStep = new JUnitTestStep();
                        testStep.setName(element.Attribute(testCaseNameAttribute).ToString());
                        testStep.setTime(element.Attribute(testCaseTimeAttribute).ToString());

                        if (!testCases.Any())
                        {
                            testStep = setFailureMessage(testStep, element);
                            testCase.addTestStep(testStep);
                            this.testCases.Add(testCase);
                        } else {
                            bool exists = false;
                            foreach (JUnitTestCase tCase in testCases)
                            {
                                if (tCase.getClassName() == testCase.getClassName())
                                {
                                    testStep = setFailureMessage(testStep, element);
                                    tCase.addTestStep(testStep);
                                    exists = true;
                                    break;
                                }
                            }
                            if (!exists)
                            {
                                testStep = setFailureMessage(testStep, element);
                                testCase.addTestStep(testStep);
                                testCases.Add(testCase);
                            }
                        }
                    }
                }

            if (node.Elements().Any())
            {
                ParseJUnitResults(node.Elements());
            }
        }
        }
    }
}
