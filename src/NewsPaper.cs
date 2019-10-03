using System;
using System.Collections.Generic;

namespace src
{
    public class NewsPaper : IObserver<News>
    {
        public IList<string> newsInfo = new List<string>();
        private string name;
        private IDisposable cancellation;

        public NewsPaper(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("The observer must be assigned a name.");

            this.name = name;
        }

        public virtual void Subscribe(NewsHandler provider)
        {
            cancellation = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            cancellation.Dispose();
            newsInfo.Clear();
        }

        public virtual void OnCompleted()
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