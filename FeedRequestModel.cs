using System;

public class FeedRequestModel
{
    public FeedRequestModel(string fileName, string feedLink, DateTime? lastUpdateDate = null){
        FeedLink = feedLink;
        FileName = fileName;
        LastUpdateDate = lastUpdateDate;
    }

    public string FileName {get;}
    public string FeedLink {get;}
    public DateTime? LastUpdateDate {get;}
}