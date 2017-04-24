using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JUnitViewer.JUnit.Model
{
    /// <summary>
    /// This class serves as container of test steps and gives additional info on test case level
    /// </summary>
    public class JUnitTestCase
    {
        private string className;
        private List<JUnitTestStep> testSteps = new List<JUnitTestStep>();
        private bool failed;

        public JUnitTestCase()
        {

        }

        public List<JUnitTestStep> getTestSteps()
        {
            return testSteps;
        }

        public void addTestStep(JUnitTestStep testStep)
        {
            foreach (KeyValuePair<string, string> entry in testStep.getAssertionFailuresList())
            {
                // do something with entry.Value or entry.Key
                if (entry.Key != null)
                {
                    if (!string.IsNullOrEmpty(entry.Key))
                    {
                        failed = true;
                    }
                }
            }
            testSteps.Add(testStep);
        }

        public String getClassName()
        {
            return className;
        }

        public void setClassName(String className)
        {
            this.className = className;
        }

        public bool isFailed()
        {
            return failed;
        }
    }
}
