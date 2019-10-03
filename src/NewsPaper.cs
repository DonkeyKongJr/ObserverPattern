using System;
using System.Collections.Generic;

namespace src
{
    public class NewsPaper : IObserver<News>
    {
        public IList<string> newsInfo = new List<string>();
        private string _name;
        private IDisposable cancellation;

        public NewsPaper(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("The observer must be assigned a name.");
            }

            _name = name;
        }

        public void Subscribe(NewsHandler provider)
        {
            cancellation = provider.Subscribe(this);
        }

        public void Unsubscribe()
        {
            cancellation.Dispose();
            newsInfo.Clear();
        }

        public void OnCompleted()
        {
            newsInfo.Clear();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(News news)
        {
            newsInfo.Add(news.ToString());
        }
    }
}