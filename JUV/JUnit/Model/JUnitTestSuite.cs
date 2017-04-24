using System.Collections.Generic;

namespace JUV.JUnit.Model
{
    /// <summary>
    /// This class serves as container of test cases and gives additional info on test suite level
    /// </summary>
    public class JUnitTestSuite
    {
        private string _name;
        private int _tests;
        private int _failures;
        private int _skipped;
        private double _time;
        private List<JUnitTestCase> _testCases = new List<JUnitTestCase>();

        public JUnitTestSuite()
        {

        }

        public List<JUnitTestCase> GetTestCases()
        {
            return _testCases;
        }

        public void AddTestCase(JUnitTestCase testCase)
        {
            _testCases.Add(testCase);
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            this._name = name;
        }

        public int GetTests()
        {
            return _tests;
        }

        public void SetTests(int tests)
        {
            this._tests = tests;
        }

        public int GetFailures()
        {
            return _failures;
        }

        public void SetFailures(int failures)
        {
            this._failures = failures;
        }

        public int GetSkipped()
        {
            return _skipped;
        }

        public void SetSkipped(int skipped)
        {
            this._skipped = skipped;
        }

        public double GetTime()
        {
            return _time;
        }

        public void SetTime(double time)
        {
            this._time = time;
        }
    }
}
