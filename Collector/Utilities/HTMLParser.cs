using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Collector.Utilities
{
   public class HTMLParser : System.IDisposable
    {
        private string hTMLString;

        public string HTMLString
        {
            get => hTMLString;

            set
            {
                try
                {
                    Doc.LoadHtml(value);
                    hTMLString = value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private HtmlAgilityPack.HtmlDocument doc;


        public HtmlDocument Doc
        {
            get => doc;

            set
            {
                doc = value;
                hTMLString = doc.ParsedText;
            }
        }

        public HTMLParser()
        {
            Doc = new HtmlAgilityPack.HtmlDocument();
        }

        public string getAttributeValue(String xPath, String AttributeName, int NodeIndex = 0)
        {
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xPath);

            if (NodeIndex <= nodes.Count)
            {
                if (nodes[NodeIndex].Attributes.Contains(AttributeName))
                {
                    return nodes[NodeIndex].Attributes[AttributeName].Value;
                }
                else
                    throw new Exception("Invalid Attribute to search");
            }
            else
                throw new Exception("Index out of range.");
        }

        public string getNodeInnerText(String xPath, int NodeIndex = 0)
        {
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xPath);

            if (NodeIndex <= nodes.Count)
                return nodes[NodeIndex].InnerText;
            else
                throw new Exception("Index out of range.");
        }

        public HtmlNodeCollection getNodes(string xPath)
        {
            return doc.DocumentNode.SelectNodes(xPath);
        }


        public void Dispose()
        {
            this.hTMLString = null;
            this.doc = null;

        }
    }
}
