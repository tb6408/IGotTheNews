using System.ServiceModel.Syndication;
using System.Xml;
using System.IO;
using System.Linq;
using System.Text;

public class NewsService
{
    private const string BaseDirectory = "C:\\Users\\tb640\\Documents\\Repos\\IGotTheNews\\Content\\";
    private const string RawNewsDirectory = "RawNews\\";
    private const string IndividualStoriesDirectory = "IndividualStories\\";

    private static string RawNewsPath => $"{BaseDirectory}{RawNewsDirectory}";
    private static string IndividualStoriesPath => $"{BaseDirectory}{IndividualStoriesDirectory}";

    public static void LoadNews(IList<FeedRequestModel> requests)
    {
        foreach(var req in requests)
        {
            XmlReader reader = XmlReader.Create(req.FeedLink);
            SyndicationFeed feed = SyndicationFeed.Load(reader);   

            var filePath = $"{RawNewsPath}{req.FileName}";
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            XmlWriter rssWriter = XmlWriter.Create(filePath);
            Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(feed);
            rssFormatter.WriteTo(rssWriter);
            rssWriter.Close();
        }
    }

    public static Dictionary<string, SyndicationFeed> ReadNews(IList<FeedRequestModel> reqList)
    {
        var feedsDict = new Dictionary<string, SyndicationFeed>();

        foreach(var req in reqList)
        {
            var filePath = $"{RawNewsPath}{req.FileName}";
            var reader = XmlReader.Create(filePath);
            
            feedsDict.TryAdd(req.FileName, SyndicationFeed.Load(reader));         
        }
        return feedsDict;
    }

    public static void WriteTheStories(Dictionary<string, SyndicationFeed> feedDict)
    {
        foreach(var feed in feedDict)
        {
            var filePath = $"{IndividualStoriesPath}{feed.Key}";
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            var sb = new StringBuilder();
            foreach(var feedItem in feed.Value.Items)
            {     
                sb.AppendLine("***************************");
                sb.AppendLine($"URL: {feedItem.Id}");
                sb.AppendLine($"Title: {feedItem.Title.Text}");
                sb.AppendLine($"Publish Date: {feedItem.PublishDate}");
                sb.AppendLine($"Last Update: {feedItem.LastUpdatedTime}");
                sb.AppendLine("***************************");
                sb.AppendLine("");
                File.AppendAllText(filePath, sb.ToString());
            }
        }
    }
}