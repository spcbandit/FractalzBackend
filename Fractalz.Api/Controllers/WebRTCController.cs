using OpenTokSDK;
using OpenTokSDK.Render;

public class VideoChat
{
    private const string ApiKey = "your_api_key";
    private const string ApiSecret = "your_api_secret";
    private readonly OpenTok _opentok;
    private readonly Session _session;
    private readonly Publisher _publisher;
    private Subscriber _subscriber;
    private readonly string _publisherToken;

    public VideoChat()
    {
        _opentok = new OpenTok(ApiKey, ApiSecret);
        _session = _opentok.CreateSession();
        _publisherToken = _session.GenerateToken();
        _publisher = new Publisher(Context.Instance, "publisherDiv", new StartRenderRequest.PublisherProperties
        {
            ShowMicButton = false,
            Width = 240,
            Height = 180
        });
        _publisher.StartPublishing();
    }

    public string StartChat()
    {
        return _session.Id + ":" + _publisherToken;
    }

    public void JoinChat(string sessionId, string subscriberToken)
    {
        _subscriber = new Subscriber(Context.Instance, "subscriberDiv", subscriberToken);
        _subscriber.SubscribeToAudio = false;
        _subscriber.SubscribeToVideo = true;
        _subscriber.StartSubscribing();
    }

    public void EndChat()
    {
        _subscriber.EndSubscribing();
        _publisher.StopPublishing();
    }
}