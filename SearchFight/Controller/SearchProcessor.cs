using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SearchFight.Model;
using System.Threading;

/// <summary>
/// Controller class that handles the main behaviour
/// and makes the calculations
/// </summary>

namespace SearchFight.Controller
{
    public class SearchProcessor
    {
        private List<IQuery> _queries;
        private List<RunnerSerializer> _runners;
        private Dictionary<string,string> _partialWinners;
        private long[,] _results;
        private string _totalWinner;
        private List<Thread> _threads;
        private object syncLock = new object();


        public List<string> Queries
        {
            get { return _queries.ConvertAll((res) => res.QueryText); }
        }


        public Dictionary<string, string> PartialWinners
        {
            get { return _partialWinners; }
        }


        public string TotalWinner
        {
            get { return _totalWinner; }
        }
        

        public long[,] Results
        {
            get { return _results; }
        }


        public SearchProcessor(List<RunnerSerializer> runners)
        {
            _runners = runners;
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
            var result = new Dictionary<string, long>();
            _results = new long[_queries.Count, _runners.Count];

            for (int i = 0; i < _queries.Count; i++)
            {                
                for (int j = 0; j < _runners.Count; j++)
                {
                    var runner = _runners[j] as SearchRunner;
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

        private void ProcessQuery(SearchRunner runner, int i, int j)
        {
            long result = runner.ProcessQuery(_queries[i]);

            lock (syncLock)
            {
                _results[i, j] = result;
            }            
        }

        private void GetTotalWinner()
        {
            long max = 0;
            int maxIndex = -1;
            for (int i = 0; i < _queries.Count; i++)
            {
                long sum = GetTotalResults(i);
                if (sum > max)
                {
                    max = sum;
                    maxIndex = i;
                }
            }

            _totalWinner = _queries[maxIndex].QueryText;
        }


        private void GetPartialWinners()
        {
            for (int i = 0; i < _runners.Count; i++)
            {
                int index = GetMaxIndex(i);
                _partialWinners[_runners[i].SearchEngineName] = _queries[index].QueryText;
            }
        }


        private int GetMaxIndex(int index)
        {
            long max = 0;
            int maxIndex=-1;
            for (int i = 0; i < _queries.Count; i++)
            {                
                if (_results[i, index] > max)
                {
                    max = _results[i, index];
                    maxIndex = i;
                }                    
            }
            return maxIndex;
        }

        private long GetTotalResults(int index)
        {
            long sum = 0;

            for (int i = 0; i < _runners.Count; i++)
            {
                sum += _results[index, i];
            }
            return sum;
        }
    }
}
