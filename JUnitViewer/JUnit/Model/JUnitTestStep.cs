using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JUnitViewer.JUnit.Model
{
    /// <summary>
    /// This class is used for test steps information
    /// </summary>
    public class JUnitTestStep
    {
        private string name;
        private string time;
        private Dictionary<string, string> assertionFailuresList = new Dictionary<string, string>();
        private bool skipped = false;

        public JUnitTestStep()
        {

        }

        public string getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public String getTime()
        {
            return time;
        }

        public void setTime(String time)
        {
            this.time = time;
        }

        public Dictionary<string, string> getAssertionFailuresList()
        {
            return assertionFailuresList;
        }

        public void setAssertionFailures(String message, String value)
        {
            assertionFailuresList.Add(message, value);
        }

        public Boolean isSkipped()
        {
            return skipped;
        }

        public void setSkipped(Boolean skipped)
        {
            this.skipped = skipped;
        }
    }
}
