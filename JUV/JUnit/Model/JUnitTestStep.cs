using System;
using System.Collections.Generic;

namespace JUV.JUnit.Model
{
    /// <summary>
    /// This class is used for test steps information
    /// </summary>
    public class JUnitTestStep
    {
        private string _name;
        private string _time;
        private Dictionary<string, string> _assertionFailuresList = new Dictionary<string, string>();
        private bool _skipped = false;

        public JUnitTestStep()
        {

        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(String name)
        {
            this._name = name;
        }

        public String GetTime()
        {
            return _time;
        }

        public void SetTime(String time)
        {
            this._time = time;
        }

        public Dictionary<string, string> GetAssertionFailuresList()
        {
            return _assertionFailuresList;
        }

        public void SetAssertionFailures(String message, String value)
        {
            _assertionFailuresList.Add(message, value);
        }

        public Boolean IsSkipped()
        {
            return _skipped;
        }

        public void SetSkipped(Boolean skipped)
        {
            this._skipped = skipped;
        }
    }
}
