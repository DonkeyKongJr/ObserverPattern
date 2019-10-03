using System;
using System.Collections.Generic;

namespace src
{
    public class NewsHandler : IObservable<News>
    {
        private readonly IList<IObserver<News>> observers;
        private readonly IList<News> newsList;

        public NewsHandler()
        {
            observers = new List<IObserver<News>>();
            newsList = new List<News>();
        }

        public IDisposable Subscribe(IObserver<News> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                // Provide observer with existing data.
                foreach (var item in newsList)
                {
                    observer.OnNext(item);
                }
            }
            return new Unsubscriber<News>(observers, observer);
        }

        public void PostNews(News newNews)
        {
            if (newNews.Id > 0 && !newsList.Contains(newNews))
            {
                newsList.Add(newNews);

                foreach (var observer in observers)
                {
                    observer.OnNext(newNews);
                }
            }
        }
    }
}