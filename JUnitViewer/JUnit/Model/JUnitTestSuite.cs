using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JUnitViewer.JUnit.Model
{
    /// <summary>
    /// This class serves as container of test cases and gives additional info on test suite level
    /// </summary>
    public class JUnitTestSuite
    {
        private string name;
        private int tests;
        private int failures;
        private int skipped;
        private double time;
        private List<JUnitTestCase> testCases = new List<JUnitTestCase>();

        public JUnitTestSuite()
        {

        }

        public List<JUnitTestCase> getTestCases()
        {
            return testCases;
        }

        public void addTestCase(JUnitTestCase testCase)
        {
            testCases.Add(testCase);
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public int getTests()
        {
            return tests;
        }

        public void setTests(int tests)
        {
            this.tests = tests;
        }

        public int getFailures()
        {
            return failures;
        }

        public void setFailures(int failures)
        {
            this.failures = failures;
        }

        public int getSkipped()
        {
            return skipped;
        }

        public void setSkipped(int skipped)
        {
            this.skipped = skipped;
        }

        public double getTime()
        {
            return time;
        }

        public void setTime(double time)
        {
            this.time = time;
        }
    }
}
