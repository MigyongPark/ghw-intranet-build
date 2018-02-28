using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Examine;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Highlight;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using System.IO;

namespace USNStarterKit.USNBusinessLogic
{

    /// <summary>
    /// Summary description for USNLuceneHelper
    /// </summary>
    public class USNLuceneHelper
    {
        private readonly Lucene.Net.Util.Version _luceneVersion = Lucene.Net.Util.Version.LUCENE_29;

        protected Dictionary<string, QueryParser> QueryParsers = new Dictionary<string, QueryParser>();

        public string Separator { get; set; }
        public int MaxNumHighlights { get; set; }
        public Formatter HighlightFormatter { get; set; }
        public Analyzer HighlightAnalyzer { get; set; }

        private static readonly USNLuceneHelper instance = new USNLuceneHelper();

        public static USNLuceneHelper Instance
        {
            get { return instance; }
        }

        private USNLuceneHelper()
        {
            Separator = "...";
            MaxNumHighlights = 5;
            HighlightAnalyzer = new StandardAnalyzer(_luceneVersion);
            HighlightFormatter = new SimpleHTMLFormatter("<span class=\"label c1-bg c5-text\">", "</span>");
        }

        public string GetHighlight(string value, string highlightField, Searcher searcher, string luceneRawQuery)
        {
            var query = GetQueryParser(highlightField).Parse(luceneRawQuery);
            var scorer = new QueryScorer(searcher.Rewrite(query));

            var highlighter = new Highlighter(HighlightFormatter, scorer);

            var tokenStream = HighlightAnalyzer.TokenStream(highlightField, new StringReader(value));
            string bestFragments = highlighter.GetBestFragments(tokenStream, value, MaxNumHighlights, Separator);
            return bestFragments;
        }

        protected QueryParser GetQueryParser(string highlightField)
        {
            if (!QueryParsers.ContainsKey(highlightField))
            {
                QueryParsers[highlightField] = new QueryParser(_luceneVersion, highlightField, HighlightAnalyzer);
            }
            return QueryParsers[highlightField];
        }
    }

}