using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Proj_1.News
{
    internal class NewsGenerator
    {
        private NewsIterator iterator;
        public NewsGenerator(List<Media> medias, List<IReportable> reportables)
        {
            iterator = new NewsIterator(medias, reportables);
        }

        public string GenerateNews()
        {
            if (!iterator.IsValid()) return null;
            (Media media, IReportable reportable) = iterator.Current;
            iterator.MoveNext();
            return reportable.Accept(media);
        }
    }

    public class NewsIterator : IEnumerator<(Media media, IReportable reportable)>
    {
        private List<Media> medias;
        private List<IReportable> reportables;
        private int i = 0;
        private int j = 0;

        public (Media media, IReportable reportable) Current => (medias[i], reportables[j]);

        object IEnumerator.Current => Current;

        public NewsIterator(List<Media> medias, List<IReportable> reportables)
        {
            this.medias = medias;
            this.reportables = reportables;
        }

        public bool MoveNext()
        {
            if (++j >= reportables.Count)
            {
                if (++i >= medias.Count) return false;
                j = 0;
            }
            return true;
        }

        public void Reset()
        {
            i = 0;
            j = 0;
        }

        public bool IsValid()
        {
            return (medias.Count > i && reportables.Count > j);
        }

        public void Dispose() { }
    }

}
