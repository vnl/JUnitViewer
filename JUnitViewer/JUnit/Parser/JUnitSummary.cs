using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JUnitViewer.JUnit.Model;

namespace JUnitViewer.JUnit.Parser
{
    /// <summary>
    /// Class used to aggregate summary results
    /// </summary>
    public class JUnitSummary
    {
        private int totalSuccess;
        private int totalFailed;
        private int totalSkipped;
        private int totalTests;
        private double totalExecutionTime;
        private List<JUnitTestSuite> testSuites = new List<JUnitTestSuite>();

        public JUnitSummary()
        {

        }

        public int getTotalSuccess()
        {
            return totalSuccess;
        }

        public int getTotalFailed()
        {
            return totalFailed;
        }

        public int getTotalSkipped()
        {
            return totalSkipped;
        }

        public int getTotalTests()
        {
            return totalTests;
        }

        public double getTotalExecutionTime()
        {
            return totalExecutionTime;
        }

        public void addTestSuite(JUnitTestSuite testSuite)
        {
            testSuites.Add(testSuite);
        }

        public void calculateSummaryResults()
        {
            foreach (JUnitTestSuite testSuite in testSuites)
            {
                totalTests = totalTests + testSuite.getTests();
                totalFailed = totalFailed + testSuite.getFailures();
                totalSkipped = totalSkipped + testSuite.getSkipped();
                totalExecutionTime = totalExecutionTime + testSuite.getTime();
            }
            totalSuccess = totalTests - totalFailed - totalSkipped;
        }

        public double getSuccessRate()
        {
            return (getTotalSuccess() / (double)getTotalTests()) * 100;
            //return double.parseDouble(new DecimalFormat("###.##").format((getTotalSuccess() / (double)getTotalTests()) * 100));
        }
    }
}
