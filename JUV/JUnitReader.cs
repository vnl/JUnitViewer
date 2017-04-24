using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Linq;
using JUV.JUnit.Model;
using JUV.JUnit.Parser;

namespace JUnitViewer
{
    public class JUnitReader
    {
        public static JUnitSummary summary;
        public static JUnitParser parser;
        public JUnitReader()
        {
            
        }

        public bool ParseJUnitFile(string fileLocation)
        {
            parser = new JUnitParser();
            summary = new JUnitSummary();
            IEnumerable<XElement> nodeList;
            try
            {
                nodeList = parser.GetStartNode(fileLocation);
                parser.ParseJUnitResults(nodeList);
                summary.AddTestSuite(parser.GetTestSuite());
                parser.AddTestCasesToTestSuite();
                summary.CalculateSummaryResults();
            }
            catch (Exception e)
            {

                MessageBoxResult result = MessageBox.Show(e.Message.ToString(), "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            }
            return true;
        }
    }
}
