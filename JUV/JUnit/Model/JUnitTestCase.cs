using System;
using System.Collections.Generic;

namespace JUV.JUnit.Model
{
    /// <summary>
    /// This class serves as container of test steps and gives additional info on test case level
    /// </summary>
    public class JUnitTestCase
    {
        private string _className;
        private List<JUnitTestStep> _testSteps = new List<JUnitTestStep>();
        private bool _failed;

        public JUnitTestCase()
        {

        }

        public List<JUnitTestStep> GetTestSteps()
        {
            return _testSteps;
        }

        public void AddTestStep(JUnitTestStep testStep)
        {
            foreach (KeyValuePair<string, string> entry in testStep.GetAssertionFailuresList())
            {
                // do something with entry.Value or entry.Key
                if (entry.Key != null)
                {
                    if (!string.IsNullOrEmpty(entry.Key))
                    {
                        _failed = true;
                    }
                }
            }
            _testSteps.Add(testStep);
        }

        public String GetClassName()
        {
            return _className;
        }

        public void SetClassName(String className)
        {
            this._className = className;
        }

        public bool IsFailed()
        {
            return _failed;
        }
    }
}
