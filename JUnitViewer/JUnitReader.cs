using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using JUnitViewer.JUnit.Parser;

namespace JUnitViewer
{
    public class JUnitReader
    {
        public JUnitReader()
        {
            
        }

        public bool parseJUnitFile(string fileLocation)
        {
            JUnitParser parser = new JUnitParser();
            JUnitSummary summary = new JUnitSummary();
            IEnumerable<XElement> nodeList;
            try
            {
                nodeList = parser.getStartNode(fileLocation);
                parser.ParseJUnitResults(nodeList);
                summary.addTestSuite(parser.getTestSuite());
                parser.addTestCasesToTestSuite();
                summary.calculateSummaryResults();

                // Code added for implementing the buildAction screen
                //JUnitParserBuildAction buildAction = new JUnitParserBuildAction(parser.getTestSuite(), summary, build);
                //build.addAction(buildAction);

            }
            catch (Exception e)
            {
                //e.Message();
            }
            return true;
        }
    }
}
