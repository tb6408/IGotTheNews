using System;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ServiceModel.Syndication;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        var reqList = new List<FeedRequestModel>
        {
            new FeedRequestModel("TopStories", "http://rss.cnn.com/rss/cnn_topstories.rss")
        };

        NewsService.LoadNews(reqList);
        var feedDict = NewsService.ReadNews(reqList);
        NewsService.WriteTheStories(feedDict);

    }
}