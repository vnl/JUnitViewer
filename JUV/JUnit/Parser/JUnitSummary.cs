using System.Collections.Generic;
using JUV.JUnit.Model;

namespace JUV.JUnit.Parser
{
    /// <summary>
    /// Class used to aggregate summary results
    /// </summary>
    public class JUnitSummary
    {
        private int _totalSuccess;
        private int _totalFailed;
        private int _totalSkipped;
        private int _totalTests;
        private double _totalExecutionTime;
        private List<JUnitTestSuite> _testSuites = new List<JUnitTestSuite>();

        public JUnitSummary()
        {

        }

        public int GetTotalSuccess()
        {
            return _totalSuccess;
        }

        public int GetTotalFailed()
        {
            return _totalFailed;
        }

        public int GetTotalSkipped()
        {
            return _totalSkipped;
        }

        public int GetTotalTests()
        {
            return _totalTests;
        }

        public double GetTotalExecutionTime()
        {
            return _totalExecutionTime;
        }

        public void AddTestSuite(JUnitTestSuite testSuite)
        {
            _testSuites.Add(testSuite);
        }

        public void CalculateSummaryResults()
        {
            foreach (JUnitTestSuite testSuite in _testSuites)
            {
                _totalTests = _totalTests + testSuite.GetTests();
                _totalFailed = _totalFailed + testSuite.GetFailures();
                _totalSkipped = _totalSkipped + testSuite.GetSkipped();
                _totalExecutionTime = _totalExecutionTime + testSuite.GetTime();
            }
            _totalSuccess = _totalTests - _totalFailed - _totalSkipped;
        }

        public double GetSuccessRate()
        {
            return (GetTotalSuccess() / (double)GetTotalTests()) * 100;
            //return double.parseDouble(new DecimalFormat("###.##").format((getTotalSuccess() / (double)getTotalTests()) * 100));
        }
    }
}
