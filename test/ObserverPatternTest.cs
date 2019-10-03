using System.Linq;
using System.Collections.Generic;
using Bogus;
using FluentAssertions;
using src;
using Xunit;

namespace test
{
    public class ObserverPatternTest
    {
        [Fact]
        public void ShouldPublishToAllSubscribers()
        {
            var newsHandler = new NewsHandler();
            var newsPaperOne = new NewsPaper("New York Times");
            var newsPaperTwo = new NewsPaper("Washington Post");

            var news = GenerateTestNews();

            newsHandler.PostNews(news[0]);

            newsPaperOne.Subscribe(newsHandler);

            newsHandler.PostNews(news[1]);

            newsPaperTwo.Subscribe(newsHandler);

            newsHandler.PostNews(news[2]);

            newsPaperOne.newsInfo.Count.Should().Be(3);
            newsPaperTwo.newsInfo.Count.Should().Be(3);

            newsPaperOne.newsInfo[0].Should().Be(news[0].ToString());
            newsPaperOne.newsInfo[1].Should().Be(news[1].ToString());
            newsPaperOne.newsInfo[2].Should().Be(news[2].ToString());

            newsPaperTwo.newsInfo[0].Should().Be(news[0].ToString());
            newsPaperTwo.newsInfo[1].Should().Be(news[1].ToString());
            newsPaperTwo.newsInfo[2].Should().Be(news[2].ToString());
        }

        [Fact]
        public void ShouldResetAllNewsWhenUnsubscribed()
        {
            var newsHandler = new NewsHandler();
            var newsPaperOne = new NewsPaper("New York Times");
            var news = GenerateTestNews();

            news.ToList().ForEach(newsHandler.PostNews);

            newsPaperOne.Subscribe(newsHandler);

            newsPaperOne.newsInfo.Count.Should().Be(3);

            newsPaperOne.Unsubscribe();

            newsPaperOne.newsInfo.Should().BeEmpty();
        }

        private static IList<News> GenerateTestNews()
        {
            var ids = 1;

            return new Faker<News>()
            .StrictMode(true)
            .RuleFor(n => n.Id, f => ids++)
            .RuleFor(n => n.Headline, f => f.Lorem.Sentence())
            .RuleFor(n => n.Description, f => f.Lorem.Paragraph())
            .Generate(3);
        }
    }
}
