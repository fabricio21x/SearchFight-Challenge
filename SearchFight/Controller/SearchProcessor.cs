using System.Collections.Generic;
using System.Threading;
using SearchFight.Model;
using SearchFight.Model.Interfaces;

/// <summary>
/// Controller class that handles the main behaviour
/// and makes the calculations
/// </summary>

namespace SearchFight.Controller
{
    public class SearchProcessor
    {
        private readonly List<IQuery> _queries;
        private readonly List<ISearchEngine> _searchEngines;
        private readonly Dictionary<string,string> _partialWinners;
        private double[,] _results;
        private readonly List<Thread> _threads;
        private readonly object _syncLock = new object();


        public List<string> Queries
        {
            get { return _queries.ConvertAll(res => res.QueryText); }
        }


        public Dictionary<string, string> PartialWinners => _partialWinners;


        public string TotalWinner { get; private set; }


        public double[,] Results => _results;


        public SearchProcessor(List<ISearchEngine> searchEngines)
        {
            _searchEngines = searchEngines;
            _queries = new List<IQuery>();
            _partialWinners = new Dictionary<string, string>();
            _threads = new List<Thread>();         
        }


        public void AddQuery(IQuery query)
        {
            _queries.Add(query);
        }


        public void ProcessQueries()
        {            
            _results = new double[_queries.Count, _searchEngines.Count];

            for (int i = 0; i < _queries.Count; i++)
            {                
                for (int j = 0; j < _searchEngines.Count; j++)
                {
                    var runner = _searchEngines[j];
                    int a = i, b = j;
                    Thread thr = new Thread(() => ProcessQuery(runner,a,b));
                    _threads.Add(thr);
                    thr.Start();
                }
            }

            foreach (var thread in _threads)
            {
                    thread.Join();
            }

            GetPartialWinners();
            GetTotalWinner();                                
        }

        private void ProcessQuery(ISearchEngine runner, int i, int j)
        {
            var result = runner.ProcessQuery(_queries[i]);

            lock (_syncLock)
            {
                _results[i, j] = result;
            }            
        }

        private void GetTotalWinner()
        {
            double max = 0;
            var maxIndex = -1;
            for (var i = 0; i < _queries.Count; i++)
            {
                var sum = GetTotalResults(i);
                if (!(sum > max)) continue;
                max = sum;
                maxIndex = i;
            }

            TotalWinner = _queries[maxIndex].QueryText;
        }


        private void GetPartialWinners()
        {
            for (var i = 0; i < _searchEngines.Count; i++)
            {
                int index = GetMaxIndex(i);
                _partialWinners[_searchEngines[i].Name] = _queries[index].QueryText;
            }
        }


        private int GetMaxIndex(int index)
        {
            double max = 0;
            var maxIndex=-1;
            for (var i = 0; i < _queries.Count; i++)
            {
                if (!(_results[i, index] > max)) continue;
                max = _results[i, index];
                maxIndex = i;
            }
            return maxIndex;
        }

        private double GetTotalResults(int index)
        {
            double sum = 0;

            for (var i = 0; i < _searchEngines.Count; i++)
            {
                sum += _results[index, i];
            }
            return sum;
        }
    }
}
